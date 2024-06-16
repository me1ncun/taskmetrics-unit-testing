using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.GetUserList;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.Get;

public class User_Get
{
    private UserController _controller;

    [Fact]
    public async void Get_ShouldReturnCorrectResponse()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);

        // Arrange
        var request = new GetUserRequest()
        {
            Id = 1
        };

        // Act
        _userServiceMock.Setup(services => services.GetAsync(request.Id)).ReturnsAsync(new GetUserResponse());

        // Assert
        var result = await _controller.Details(request.Id);
        
        var okResult = result as OkObjectResult;
        Assert.AreEqual(200, okResult.StatusCode);
    }
}