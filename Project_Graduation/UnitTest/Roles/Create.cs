using Domain.Models.Dto.Role;
using Infrastructure.Entities;
using Infrastructure.Services.RoleService;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Roles
{
    public class Create
    {
        private readonly Mock<RoleManager<AppRole>> _mockRoleManager;
        private readonly RoleService _serviceRole;

        public Create()
        {
            var store = new Mock<IRoleStore<AppRole>>();
            _mockRoleManager = new Mock<RoleManager<AppRole>>(store.Object, null, null, null, null);
            _serviceRole = new RoleService(_mockRoleManager.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnTrue_WhenRoleIsCreatedSuccessfully()
        {
            // Arrange
            var request = new RoleRequestDto { Name = "NewRole" };

            _mockRoleManager.Setup(rm => rm.FindByNameAsync(request.Name))
                .ReturnsAsync((AppRole)null);

            _mockRoleManager.Setup(rm => rm.CreateAsync(It.IsAny<AppRole>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _serviceRole.Create(request);

            // Assert
            Assert.True(result);
            _mockRoleManager.Verify(rm => rm.CreateAsync(It.IsAny<AppRole>()), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnFalse_WhenRoleAlreadyExists()
        {
            // Arrange
            var request = new RoleRequestDto { Name = "ExistingRole" };
            var role = new AppRole { Name = "ExistingRole" };

            _mockRoleManager.Setup(rm => rm.FindByNameAsync(request.Name))
                .ReturnsAsync(role);

            // Act
            var result = await _serviceRole.Create(request);

            // Assert
            Assert.False(result);
            _mockRoleManager.Verify(rm => rm.CreateAsync(It.IsAny<AppRole>()), Times.Never);
        }

    }
}
