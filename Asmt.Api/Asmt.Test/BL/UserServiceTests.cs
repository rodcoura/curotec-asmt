using Moq;
using Asmt.BL.Services;
using Asmt.DAL.Interfaces;
using Asmt.DAL.Models;
using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using System.Linq.Expressions;

namespace Asmt.Test.BL;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Test]
    public async Task GetUserByEmailAndPassword_ValidCredentials_ReturnsUserDto()
    {
        // Arrange
        CancellationToken cancellationToken = default;
        string email = "test@example.com";
        string password = "password123";
        User user = new()
        {
            Id = 1,
            Name = "Test User",
            Email = email,
            Password = password,
            Role = "User"
        };

        _userRepositoryMock.Setup(x => x.GetByAsync<UserDto>(
            It.IsAny<Expression<Func<User, UserDto>>>(),
            It.IsAny<Expression<Func<User, bool>>>(),
            null, null,
            cancellationToken))
            .ReturnsAsync(new List<UserDto>
            {
                new() {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role
                }
            });

        // Act
        UserDto result = await _userService.GetUserByEmailAndPasword(email, password);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(user.Id));
            Assert.That(result.Name, Is.EqualTo(user.Name));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.Role, Is.EqualTo(user.Role));
        });
    }

    [Test]
    public async Task GetUserByEmailAndPassword_InvalidCredentials_ThrowsAsmtException()
    {
        // Arrange
        CancellationToken cancellationToken = default;
        string email = "test@example.com";
        string password = "wrongpassword";

        _userRepositoryMock.Setup(x => x.GetByAsync<UserDto>(
            It.IsAny<Expression<Func<User, UserDto>>>(),
            It.IsAny<Expression<Func<User, bool>>>(),
            null, null,
            cancellationToken))
            .ReturnsAsync(new List<UserDto>());

        // Act & Assert
        AsmtException? exception = Assert.ThrowsAsync<AsmtException>(async () =>
            await _userService.GetUserByEmailAndPasword(email, password));
        
        Assert.Multiple(() =>
        {
            Assert.That(exception.Message, Is.EqualTo("User or password are invalid"));
            Assert.That(exception.ExceptionType, Is.EqualTo(AsmtExceptionType.NotFound));
        });
    }

    [Test]
    public void Constructor_NullRepository_ThrowsArgumentNullException()
    {
        // Act & Assert
        ArgumentNullException? exception = Assert.Throws<ArgumentNullException>(() => new UserService(null));
        Assert.That(exception.ParamName, Is.EqualTo("userRepository"));
    }
}
