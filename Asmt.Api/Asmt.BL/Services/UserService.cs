using System;
using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using Asmt.BL.Interfaces;
using Asmt.DAL.Interfaces;
using Asmt.DAL.Models;

namespace Asmt.BL.Services;

/// <summary>
/// User service.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor for UserService.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <inheritdoc/>
    public async Task<UserDto> GetUserByEmailAndPasword(string email, string password, CancellationToken cancellationToken = default)
    {
        //TODO: Hash password
        IEnumerable<UserDto> result = await _userRepository
            .GetByAsync<UserDto>(p => new UserDto {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Role = p.Role,
            }, u => u.Email == email && u.Password == password, cancellationToken: cancellationToken);

        return result.FirstOrDefault() ?? throw new AsmtException("User or password are invalid", AsmtExceptionType.NotFound);
    }
}
