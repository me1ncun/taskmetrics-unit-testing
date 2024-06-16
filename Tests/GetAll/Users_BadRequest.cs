using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.GetUserList;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.GetAll;

public class Users_BadRequest
{
    private UserController _controller;

    [Fact]
    public async void GetAll_ShouldReturnBadRequest()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        
        // Arrange
        var users = new List<GetUserResponse>();

        _userServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync((List<GetUserResponse>)null);

        // Act
        var result = await _controller.Get();

        // Assert
        var notFoundResult = result as NotFoundResult;
        Assert.AreEqual(404, notFoundResult.StatusCode);
    }
}