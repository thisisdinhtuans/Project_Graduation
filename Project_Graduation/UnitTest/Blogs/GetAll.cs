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
    public class GetAll
    {
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BlogService _blogService;
        public GetAll()
        {
            _mockBlogRepository = new Mock<IBlogRepository>();
            _mockMapper = new Mock<IMapper>();
            _blogService = new BlogService(
                _mockBlogRepository.Object,
                _mockMapper.Object);
        }
        [Fact]
        public async Task GetAllBlogsAsync_ShouldReturnAllBlogs()
        {
            // Arrange
            var blogs = new List<Blog>
            {
                new Blog { BlogID = 1, Title = "Blog 1", Image = "image1.jpg", SubTitle = "Subtitle 1", Description = "Description 1", Status = true },
                new Blog { BlogID = 2, Title = "Blog 2", Image = "image2.jpg", SubTitle = "Subtitle 2", Description = "Description 2", Status = false }
            };

            _mockBlogRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(blogs);

            // Act
            var result = await _blogService.GetAllBlogsAsync();

            // Assert
            Assert.True(result.IsSuccessed);
            Assert.NotNull(result.ResultObj);
            Assert.Equal(2, result.ResultObj.Count);
            _mockBlogRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

    }
}
