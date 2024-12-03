using AutoMapper;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;
using Infrastructure.Services.RestaurantService;
using Moq;
using System.Linq.Expressions;

namespace UnitTest.Restaurants
{
    public class Create
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RestaurantService _restaurantService;
        public Create()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IMapper>();
            _restaurantService = new RestaurantService(
                _mockRestaurantRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task CreateRestaurantAsync_ShouldReturnSuccess_WhenRestaurantIsCreated()
        {
            // Arrange
            var createRestaurantDto = new CreateRestaurantDto { Address = "123 Main St" };
            var restaurant = new Restaurant { RestaurantID = 0, Address = "123 Main St" };

            _mockRestaurantRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Restaurant, bool>>>()))
                .ReturnsAsync(false); // Address does not exist

            _mockMapper.Setup(mapper => mapper.Map<Restaurant>(createRestaurantDto))
                .Returns(restaurant);

            // Act
            var result = await _restaurantService.CreateRestaurantAsync(createRestaurantDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockRestaurantRepository.Verify(repo => repo.Add(It.IsAny<Restaurant>()), Times.Once);
        }

        [Fact]
        public async Task CreateRestaurantAsync_ShouldReturnError_WhenAddressAlreadyExists()
        {
            // Arrange
            var createRestaurantDto = new CreateRestaurantDto { Address = "123 Main St" };

            _mockRestaurantRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Restaurant, bool>>>()))
                .ReturnsAsync(true); // Address already exists

            // Act
            var result = await _restaurantService.CreateRestaurantAsync(createRestaurantDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Nhà hàng với địa chỉ này đã tồn tại.", result.Message);
            _mockRestaurantRepository.Verify(repo => repo.Add(It.IsAny<Restaurant>()), Times.Never);
        }
    }
}
