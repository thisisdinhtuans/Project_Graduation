using AutoMapper;
using Domain.Models.Dto.Area;
using Infrastructure.Entities;
using Infrastructure.Repositories.AreaRepository;
using Infrastructure.Services.AreaService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Areas
{
    public class Update
    {
        private readonly Mock<IAreaRepository> _mockAreaRepository;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AreaService _areaService;

        public Update()
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
        public async Task UpdateAreaAsync_ShouldReturnSuccess_WhenAreaIsUpdated()
        {
            // Arrange
            var areaDto = new AreaDto { AreaID = 1, AreaName = "Updated Area", RestaurantID = 1 };
            var area = new Area { AreaID = 1, AreaName = "Original Area", RestaurantID = 1 };

            _mockAreaRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()))
                .ReturnsAsync(false); // Area name does not already exist

            _mockAreaRepository.Setup(repo => repo.GetByIdAsync(areaDto.AreaID))
                .ReturnsAsync(area);

            _mockMapper.Setup(mapper => mapper.Map(areaDto, area));

            // Act
            var result = await _areaService.UpdateAreaAsync(areaDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockAreaRepository.Verify(repo => repo.Update(It.IsAny<Area>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAreaAsync_ShouldReturnError_WhenAreaAlreadyExists()
        {
            // Arrange
            var areaDto = new AreaDto { AreaID = 1, AreaName = "Existing Area", RestaurantID = 1 };

            _mockAreaRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()))
                .ReturnsAsync(true); // Area name already exists

            // Act
            var result = await _areaService.UpdateAreaAsync(areaDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Khu vực  này đã tồn tại.", result.Message);
            _mockAreaRepository.Verify(repo => repo.Update(It.IsAny<Area>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAreaAsync_ShouldThrowError_WhenAreaNotFound()
        {
            // Arrange
            var areaDto = new AreaDto { AreaID = 1, AreaName = "Nonexistent Area", RestaurantID = 1 };

            _mockAreaRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()))
                .ReturnsAsync(false);

            _mockAreaRepository.Setup(repo => repo.GetByIdAsync(areaDto.AreaID))
                .ReturnsAsync((Area)null); // Area not found

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _areaService.UpdateAreaAsync(areaDto));
            _mockAreaRepository.Verify(repo => repo.Update(It.IsAny<Area>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAreaAsync_ShouldReturnError_WhenExceptionOccurs()
        {
            // Arrange
            var areaDto = new AreaDto { AreaID = 1, AreaName = "Area with Exception", RestaurantID = 1 };
            var area = new Area { AreaID = 1, AreaName = "Area Name", RestaurantID = 1 };

            _mockAreaRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()))
                .ReturnsAsync(false);

            _mockAreaRepository.Setup(repo => repo.GetByIdAsync(areaDto.AreaID))
                .ReturnsAsync(area);

            _mockAreaRepository.Setup(repo => repo.Update(It.IsAny<Area>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _areaService.UpdateAreaAsync(areaDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Database error", result.Message);
            _mockAreaRepository.Verify(repo => repo.Update(It.IsAny<Area>()), Times.Once);
        }
    }
}
