using AutoMapper;
using Domain.Models.Dto.Area;
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
    public class GetAll
    {
        private readonly Mock<IAreaRepository> _mockAreaRepository;
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AreaService _areaService;

        public GetAll()
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
        public async Task GetAllAreasAsync_ShouldReturnAllAreas()
        {
            // Arrange
            var areas = new List<Area>
    {
        new Area { AreaID = 1, AreaName = "Area 1", RestaurantID = 101 },
        new Area { AreaID = 2, AreaName = "Area 2", RestaurantID = 102 },
    };

            var areaDtos = new List<AreaDto>
    {
        new AreaDto { AreaID = 1, AreaName = "Area 1", RestaurantID = 101 },
        new AreaDto { AreaID = 2, AreaName = "Area 2", RestaurantID = 102 },
    };

            _mockAreaRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(areas);

            _mockMapper.Setup(mapper => mapper.Map<List<AreaDto>>(areas))
                .Returns(areaDtos);

            // Act
            var result = await _areaService.GetAllAreasAsync();

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(2, result.ResultObj.Count);
            _mockAreaRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<AreaDto>>(areas), Times.Once);
        }

        [Fact]
        public async Task GetAllAreasAsync_ShouldReturnError_WhenExceptionOccurs()
        {
            // Arrange
            _mockAreaRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _areaService.GetAllAreasAsync();

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Database error", result.Message);
            _mockAreaRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

    }
}
