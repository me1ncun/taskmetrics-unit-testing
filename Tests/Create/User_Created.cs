using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.AddUser;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.Create;

public class User_Created
{
    private UserController _controller;

    [Fact]
    public async void Add_ShouldReturnCorrectResponse()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        
        // Arrange
        var request = new AddUserRequest()
        {
            Name = "John Doe",
            Email = "john.doe@example.com"
        };
        var response = new AddUserResponse();

        _userServiceMock.Setup(service => service.AddAsync(request)).ReturnsAsync(response);

        // Act
        var result = await _controller.Add(request) as OkObjectResult;

        // Assert
        var returnedUser = result.Value as AddUserResponse;
        Assert.IsNotNull(returnedUser);
        
        Assert.AreEqual(response.Name, returnedUser.Name);
        Assert.AreEqual(response.Email, returnedUser.Email);
    }
}