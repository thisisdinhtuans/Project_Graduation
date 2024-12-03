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
    public class Delete
    {
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DishService _dishService;

        public Delete()
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
        public async Task DeleteDishAsync_ShouldReturnSuccess_WhenDishIsDeleted()
        {
            // Arrange
            var dishId = 1;
            var dish = new Dish
            {
                DishId = dishId,
                Name = "Dish to Delete",
                Price = 9.99,
                Description = "Description of the dish",
                Type = "Main",
                Image = "image.jpg",
                CategoryID = 1
            };

            _mockDishRepository.Setup(repo => repo.GetByIdAsync(dishId))
                .ReturnsAsync(dish);

            // Act
            var result = await _dishService.DeleteDishAsync(dishId);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockDishRepository.Verify(repo => repo.Delete(It.IsAny<Dish>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDishAsync_ShouldReturnError_WhenDishNotFound()
        {
            // Arrange
            var dishId = 1;

            _mockDishRepository.Setup(repo => repo.GetByIdAsync(dishId))
                .ReturnsAsync((Dish)null); // Dish not found

            // Act
            var result = await _dishService.DeleteDishAsync(dishId);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Món không được tìm thấy.", result.Message);
            _mockDishRepository.Verify(repo => repo.Delete(It.IsAny<Dish>()), Times.Never);
        }

    }
}
