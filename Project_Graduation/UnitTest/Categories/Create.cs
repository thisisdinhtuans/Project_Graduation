using AutoMapper;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Services.CategoryService;
using Moq;
using System.Linq.Expressions;

namespace UnitTest.Categories
{
    public class Create
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _categoryService;
        public Create()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _categoryService = new CategoryService(
                _mockCategoryRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task CreateCategoryAsync_ShouldReturnSuccess_WhenCategoryIsCreated()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto { Name = "Test Category" };
            var category = new Category { Name = "Test Category" };

            _mockCategoryRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(false); // Category name does not exist

            _mockMapper.Setup(mapper => mapper.Map<Category>(createCategoryDto))
                .Returns(category);

            // Act
            var result = await _categoryService.CreateCategoryAsync(createCategoryDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockCategoryRepository.Verify(repo => repo.Add(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldReturnError_WhenCategoryAlreadyExists()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto { Name = "Existing Category" };

            _mockCategoryRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(true); // Category name already exists

            // Act
            var result = await _categoryService.CreateCategoryAsync(createCategoryDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Loại món ăn với tên này đã tồn tại.", result.Message);
            _mockCategoryRepository.Verify(repo => repo.Add(It.IsAny<Category>()), Times.Never);
        }
    }
}
