using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.AddUser;
using task_api.TaskMetrics.API.Services.Interfaces;
using task_api.TaskMetrics.Domain.Exceptions;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.Create;

public class User_BadRequest
{
    private UserController _controller;

    [Fact]
    public async void Add_ReturnsBadRequest_WhenRequestIsInvalid()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        
        // Arrange
        var request = new AddUserRequest(); // Invalid request
        _userServiceMock.Setup(service => service.AddAsync(request))
            .ThrowsAsync(new ArgumentException("Invalid request"));

        // Act
        var result = await _controller.Add(request);

        // Assert
        var actionResult = result as BadRequestObjectResult;
        Assert.NotNull(actionResult);
        Assert.AreEqual(400, actionResult.StatusCode);
        Assert.AreEqual("Invalid request", actionResult.Value);
    }

    [Fact]
    public async void Add_ShouldReturnBadRequest_WhenUserAlreadyExists()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);

        // Arrange
        var request = new AddUserRequest
        {
            Name = "John Doe",
            Email = "john.doe@example.com"
        };

        _userServiceMock.Setup(service => service.AddAsync(request)).ThrowsAsync(new DublicateUserException());

        // Act
        var result = await _controller.Add(request);
        
        // Assert
        var actionResult = result as BadRequestObjectResult;

        Assert.NotNull(actionResult);
        Assert.AreEqual(400, actionResult.StatusCode);
        Assert.AreEqual("User already exists", actionResult.Value);
    }
}