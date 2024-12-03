using AutoMapper;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;
using Infrastructure.Services.RestaurantService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Restaurants
{
    public class Update
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RestaurantService _restaurantService;
        public Update()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IMapper>();
            _restaurantService = new RestaurantService(
                _mockRestaurantRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task UpdateRestaurantAsync_ShouldReturnSuccess_WhenRestaurantIsUpdated()
        {
            // Arrange
            var restaurantDto = new RestaurantDto { RestaurantID = 1, Address = "456 Elm St" };
            var restaurant = new Restaurant { RestaurantID = 1, Address = "123 Main St" };

            _mockRestaurantRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Restaurant, bool>>>()))
                .ReturnsAsync(false); // Address does not exist

            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurantDto.RestaurantID))
                .ReturnsAsync(restaurant);

            _mockMapper.Setup(mapper => mapper.Map(restaurantDto, restaurant));

            // Act
            var result = await _restaurantService.UpdateRestaurantAsync(restaurantDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockRestaurantRepository.Verify(repo => repo.Update(It.IsAny<Restaurant>()), Times.Once);
        }

        [Fact]
        public async Task UpdateRestaurantAsync_ShouldThrowError_WhenRestaurantNotFound()
        {
            // Arrange
            var restaurantDto = new RestaurantDto { RestaurantID = 1, Address = "456 Elm St" };

            _mockRestaurantRepository.Setup(repo => repo.GetByIdAsync(restaurantDto.RestaurantID))
                .ReturnsAsync((Restaurant)null); // Restaurant not found

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _restaurantService.UpdateRestaurantAsync(restaurantDto));
            _mockRestaurantRepository.Verify(repo => repo.Update(It.IsAny<Restaurant>()), Times.Never);
        }
    }
}
