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
    public class GetById
    {
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BlogService _blogService;
        public GetById()
        {
            _mockBlogRepository = new Mock<IBlogRepository>();
            _mockMapper = new Mock<IMapper>();
            _blogService = new BlogService(
                _mockBlogRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task GetBlogByIdAsync_ShouldReturnBlog_WhenBlogExists()
        {
            // Arrange
            var blogId = 1;
            var blog = new Blog
            {
                BlogID = blogId,
                Title = "Blog Title",
                Image = "image.jpg",
                SubTitle = "Subtitle",
                Description = "Description of the blog",
                Status = true
            };

            _mockBlogRepository.Setup(repo => repo.GetByIdAsync(blogId))
                .ReturnsAsync(blog);

            // Act
            var result = await _blogService.GetBlogByIdAsync(blogId);

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(blogId, result.ResultObj.BlogID);
            _mockBlogRepository.Verify(repo => repo.GetByIdAsync(blogId), Times.Once);
        }

        [Fact]
        public async Task GetBlogByIdAsync_ShouldReturnNull_WhenBlogDoesNotExist()
        {
            // Arrange
            var blogId = 1;

            _mockBlogRepository.Setup(repo => repo.GetByIdAsync(blogId))
                .ReturnsAsync((Blog)null); // Blog not found

            // Act
            var result = await _blogService.GetBlogByIdAsync(blogId);

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.Null(result.ResultObj);
            _mockBlogRepository.Verify(repo => repo.GetByIdAsync(blogId), Times.Once);
        }

    }
}
