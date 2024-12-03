using AutoMapper;
using Domain.Models.Dto.Dish;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Repositories.DishRepository;
using Infrastructure.Services.DishService;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace UnitTest.Dishes
{
    public class Create
    {
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DishService _dishService;

        public Create()
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
        public async Task CreateDishAsync_ShouldReturnSuccess_WhenDishIsCreated()
        {
            // Arrange
            var createDishDto = new CreateDishDto
            {
                Name = "Test Dish",
                Price = 9.99,
                Description = "Delicious test dish",
                Type = "Main",
                Image = "image.jpg",
                CategoryID = 1
            };

            var dish = new Dish
            {
                DishId = 0,
                Name = "Test Dish",
                Price = 9.99,
                Description = "Delicious test dish",
                Type = "Main",
                Image = "image.jpg",
                CategoryID = 1
            };

            var category = new Category { IdCategory = 1, Name = "Main Dishes" };

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(createDishDto.CategoryID))
                .ReturnsAsync(category);

            _mockDishRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Dish, bool>>>()))
                .ReturnsAsync(false); // Dish name does not exist

            _mockMapper.Setup(mapper => mapper.Map<Dish>(createDishDto))
                .Returns(dish);

            // Act
            var result = await _dishService.CreateDishAsync(createDishDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockDishRepository.Verify(repo => repo.Add(It.IsAny<Dish>()), Times.Once);
        }

        [Fact]
        public async Task CreateDishAsync_ShouldReturnError_WhenDishAlreadyExists()
        {
            // Arrange
            var createDishDto = new CreateDishDto { Name = "Existing Dish", CategoryID = 1 };

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(createDishDto.CategoryID))
                .ReturnsAsync(new Category());

            _mockDishRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Dish, bool>>>()))
                .ReturnsAsync(true); // Dish name already exists

            // Act
            var result = await _dishService.CreateDishAsync(createDishDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Món  này đã tồn tại.", result.Message);
            _mockDishRepository.Verify(repo => repo.Add(It.IsAny<Dish>()), Times.Never);
        }

        [Fact]
        public async Task CreateDishAsync_ShouldReturnError_WhenCategoryDoesNotExist()
        {
            // Arrange
            var createDishDto = new CreateDishDto { Name = "Test Dish", CategoryID = 1 };

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(createDishDto.CategoryID))
                .ReturnsAsync((Category)null); // Category does not exist

            // Act
            var result = await _dishService.CreateDishAsync(createDishDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Loại món này không tồn tại", result.Message);
            _mockDishRepository.Verify(repo => repo.Add(It.IsAny<Dish>()), Times.Never);
        }

    }
}
