using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Services.RestaurantService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Restaurants
{
    public class Delete
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RestaurantService _restaurantService;
        public Delete()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IMapper>();
            _restaurantService = new RestaurantService(
                _mockRestaurantRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_ShouldReturnSuccess_WhenRestaurantIsDeleted()
        {
            // Arrange
            var restaurantId = 1;
            var restaurant = new Restaurant { RestaurantID = restaurantId, Address = "123 Main St" };

            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);

            // Act
            var result = await _restaurantService.DeleteRestaurantAsync(restaurantId);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockRestaurantRepository.Verify(repo => repo.Delete(It.IsAny<Restaurant>()), Times.Once);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_ShouldReturnError_WhenRestaurantNotFound()
        {
            // Arrange
            var restaurantId = 1;

            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync((Restaurant)null); // Restaurant not found

            // Act
            var result = await _restaurantService.DeleteRestaurantAsync(restaurantId);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Nhà hàng không được tìm thấy.", result.Message);
            _mockRestaurantRepository.Verify(repo => repo.Delete(It.IsAny<Restaurant>()), Times.Never);
        }
    }
}
