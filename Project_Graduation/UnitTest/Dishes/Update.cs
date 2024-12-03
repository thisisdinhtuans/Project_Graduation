using AutoMapper;
using Domain.Models.Dto.Dish;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Repositories.DishRepository;
using Infrastructure.Services.DishService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Dishes
{
    public class Update
    {
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DishService _dishService;

        public Update()
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
        public async Task UpdateDishAsync_ShouldReturnSuccess_WhenDishIsUpdated()
        {
            // Arrange
            var dishDto = new DishDto
            {
                DishId = 1,
                Name = "Updated Dish",
                Price = 10.99,
                Description = "Updated description",
                Type = "Main",
                Image = "updated_image.jpg",
                CategoryID = 1
            };

            var dish = new Dish
            {
                DishId = 1,
                Name = "Original Dish",
                Price = 9.99,
                Description = "Original description",
                Type = "Main",
                Image = "original_image.jpg",
                CategoryID = 1
            };

            _mockDishRepository.Setup(repo => repo.GetByIdAsync(dishDto.DishId))
                .ReturnsAsync(dish);

            _mockDishRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Dish, bool>>>()))
                .ReturnsAsync(false);

            _mockMapper.Setup(mapper => mapper.Map(dishDto, dish));

            // Act
            var result = await _dishService.UpdateDishAsync(dishDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockDishRepository.Verify(repo => repo.Update(It.IsAny<Dish>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDishAsync_ShouldThrowError_WhenDishNotFound()
        {
            // Arrange
            var dishDto = new DishDto { DishId = 1, Name = "Nonexistent Dish" };

            _mockDishRepository.Setup(repo => repo.GetByIdAsync(dishDto.DishId))
                .ReturnsAsync((Dish)null); // Dish not found

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _dishService.UpdateDishAsync(dishDto));
            _mockDishRepository.Verify(repo => repo.Update(It.IsAny<Dish>()), Times.Never);
        }

    }
}
