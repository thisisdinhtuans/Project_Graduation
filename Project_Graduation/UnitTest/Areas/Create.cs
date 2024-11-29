using AutoMapper;
using Domain.Models.Dto.Area;
using Infrastructure.Entities;
using Infrastructure.Repositories.AreaRepository;
using Infrastructure.Services.AreaService;
using Moq;

namespace UnitTest.Areas
{
    public class Create
    {

        private readonly Mock<IAreaRepository> _areaRepositoryMock;
        private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AreaService _areaService;

        public Create()
        {
            _areaRepositoryMock = new Mock<IAreaRepository>();
            _restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            _mapperMock = new Mock<IMapper>();
            _areaService = new AreaService(
                _areaRepositoryMock.Object,
                _restaurantRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CreateAreaAsync_ShouldReturnError_WhenRestaurantDoesNotExist()
        {
            // Arrange
            var areaDto = new CreateAreaDto { RestaurantID = 1, AreaName = "Test Area" };
            _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Restaurant)null);

            // Act
            var result = await _areaService.CreateAreaAsync(areaDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Nhà hàng này không tồn tại", result.Message);
        }

        [Fact]
        public async Task CreateAreaAsync_ShouldReturnError_WhenAreaAlreadyExists()
        {
            // Arrange
            var areaDto = new CreateAreaDto { RestaurantID = 1, AreaName = "Test Area" };
            _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Restaurant());
            _areaRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Area, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _areaService.CreateAreaAsync(areaDto);

            // Assert
            Assert.False(result.IsSuccessed);
            Assert.Equal("Khu vực  này đã tồn tại.", result.Message);
        }

        [Fact]
        public async Task CreateAreaAsync_ShouldReturnSuccess_WhenAreaCreatedSuccessfully()
        {
            // Arrange
            var areaDto = new CreateAreaDto { RestaurantID = 1, AreaName = "New Area" };
            var area = new Area();
            _restaurantRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Restaurant());
            _areaRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Area, bool>>>()))
                .ReturnsAsync(false);
            _mapperMock.Setup(x => x.Map<Area>(areaDto)).Returns(area);

            // Act
            var result = await _areaService.CreateAreaAsync(areaDto);

            // Assert
            Assert.True(result.IsSuccessed);
        }
    }
}
