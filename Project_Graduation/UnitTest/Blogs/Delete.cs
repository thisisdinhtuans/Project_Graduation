using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Repositories.BlogRepository;
using Infrastructure.Services.BlogService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Blogs
{
    public class Delete
    {
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BlogService _blogService;
        public Delete()
        {
            _mockBlogRepository = new Mock<IBlogRepository>();
            _mockMapper = new Mock<IMapper>();
            _blogService = new BlogService(
                _mockBlogRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task DeleteBlogAsync_ShouldReturnSuccess_WhenBlogIsDeleted()
        {
            // Arrange
            var blogId = 1;
            var blog = new Blog
            {
                BlogID = blogId,
                Title = "Blog to Delete",
                Image = "image.jpg",
                SubTitle = "Subtitle",
                Description = "Description of the blog",
                Status = true
            };

            _mockBlogRepository.Setup(repo => repo.GetByIdAsync(blogId))
                .ReturnsAsync(blog);

            // Act
            var result = await _blogService.DeleteBlogAsync(blogId);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockBlogRepository.Verify(repo => repo.Delete(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBlogAsync_ShouldReturnError_WhenBlogNotFound()
        {
            // Arrange
            var blogId = 1;

            _mockBlogRepository.Setup(repo => repo.GetByIdAsync(blogId))
                .ReturnsAsync((Blog)null); // Blog not found

            // Act
            var result = await _blogService.DeleteBlogAsync(blogId);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Blog không được tìm thấy.", result.Message);
            _mockBlogRepository.Verify(repo => repo.Delete(It.IsAny<Blog>()), Times.Never);
        }

    }
}
