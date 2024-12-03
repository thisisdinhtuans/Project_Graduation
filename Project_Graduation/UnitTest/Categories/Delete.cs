using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Services.CategoryService;
using Moq;

namespace UnitTest.Categories
{
    public class Delete
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _categoryService;
        public Delete()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _categoryService = new CategoryService(
                _mockCategoryRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task DeleteCategoryAsync_ShouldReturnSuccess_WhenCategoryIsDeleted()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { IdCategory = categoryId, Name = "Test Category" };

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(categoryId))
                .ReturnsAsync(category);

            // Act
            var result = await _categoryService.DeleteCategoryAsync(categoryId);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockCategoryRepository.Verify(repo => repo.Delete(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldReturnError_WhenCategoryNotFound()
        {
            // Arrange
            var categoryId = 1;

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(categoryId))
                .ReturnsAsync((Category)null); // Category not found

            // Act
            var result = await _categoryService.DeleteCategoryAsync(categoryId);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Loại món ăn không được tìm thấy.", result.Message);
            _mockCategoryRepository.Verify(repo => repo.Delete(It.IsAny<Category>()), Times.Never);
        }
    }
}
