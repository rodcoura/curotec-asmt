using System;

namespace Asmt.BL.DTOs;

/// <summary>
/// User DTO.
/// </summary>
public class UserDto
{   
    /// <summary>
    /// The user ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The user name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The user email.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// The user role.
    /// </summary>
    public required string Role { get; set; }

    /// <summary>
    /// The user token.
    /// </summary>
    public string? Token { get; set; }
}
