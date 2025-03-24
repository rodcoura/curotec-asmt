using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Asmt.Main.Common;

/// <summary>
/// Authorization class for generating and validating tokens.
/// </summary>
public static class Authorization
{
    /// <summary>
    /// Gets the user ID from the claims principal.
    /// </summary>
    /// <param name="user">The user to get the ID from.</param>
    /// <returns>The user ID.</returns> 
    public static int GetUserId(ClaimsPrincipal user) => 
        int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new AsmtException("User ID not found", AsmtExceptionType.Unauthorized));

    /// <summary>
    /// Generates a token for the user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    public static void GenerateToken(UserDto user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(AsmtOpts.Authentication.Secret);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Audience = AsmtOpts.Authentication.Audience,
            Issuer = AsmtOpts.Authentication.Issuer,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        //Pointer to the token
        user.Token = tokenHandler.WriteToken(token);
    }
}
