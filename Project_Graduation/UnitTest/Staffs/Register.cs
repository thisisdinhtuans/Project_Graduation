using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Infrastructure.Services.StaffService;
using Domain.Models.Dto.Staff;
using System;
using System.Threading.Tasks;

public class Register
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly StaffService _staffService;

    public Register()
    {
        var userStoreMock = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);
        _staffService = new StaffService(null, _userManagerMock.Object);
    }

    [Fact]
    public async Task Register_ShouldReturnTrue_WhenStaffIsRegisteredSuccessfully()
    {
        // Arrange
        var staffCreateDto = new StaffCreateDto
        {
            Email = "newuser@example.com",
            UserName = "newuser",
            FullName = "New User",
            PassWord = "password123",
            Dob = DateTime.Now.AddYears(-25),
            PhoneNumber = "1234567890",
            Role = "Waiter",
            Gender = true,
            RestaurantID = 1
        };

        _userManagerMock.Setup(um => um.Users)
            .Returns(new List<User>().AsQueryable()); // No users with the email
        _userManagerMock.Setup(um => um.FindByNameAsync(staffCreateDto.UserName))
            .ReturnsAsync((User)null); // No users with the username
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), staffCreateDto.PassWord))
            .ReturnsAsync(IdentityResult.Success); // User creation successful
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), staffCreateDto.Role))
            .ReturnsAsync(IdentityResult.Success); // Role assignment successful

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.True(result);
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), staffCreateDto.PassWord), Times.Once);
        _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), staffCreateDto.Role), Times.Once);
    }

    [Fact]
    public async Task Register_ShouldReturnFalse_WhenEmailAlreadyExists()
    {
        // Arrange
        var staffCreateDto = new StaffCreateDto
        {
            Email = "existinguser@example.com",
            UserName = "newuser",
            FullName = "New User",
            PassWord = "password123",
            Dob = DateTime.Now.AddYears(-25),
            PhoneNumber = "1234567890",
            Role = "Waiter",
            Gender = true,
            RestaurantID = 1
        };

        _userManagerMock.Setup(um => um.Users)
            .Returns(new List<User>
            {
                new User { Email = staffCreateDto.Email }
            }.AsQueryable()); // User with email already exists

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), staffCreateDto.PassWord), Times.Never);
    }

    [Fact]
    public async Task Register_ShouldReturnFalse_WhenUserNameAlreadyExists()
    {
        // Arrange
        var staffCreateDto = new StaffCreateDto
        {
            Email = "newuser@example.com",
            UserName = "existinguser",
            FullName = "New User",
            PassWord = "password123",
            Dob = DateTime.Now.AddYears(-25),
            PhoneNumber = "1234567890",
            Role = "Waiter",
            Gender = true,
            RestaurantID = 1
        };

        _userManagerMock.Setup(um => um.FindByNameAsync(staffCreateDto.UserName))
            .ReturnsAsync(new User { UserName = staffCreateDto.UserName }); // Username already exists

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), staffCreateDto.PassWord), Times.Never);
    }

    [Fact]
    public async Task Register_ShouldReturnFalse_WhenRoleAssignmentFails()
    {
        // Arrange
        var staffCreateDto = new StaffCreateDto
        {
            Email = "newuser@example.com",
            UserName = "newuser",
            FullName = "New User",
            PassWord = "password123",
            Dob = DateTime.Now.AddYears(-25),
            PhoneNumber = "1234567890",
            Role = "Waiter",
            Gender = true,
            RestaurantID = 1
        };

        _userManagerMock.Setup(um => um.Users)
            .Returns(new List<User>().AsQueryable()); // No users with the email
        _userManagerMock.Setup(um => um.FindByNameAsync(staffCreateDto.UserName))
            .ReturnsAsync((User)null); // No users with the username
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), staffCreateDto.PassWord))
            .ReturnsAsync(IdentityResult.Success); // User creation successful
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), staffCreateDto.Role))
            .ReturnsAsync(IdentityResult.Failed()); // Role assignment fails

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), staffCreateDto.Role), Times.Once);
    }
}
