using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Repositories.DishRepository;
using Infrastructure.Services.DishService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Dishes
{
    public class GetById
    {
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DishService _dishService;

        public GetById()
        {
            _mockDishRepository = new Mock<IDishRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _dishService = new DishService(
                _mockDishRepository.Object,
                _mockCategoryRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task GetDishByIdAsync_ShouldReturnDish_WhenDishExists()
        {
            // Arrange
            var dishId = 1;
            var dish = new Dish
            {
                DishId = dishId,
                Name = "Test Dish",
                Price = 9.99,
                Description = "Description of the dish",
                Type = "Main",
                Image = "image.jpg",
                CategoryID = 1
            };

            _mockDishRepository.Setup(repo => repo.GetByIdAsync(dishId))
                .ReturnsAsync(dish);

            // Act
            var result = await _dishService.GetDishByIdAsync(dishId);

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(dishId, result.ResultObj.DishId);
            _mockDishRepository.Verify(repo => repo.GetByIdAsync(dishId), Times.Once);
        }

    }
}
