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
    public class GetById
    {
        private readonly Mock<RoleManager<Role>> _mockRoleManager;
        private readonly RoleService _serviceRole;

        public GetById()
        {
            var store = new Mock<IRoleStore<Role>>();
            _mockRoleManager = new Mock<RoleManager<Role>>(store.Object, null, null, null, null);
            _serviceRole = new RoleService(_mockRoleManager.Object);
        }
        [Fact]
        public async Task GetById_ShouldReturnRole_WhenRoleExists()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var role = new Role { Id = roleId, Name = "Admin", Description = "Administrator Role" };

            _mockRoleManager.Setup(rm => rm.FindByIdAsync(roleId.ToString()))
                .ReturnsAsync(role);

            // Act
            var result = await _serviceRole.GetById(roleId.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(role.Name, result.Name);
            Assert.Equal(role.Description, result.Description);
            Assert.Equal(role.Id, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenRoleDoesNotExist()
        {
            // Arrange
            var roleId = Guid.NewGuid();

            _mockRoleManager.Setup(rm => rm.FindByIdAsync(roleId.ToString()))
                .ReturnsAsync((Role)null);  // Role doesn't exist

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _serviceRole.GetById(roleId.ToString()));
            Assert.Equal("Role not found", exception.Message);  // Adjusted error message
        }


    }
}
