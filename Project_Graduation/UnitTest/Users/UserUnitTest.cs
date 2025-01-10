using Domain.Models.Dto.Role;
using Domain.Models.Page;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Users
{
    namespace UnitTest.Users
    {
        public class UserUnitTest
        {
            private readonly Mock<IConfiguration> _configurationMock;
            private readonly Mock<Project_Graduation_Context> _contextMock;
            private readonly Mock<UserManager<User>> _userManagerMock;
            private readonly UserService _userService;

            public UserUnitTest()
            {
                _configurationMock = new Mock<IConfiguration>();
                _contextMock = new Mock<Project_Graduation_Context>();

                var userStoreMock = new Mock<IUserStore<User>>();
                _userManagerMock = new Mock<UserManager<User>>(
                    userStoreMock.Object, null, null, null, null, null, null, null, null);

                _userService = new UserService(
                    _configurationMock.Object,
                    _contextMock.Object,
                    _userManagerMock.Object);
            }

            [Fact]
            public async Task Delete_ShouldReturnTrue_WhenUserIsDeletedSuccessfully()
            {
                // Arrange
                var userId = Guid.NewGuid();
                var user = new User { Id = userId };

                _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                    .ReturnsAsync(user);
                _userManagerMock.Setup(um => um.DeleteAsync(user))
                    .ReturnsAsync(IdentityResult.Success);

                // Act
                var result = await _userService.Delete(userId);

                // Assert
                Assert.True(result);
                _userManagerMock.Verify(um => um.DeleteAsync(user), Times.Once);
            }

            [Fact]
            public async Task Delete_ShouldReturnFalse_WhenUserDoesNotExist()
            {
                // Arrange
                var userId = Guid.NewGuid();

                _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                    .ReturnsAsync((User)null);

                // Act
                var result = await _userService.Delete(userId);

                // Assert
                Assert.False(result);
                _userManagerMock.Verify(um => um.DeleteAsync(It.IsAny<User>()), Times.Never);
            }

            [Fact]
            public async Task GetById_ShouldReturnUser_WhenUserExists()
            {
                // Arrange
                var userId = Guid.NewGuid();
                var user = new User
                {
                    Id = userId,
                    FullName = "Test User",
                    Email = "test@example.com",
                    PhoneNumber = "123456789",
                    UserName = "testuser",
                    Dob = DateTime.Now.AddYears(-25),
                    Gender = true,
                    CCCD = "1234567890",
                    RestaurantID = 1
                };

                var roles = new List<string> { "Customer" };

                _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                    .ReturnsAsync(user);
                _userManagerMock.Setup(um => um.GetRolesAsync(user))
                    .ReturnsAsync(roles);

                // Act
                var result = await _userService.GetById(userId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(userId, result.Id);
                Assert.Contains("Customer", result.Roles);
            }

            [Fact]
            public async Task GetById_ShouldReturnEmptyDto_WhenUserDoesNotExist()
            {
                // Arrange
                var userId = Guid.NewGuid();

                _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                    .ReturnsAsync((User)null);

                // Act
                var result = await _userService.GetById(userId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(Guid.Empty, result.Id);
                Assert.Empty(result.Roles);
            }

            [Fact]
            public async Task RoleAssign_ShouldReturnTrue_WhenRolesAreAssignedSuccessfully()
            {
                // Arrange
                var userId = Guid.NewGuid();
                var user = new User { Id = userId };
                var roles = new List<SelectItem>
        {
            new SelectItem { Name = "Manager", Selected = true },
            new SelectItem { Name = "Waiter", Selected = false }
        };

                var request = new RoleAssignRequestDto { Roles = roles };

                _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                    .ReturnsAsync(user);
                _userManagerMock.Setup(um => um.IsInRoleAsync(user, "Manager"))
                    .ReturnsAsync(false);
                _userManagerMock.Setup(um => um.AddToRoleAsync(user, "Manager"))
                    .ReturnsAsync(IdentityResult.Success);

                _userManagerMock.Setup(um => um.IsInRoleAsync(user, "Waiter"))
                    .ReturnsAsync(true);
                _userManagerMock.Setup(um => um.RemoveFromRoleAsync(user, "Waiter"))
                    .ReturnsAsync(IdentityResult.Success);

                // Act
                var result = await _userService.RoleAssign(userId, request);

                // Assert
                Assert.True(result);
                _userManagerMock.Verify(um => um.AddToRoleAsync(user, "Manager"), Times.Once);
                _userManagerMock.Verify(um => um.RemoveFromRoleAsync(user, "Waiter"), Times.Once);
            }

            [Fact]
            public async Task RoleAssign_ShouldReturnFalse_WhenUserDoesNotExist()
            {
                // Arrange
                var userId = Guid.NewGuid();
                var request = new RoleAssignRequestDto();

                _userManagerMock.Setup(um => um.FindByIdAsync(userId.ToString()))
                    .ReturnsAsync((User)null);

                // Act
                var result = await _userService.RoleAssign(userId, request);

                // Assert
                Assert.False(result);
            }
        }
    }
}
