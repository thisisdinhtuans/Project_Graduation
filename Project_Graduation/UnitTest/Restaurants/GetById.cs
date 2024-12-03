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
    public class GetById
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RestaurantService _restaurantService;
        public GetById()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IMapper>();
            _restaurantService = new RestaurantService(
                _mockRestaurantRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task GetRestaurantByIdAsync_ShouldReturnRestaurant_WhenRestaurantExists()
        {
            // Arrange
            var restaurantId = 1;
            var restaurant = new Restaurant { RestaurantID = restaurantId, Address = "123 Main St" };

            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);

            // Act
            var result = await _restaurantService.GetRestaurantByIdAsync(restaurantId);

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(restaurantId, result.ResultObj.RestaurantID);
            _mockRestaurantRepository.Verify(repo => repo.GetByIdAsync(restaurantId), Times.Once);
        }

    }
}
