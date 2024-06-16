using Microsoft.AspNetCore.Mvc;
using Moq;
using task_api.Controllers;
using task_api.TaskMetrics.API.DTOs.Users.GetUserList;
using task_api.TaskMetrics.API.Services.Interfaces;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace TaskMetrics.Tests.Tests.GetAll;

public class Users_GetAll
{
    private UserController _controller;
    
    [Fact]
    public async void GetAll_ShouldReturnCorrectResponse()
    {
        var _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
        
        // Arrange
        var users = new List<GetUserResponse>
        {
            new GetUserResponse
            {
                Name = "John Doe",
                Email = "johndoe@yahoo.com"
            },
            new GetUserResponse
            {
                Name = "Jane Doe",
                Email = "janedoe@yahoo.com"
            }
        };

        _userServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        var returnUsers = okResult.Value as List<GetUserResponse>;
        Assert.AreEqual(2, returnUsers.Count);
    }
}