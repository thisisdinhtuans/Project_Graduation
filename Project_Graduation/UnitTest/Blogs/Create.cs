using AutoMapper;
using Domain.Models.Dto.Blog;
using Infrastructure.Entities;
using Infrastructure.Repositories.BlogRepository;
using Infrastructure.Services.BlogService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Blogs
{
    public class Create
    {
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BlogService _blogService;
        public Create()
        {
            _mockBlogRepository = new Mock<IBlogRepository>();
            _mockMapper = new Mock<IMapper>();
            _blogService = new BlogService(
                _mockBlogRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task CreateBlogAsync_ShouldReturnSuccess_WhenBlogIsCreated()
        {
            // Arrange
            var createBlogDto = new CreateBlogDto
            {
                Image = "sample.jpg",
                Title = "New Blog",
                SubTitle = "Subtitle for New Blog",
                Description = "Detailed description of the blog",
                Status = true
            };

            var blog = new Blog
            {
                BlogID = 0,
                Image = "sample.jpg",
                Title = "New Blog",
                SubTitle = "Subtitle for New Blog",
                Description = "Detailed description of the blog",
                Status = true
            };

            _mockBlogRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Blog, bool>>>()))
                .ReturnsAsync(false); // Blog title does not exist

            _mockMapper.Setup(mapper => mapper.Map<Blog>(createBlogDto))
                .Returns(blog);

            // Act
            var result = await _blogService.CreateBlogAsync(createBlogDto);

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            _mockBlogRepository.Verify(repo => repo.Add(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public async Task CreateBlogAsync_ShouldReturnError_WhenBlogAlreadyExists()
        {
            // Arrange
            var createBlogDto = new CreateBlogDto
            {
                Image = "existing.jpg",
                Title = "Existing Blog",
                SubTitle = "Subtitle for Existing Blog",
                Description = "Detailed description of the existing blog",
                Status = true
            };

            _mockBlogRepository.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Blog, bool>>>()))
                .ReturnsAsync(true); // Blog title already exists

            // Act
            var result = await _blogService.CreateBlogAsync(createBlogDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Blog với tiêu đề này đã tồn tại.", result.Message);
            _mockBlogRepository.Verify(repo => repo.Add(It.IsAny<Blog>()), Times.Never);
        }


    }
}
