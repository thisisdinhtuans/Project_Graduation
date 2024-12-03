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
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly StaffService _staffService;

    public Register()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(
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
            .Returns(new List<AppUser>().AsQueryable()); // No users with the email
        _userManagerMock.Setup(um => um.FindByNameAsync(staffCreateDto.UserName))
            .ReturnsAsync((AppUser)null); // No users with the username
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), staffCreateDto.PassWord))
            .ReturnsAsync(IdentityResult.Success); // User creation successful
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), staffCreateDto.Role))
            .ReturnsAsync(IdentityResult.Success); // Role assignment successful

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.True(result);
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<AppUser>(), staffCreateDto.PassWord), Times.Once);
        _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<AppUser>(), staffCreateDto.Role), Times.Once);
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
            .Returns(new List<AppUser>
            {
                new AppUser { Email = staffCreateDto.Email }
            }.AsQueryable()); // User with email already exists

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<AppUser>(), staffCreateDto.PassWord), Times.Never);
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
            .ReturnsAsync(new AppUser { UserName = staffCreateDto.UserName }); // Username already exists

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<AppUser>(), staffCreateDto.PassWord), Times.Never);
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
            .Returns(new List<AppUser>().AsQueryable()); // No users with the email
        _userManagerMock.Setup(um => um.FindByNameAsync(staffCreateDto.UserName))
            .ReturnsAsync((AppUser)null); // No users with the username
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), staffCreateDto.PassWord))
            .ReturnsAsync(IdentityResult.Success); // User creation successful
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), staffCreateDto.Role))
            .ReturnsAsync(IdentityResult.Failed()); // Role assignment fails

        // Act
        var result = await _staffService.Register(staffCreateDto);

        // Assert
        Assert.False(result);
        _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<AppUser>(), staffCreateDto.Role), Times.Once);
    }
}
