using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.DeleteUser;
using task_api.TaskMetrics.API.DTOs.Users.GetUserList;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.Delete;

public class User_BadRequest
{
    private UserController _controller;
    
    [Fact]
    public async void Delete_ShouldReturnBadRequest()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        
        // Arrange
        var request = new DeleteUserRequest()
        {
            Id = 1
        };
        
        _userServiceMock.Setup(service => service.GetAsync(request.Id)).ReturnsAsync((GetUserResponse)null);
        
        // Act
        var result = await _controller.Delete(request.Id);
        
        // Assert
        var notFoundResult = result as NotFoundResult;
        Assert.AreEqual(404, notFoundResult.StatusCode);
    }
}