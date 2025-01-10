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
    public class Edit
    {
        private readonly Mock<RoleManager<Role>> _mockRoleManager;
        private readonly RoleService _roleService;

        public Edit()
        {
            var store = new Mock<IRoleStore<Role>>();
            _mockRoleManager = new Mock<RoleManager<Role>>(store.Object, null, null, null, null);
            _roleService = new RoleService(_mockRoleManager.Object);
        }

        [Fact]
        public async Task Edit_ShouldReturnTrue_WhenRoleIsUpdatedSuccessfully()
        {
            // Arrange
            var request = new RoleRequestDto
            {
                Id = Guid.NewGuid(),
                Name = "UpdatedRole",
                Description = "Updated Description"
            };

            var role = new Role
            {
                Id = request.Id,
                Name = "OriginalRole",
                Description = "Original Description"
            };

            _mockRoleManager.Setup(rm => rm.FindByIdAsync(request.Id.ToString()))
                .ReturnsAsync(role);

            _mockRoleManager.Setup(rm => rm.UpdateAsync(It.IsAny<Role>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _roleService.Edit(request);

            // Assert
            Assert.True(result);
            Assert.Equal(request.Name, role.Name);
            Assert.Equal(request.Description, role.Description);
            _mockRoleManager.Verify(rm => rm.UpdateAsync(role), Times.Once);
        }

        [Fact]
        public async Task Edit_ShouldReturnFalse_WhenRoleDoesNotExist()
        {
            // Arrange
            var request = new RoleRequestDto
            {
                Id = Guid.NewGuid(),
                Name = "NonexistentRole",
                Description = "Nonexistent Description"
            };

            _mockRoleManager.Setup(rm => rm.FindByIdAsync(request.Id.ToString()))
                .ReturnsAsync((Role)null);

            // Act
            var result = await _roleService.Edit(request);

            // Assert
            Assert.False(result);
            _mockRoleManager.Verify(rm => rm.UpdateAsync(It.IsAny<Role>()), Times.Never);
        }

    }
}
