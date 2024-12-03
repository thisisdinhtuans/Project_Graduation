using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Dto.Area;
using Infrastructure.Entities;
using Infrastructure.Repositories.AreaRepository;
using Infrastructure.Services.AreaService;
using System.Linq.Expressions;

public class Create
{
    private readonly Mock<IAreaRepository> _mockAreaRepository;
    private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AreaService _areaService;

    public Create()
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
    public async Task CreateAreaAsync_ShouldReturnError_WhenRestaurantDoesNotExist()
    {
        // Arrange
        var createAreaDto = new CreateAreaDto { AreaName = "Test Area", RestaurantID = 1 };
        _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Restaurant)null); // Restaurant does not exist

        // Act
        var result = await _areaService.CreateAreaAsync(createAreaDto);

        // Assert
        Assert.False(result.IsSuccessed);
        Assert.Equal("Nhà hàng này không tồn tại", result.Message);
        _mockAreaRepository.Verify(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()), Times.Never);
        _mockAreaRepository.Verify(repo => repo.Add(It.IsAny<Area>()), Times.Never);
    }

    [Fact]
    public async Task CreateAreaAsync_ShouldReturnError_WhenAreaAlreadyExists()
    {
        // Arrange
        var createAreaDto = new CreateAreaDto { AreaName = "Test Area", RestaurantID = 1 };
        var restaurant = new Restaurant { RestaurantID = 1 };

        _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(restaurant);
        _mockAreaRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()))
            .ReturnsAsync(true); // Simulate that the area already exists

        // Act
        var result = await _areaService.CreateAreaAsync(createAreaDto);

        // Assert
        Assert.False(result.IsSuccessed);
        Assert.Equal("Khu vực  này đã tồn tại.", result.Message);
        _mockAreaRepository.Verify(repo => repo.Add(It.IsAny<Area>()), Times.Never);
    }


    [Fact]
    public async Task CreateAreaAsync_ShouldReturnSuccess_WhenAreaIsCreatedSuccessfully()
    {
        // Arrange
        var createAreaDto = new CreateAreaDto { AreaName = "Test Area", RestaurantID = 1 };
        var restaurant = new Restaurant { RestaurantID = 1 };
        var area = new Area { AreaName = "Test Area", RestaurantID = 1 };

        _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(restaurant);
        _mockAreaRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()))
        .ReturnsAsync(false);
        _mockMapper.Setup(mapper => mapper.Map<Area>(createAreaDto))
            .Returns(area);

        // Act
        var result = await _areaService.CreateAreaAsync(createAreaDto);

        // Assert
        Assert.True(result.IsSuccessed);
        Assert.True(result.ResultObj);
        _mockAreaRepository.Verify(repo => repo.Add(It.IsAny<Area>()), Times.Once);
    }

    [Fact]
    public async Task CreateAreaAsync_ShouldReturnError_WhenExceptionOccurs()
    {
        // Arrange
        var createAreaDto = new CreateAreaDto { AreaName = "Test Area", RestaurantID = 1 };
        var restaurant = new Restaurant { RestaurantID = 1 };

        _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(restaurant);
        _mockAreaRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Area, bool>>>()))
        .ReturnsAsync(false);
        _mockAreaRepository.Setup(repo => repo.Add(It.IsAny<Area>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _areaService.CreateAreaAsync(createAreaDto);

        // Assert
        Assert.False(result.IsSuccessed);
        Assert.Equal("Database error", result.Message);
        _mockAreaRepository.Verify(repo => repo.Add(It.IsAny<Area>()), Times.Once);
    }
}
