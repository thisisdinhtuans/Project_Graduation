using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Repositories.AreaRepository;
using Infrastructure.Services.AreaService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Areas
{
    public class Delete
    {
        private readonly Mock<IAreaRepository> _mockAreaRepository;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AreaService _areaService;
        public Delete()
        {
            _mockAreaRepository = new Mock<IAreaRepository>();
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IMapper>();
            _areaService = new AreaService(
                _mockAreaRepository.Object,
                _mockRestaurantRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccessResult_WhenAreaIsDeleted()
        {
            // Arrange
            var areaId = 1;
            var area = new Area { AreaID = areaId, AreaName = "Test Area" };

            _mockAreaRepository.Setup(repo => repo.GetByIdAsync(areaId))
                .ReturnsAsync(area);
            _mockAreaRepository.Setup(repo => repo.Delete(area))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _areaService.DeleteAreaAsync(areaId);

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.True(result.ResultObj);
            _mockAreaRepository.Verify(repo => repo.GetByIdAsync(areaId), Times.Once);
            _mockAreaRepository.Verify(repo => repo.Delete(area), Times.Once);
        }
        [Fact]
        public async Task Delete_ShouldReturnErrorResult_WhenIdIsInvalid()
        {
            // Arrange
            var invalidId = -1;

            // Act
            var result = await _areaService.DeleteAreaAsync(invalidId);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Id không hợp lệ.", result.Message);
            _mockAreaRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockAreaRepository.Verify(repo => repo.Delete(It.IsAny<Area>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldReturnErrorResult_WhenAreaNotFound()
        {
            // Arrange
            var areaId = 1;

            _mockAreaRepository.Setup(repo => repo.GetByIdAsync(areaId))
                .ReturnsAsync((Area)null);

            // Act
            var result = await _areaService.DeleteAreaAsync(areaId);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Khu vực không được tìm thấy.", result.Message);
            _mockAreaRepository.Verify(repo => repo.GetByIdAsync(areaId), Times.Once);
            _mockAreaRepository.Verify(repo => repo.Delete(It.IsAny<Area>()), Times.Never);
        }

    }
}
