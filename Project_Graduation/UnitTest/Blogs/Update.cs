using AutoMapper;
using Domain.Models.Dto.Blog;
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
    public class Update
    {
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BlogService _blogService;
        public Update()
        {
            _mockBlogRepository = new Mock<IBlogRepository>();
            _mockMapper = new Mock<IMapper>();
            _blogService = new BlogService(
                _mockBlogRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task UpdateBlogAsync_ShouldReturnSuccess_WhenBlogIsUpdated()
        {
            // Arrange
            var blogDto = new BlogDto
            {
                BlogID = 1,
                Title = "Updated Blog",
                Image = "updated_image.jpg",
                SubTitle = "Updated Subtitle",
                Description = "Updated Description",
                Status = true
            };

            var blog = new Blog
            {
                BlogID = 1,
                Title = "Original Blog",
                Image = "original_image.jpg",
                SubTitle = "Original Subtitle",
                Description = "Original Description",
                Status = false
            };

            _mockBlogRepository.Setup(repo => repo.GetByIdAsync(blogDto.BlogID))
                .ReturnsAsync(blog);

            _mockMapper.Setup(mapper => mapper.Map(blogDto, blog));

            // Act
            var result = await _blogService.UpdateBlogAsync(blogDto);

            // Assert
            Assert.True(result.IsSuccessed);
            _mockBlogRepository.Verify(repo => repo.Update(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBlogAsync_ShouldThrowError_WhenBlogNotFound()
        {
            // Arrange
            var blogDto = new BlogDto
            {
                BlogID = 1,
                Title = "Nonexistent Blog",
                Image = "image.jpg",
                SubTitle = "Subtitle",
                Description = "Description",
                Status = true
            };

            _mockBlogRepository.Setup(repo => repo.GetByIdAsync(blogDto.BlogID))
                .ReturnsAsync((Blog)null); // Blog not found

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _blogService.UpdateBlogAsync(blogDto));
            _mockBlogRepository.Verify(repo => repo.Update(It.IsAny<Blog>()), Times.Never);
        }

    }
}
