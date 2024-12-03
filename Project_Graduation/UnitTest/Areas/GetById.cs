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
    public class GetById
    {
        private readonly Mock<IAreaRepository> _mockAreaRepository;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AreaService _areaService;

        public GetById()
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
        public async Task GetAreaByIdAsync_ShouldReturnArea_WhenAreaExists()
        {
            // Arrange
            var areaId = 1;
            var area = new Area { AreaID = areaId, AreaName = "Test Area", RestaurantID = 1 };

            _mockAreaRepository.Setup(repo => repo.GetByIdAsync(areaId))
                .ReturnsAsync(area);

            // Act
            var result = await _areaService.GetAreaByIdAsync(areaId);

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(areaId, result.ResultObj.AreaID);
            _mockAreaRepository.Verify(repo => repo.GetByIdAsync(areaId), Times.Once);
        }

        [Fact]
        public async Task GetAreaByIdAsync_ShouldReturnError_WhenExceptionOccurs()
        {
            // Arrange
            var areaId = 1;
            _mockAreaRepository.Setup(repo => repo.GetByIdAsync(areaId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _areaService.GetAreaByIdAsync(areaId);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Database error", result.Message);
            _mockAreaRepository.Verify(repo => repo.GetByIdAsync(areaId), Times.Once);
        }
    }
}
