using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.GetUserList;
using task_api.TaskMetrics.API.DTOs.Users.UpdateUser;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.Update;

public class User_BadRequest
{
    private UserController _controller;

    [Fact]
    public async void Update_ShouldReturnBadRequest()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        
        // Arrange
        var request = new UpdateUserRequest()
        {
            Name = "John Doe",
            Email = "johndoe12@gmail.com",
            Password = "supersecretpassword"
        };
        
        _userServiceMock.Setup(service => service.GetAsync(request.Email)).ReturnsAsync((GetUserResponse)null);
        
        // Act
        var result = await _controller.Update(request);
        
        // Assert
        var notFoundResult = result as NotFoundResult;
        Assert.AreEqual(404, notFoundResult.StatusCode);
    }
}