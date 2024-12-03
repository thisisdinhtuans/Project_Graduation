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
    public class Remove
    {
        private readonly Mock<RoleManager<AppRole>> _mockRoleManager;
        private readonly RoleService _serviceRole;

        public Remove()
        {
            var store = new Mock<IRoleStore<AppRole>>();
            _mockRoleManager = new Mock<RoleManager<AppRole>>(store.Object, null, null, null, null);
            _serviceRole = new RoleService(_mockRoleManager.Object);
        }

        [Fact]
        public async Task Remove_ShouldReturnTrue_WhenRoleIsDeletedSuccessfully()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var role = new AppRole { Id = roleId, Name = "RoleToDelete" };

            _mockRoleManager.Setup(rm => rm.FindByIdAsync(roleId.ToString()))
                .ReturnsAsync(role);

            _mockRoleManager.Setup(rm => rm.DeleteAsync(role))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _serviceRole.Remove(roleId.ToString());

            // Assert
            Assert.True(result);
            _mockRoleManager.Verify(rm => rm.DeleteAsync(role), Times.Once);
        }

        [Fact]
        public async Task Remove_ShouldReturnFalse_WhenRoleDoesNotExist()
        {
            // Arrange
            var roleId = Guid.NewGuid();

            _mockRoleManager.Setup(rm => rm.FindByIdAsync(roleId.ToString()))
                .ReturnsAsync((AppRole)null);

            // Act
            var result = await _serviceRole.Remove(roleId.ToString());

            // Assert
            Assert.False(result);
            _mockRoleManager.Verify(rm => rm.DeleteAsync(It.IsAny<AppRole>()), Times.Never);
        }

    }
}
