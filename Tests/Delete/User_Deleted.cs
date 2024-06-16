using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.Domain;
using task_api.TaskMetrics.API.DTOs.Users.DeleteUser;
using task_api.TaskMetrics.API.DTOs.Users.GetUserList;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.Create;

public class User_Deleted
{
    private UserController _controller;

    [Fact]
    public async void Delete_ShouldReturnCorrectResponse()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        
        // Arrange
        var request = new DeleteUserRequest()
        {
            Id = 1
        };
        var response = new DeleteUserResponse();

        _userServiceMock.Setup(service => service.DeleteAsync(request.Id)).ReturnsAsync(response);
        _userServiceMock.Setup(service => service.GetAsync(request.Id)).ReturnsAsync(new GetUserResponse());

        // Act
        var result = await _controller.Delete(request.Id);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(response, okResult.Value);
    }
}