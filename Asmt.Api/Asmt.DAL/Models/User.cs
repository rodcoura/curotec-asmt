using System;
using Asmt.DAL.Interfaces;

namespace Asmt.DAL.Models;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User : IAtom
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the user account.
        /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets the role of the user in the system.
    /// </summary>
    public required string Role { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was created.
    /// </summary>
    public DateTime CreateDT { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was last updated.
    /// </summary>
    public DateTime? UpdateDT { get; set; }
}
