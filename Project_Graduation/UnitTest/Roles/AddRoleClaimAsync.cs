using Infrastructure.Entities;
using Infrastructure.Services.RoleService;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Roles
{
    public class AddRoleClaimAsync
    {
        private readonly Mock<RoleManager<Role>> _mockRoleManager;
        private readonly RoleService _serviceRole;

        public AddRoleClaimAsync()
        {
            var store = new Mock<IRoleStore<Role>>();
            _mockRoleManager = new Mock<RoleManager<Role>>(store.Object, null, null, null, null);
            _serviceRole = new RoleService(_mockRoleManager.Object);
        }
        [Fact]
        public async Task AddRoleClaimAsync_ShouldReturnTrue_WhenClaimIsAddedSuccessfully()
        {
            // Arrange
            var roleName = "Admin";
            var claim = new Claim("Permission", "CanEdit");

            var role = new Role { Name = roleName };

            _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName))
                .ReturnsAsync(role);

            _mockRoleManager.Setup(rm => rm.AddClaimAsync(role, claim))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _serviceRole.AddRoleClaimAsync(roleName, claim);

            // Assert
            Assert.True(result);
            _mockRoleManager.Verify(rm => rm.AddClaimAsync(role, claim), Times.Once);
        }

        [Fact]
        public async Task AddRoleClaimAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
        {
            // Arrange
            var roleName = "NonexistentRole";
            var claim = new Claim("Permission", "CanEdit");

            _mockRoleManager.Setup(rm => rm.FindByNameAsync(roleName))
                .ReturnsAsync((Role)null);

            // Act
            var result = await _serviceRole.AddRoleClaimAsync(roleName, claim);

            // Assert
            Assert.False(result);
            _mockRoleManager.Verify(rm => rm.AddClaimAsync(It.IsAny<Role>(), claim), Times.Never);
        }
    }
}
