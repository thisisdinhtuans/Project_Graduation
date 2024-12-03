using AutoMapper;
using Domain.Models.Dto.Restaurant;
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
    public class GetAll
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RestaurantService _restaurantService;
        public GetAll()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IMapper>();
            _restaurantService = new RestaurantService(
                _mockRestaurantRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task GetAllRestaurantsAsync_ShouldReturnAllRestaurants()
        {
            // Arrange
            var restaurants = new List<Restaurant>
            {
                new Restaurant { RestaurantID = 1, Address = "123 Main St" },
                new Restaurant { RestaurantID = 2, Address = "456 Elm St" }
            };

                    var restaurantDtos = new List<RestaurantDto>
            {
                new RestaurantDto { RestaurantID = 1, Address = "123 Main St" },
                new RestaurantDto { RestaurantID = 2, Address = "456 Elm St" }
            };

            _mockRestaurantRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(restaurants);

            _mockMapper.Setup(mapper => mapper.Map<List<RestaurantDto>>(restaurants))
                .Returns(restaurantDtos);

            // Act
            var result = await _restaurantService.GetAllRestaurantsAsync();

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(2, result.ResultObj.Count);
            _mockRestaurantRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

    }
}
