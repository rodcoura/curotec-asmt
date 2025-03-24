using NUnit.Framework;
using Moq;
using Asmt.Main.Controllers;
using Asmt.BL.Interfaces;
using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asmt.Test.Controllers
{
    [TestFixture]
    public class TokenControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ILogger<OrdersController>> _loggerMock;
        private TokenController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<OrdersController>>();
            _controller = new TokenController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetTokenAsync_ValidCredentials_ReturnsOkWithUser()
        {
            // Arrange
            var credentials = ("test@test.com", "password");
            var userDto = new UserDto { Id = 1, Email = "test@test.com", Name = "Test User", Role = "User" };
            
            _userServiceMock.Setup(x => x.GetUserByEmailAndPasword(
                credentials.Item1, 
                credentials.Item2, 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(userDto);

            // Act
            var result = await _controller.GetTokenAsync(credentials, CancellationToken.None);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(userDto));
        }

        [Test]
        public async Task GetTokenAsync_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var credentials = ("test@test.com", "wrongpassword");
            
            _userServiceMock.Setup(x => x.GetUserByEmailAndPasword(
                credentials.Item1, 
                credentials.Item2, 
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AsmtException("Invalid credentials", AsmtExceptionType.Unauthorized));

            // Act
            var result = await _controller.GetTokenAsync(credentials, CancellationToken.None);

            // Assert
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
        }
    }
}