using Asmt.BL.DTOs;

namespace Asmt.BL.Interfaces;

/// <summary>
/// Defines the contract for user service operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Get a user by email and password.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user.</returns>
    Task<UserDto> GetUserByEmailAndPasword(string email, string password, CancellationToken cancellationToken = default);
}
