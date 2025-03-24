using Asmt.DAL.Models;
using Asmt.DAL.Repositories;
using Asmt.Test.DAL.Fixtures;

namespace Asmt.Test.DAL;

[TestFixture]
public class UserRepositoryTests : DBContextFixture
{
    private UserRepository _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new UserRepository(_context);
    }

    [Test]
    public async Task AddAsync_ValidUser_ReturnsUserWithId()
    {
        // Arrange
        User user = new()
        {
            Name = "Test User",
            Email = "test@example.com",
            Password = "password123",
            Role = "User",
            CreateDT = DateTime.UtcNow
        };

        // Act
        User result = await _repository.AddAsync(user);

        // Assert
        Assert.That(result.Id, Is.GreaterThan(0));
        Assert.That(result.Name, Is.EqualTo("Test User"));
    }

    [Test]
    public async Task GetByIdAsync_ExistingUser_ReturnsCorrectUser()
    {
        // Arrange
        User user = new()
        {
            Name = "Test User",
            Email = "test@example.com",
            Password = "password123",
            Role = "User",
            CreateDT = DateTime.UtcNow
        };
        await _repository.AddAsync(user);

        // Act
        User result = await _repository.GetByIdAsync(user.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(user.Id));
        Assert.That(result.Email, Is.EqualTo(user.Email));
    }

    [Test]
    public async Task GetByAsync_WithFilter_ReturnsFilteredUsers()
    {
        // Arrange
        User[] users = new[]
        {
            new User { Name = "User1", Email = "user1@example.com", Password = "pass1", Role = "User", CreateDT = DateTime.UtcNow },
            new User { Name = "User2", Email = "user2@example.com", Password = "pass2", Role = "Admin", CreateDT = DateTime.UtcNow }
        };

        foreach (User? user in users)
        {
            await _repository.AddAsync(user);
        }

        // Act
        var result = await _repository.GetByAsync(
            u => new { u.Id, u.Email },
            u => u.Role == "Admin"
        );

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Email, Is.EqualTo("user2@example.com"));
    }

    [Test]
    public async Task UpdateAsync_ExistingUser_UpdatesUserSuccessfully()
    {
        // Arrange
        User user = new()
        {
            Name = "Test User",
            Email = "test@example.com",
            Password = "password123",
            Role = "User",
            CreateDT = DateTime.UtcNow
        };
        await _repository.AddAsync(user);

        // Act
        user.Name = "Updated Name";
        User result = await _repository.UpdateAsync(user);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
        Assert.That(result.UpdateDT, Is.Not.Null);
    }

    [Test]
    public async Task DeleteAsync_ExistingUser_ReturnsTrue()
    {
        // Arrange
        User user = new()
        {
            Name = "Test User",
            Email = "test@example.com",
            Password = "password123",
            Role = "User",
            CreateDT = DateTime.UtcNow
        };
        await _repository.AddAsync(user);

        // Act
        bool result = await _repository.DeleteAsync(user.Id);

        // Assert
        Assert.That(result, Is.True);
        User deletedUser = await _repository.GetByIdAsync(user.Id);
        Assert.That(deletedUser, Is.Null);
    }
}
