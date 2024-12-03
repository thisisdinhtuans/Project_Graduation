using AutoMapper;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Services.CategoryService;
using Moq;
using System.Linq.Expressions;

namespace UnitTest.Categories
{
    public class Update
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _categoryService;
        public Update()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _categoryService = new CategoryService(
                _mockCategoryRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task UpdateCategoryAsync_ShouldReturnSuccess_WhenCategoryIsUpdated()
        {
            // Arrange
            var categoryDto = new CategoryDto { IdCategory = 1, Name = "Updated Category" };
            var category = new Category { IdCategory = 1, Name = "Original Category" };

            _mockCategoryRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(false); // Category name does not already exist

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(categoryDto.IdCategory))
                .ReturnsAsync(category);

            _mockMapper.Setup(mapper => mapper.Map(categoryDto, category));

            // Act
            var result = await _categoryService.UpdateCategoryAsync(categoryDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockCategoryRepository.Verify(repo => repo.Update(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldReturnError_WhenCategoryAlreadyExists()
        {
            // Arrange
            var categoryDto = new CategoryDto { IdCategory = 1, Name = "Existing Category" };

            _mockCategoryRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(true); // Category name already exists

            // Act
            var result = await _categoryService.UpdateCategoryAsync(categoryDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Loại món ăn với tên này đã tồn tại.", result.Message);
            _mockCategoryRepository.Verify(repo => repo.Update(It.IsAny<Category>()), Times.Never);
        }

    }
}
