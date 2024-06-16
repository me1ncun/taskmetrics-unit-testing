using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.GetUserList;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.Get;

public class User_BadRequest
{
    private UserController _controller;

    [Fact]
    public async void Get_ShouldReturnBadRequest()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);

        // Arrange
        var request = new GetUserRequest()
        {
            Id = 1
        };

        // Act
        _userServiceMock.Setup(services => services.GetAsync(request.Id)).ReturnsAsync((GetUserResponse)null);

        // Assert
        var result = await _controller.Details(request.Id);
        
        var notFoundResult = result as NotFoundResult;
        Assert.AreEqual(404, notFoundResult.StatusCode);
    }
}