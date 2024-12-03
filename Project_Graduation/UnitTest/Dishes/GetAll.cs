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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Dishes
{
    public class GetAll
    {
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DishService _dishService;

        public GetAll()
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
        public async Task GetAllDishsAsync_ShouldReturnAllDishes()
        {
            // Arrange
            var dishes = new List<Dish>
    {
        new Dish { DishId = 1, Name = "Dish 1", Price = 9.99, Description = "Description 1", Type = "Main", Image = "image1.jpg", CategoryID = 1 },
        new Dish { DishId = 2, Name = "Dish 2", Price = 19.99, Description = "Description 2", Type = "Dessert", Image = "image2.jpg", CategoryID = 2 }
    };

            var dishDtos = new List<DishDto>
    {
        new DishDto { DishId = 1, Name = "Dish 1", Price = 9.99, Description = "Description 1", Type = "Main", Image = "image1.jpg", CategoryID = 1 },
        new DishDto { DishId = 2, Name = "Dish 2", Price = 19.99, Description = "Description 2", Type = "Dessert", Image = "image2.jpg", CategoryID = 2 }
    };

            _mockDishRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(dishes);

            _mockMapper.Setup(mapper => mapper.Map<List<DishDto>>(dishes))
                .Returns(dishDtos);

            // Act
            var result = await _dishService.GetAllDishsAsync();

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(2, result.ResultObj.Count);
            _mockDishRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

    }
}
