using Asmt.Main.Common;
using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using System.Security.Claims;

namespace Asmt.Test.Common
{
    [TestFixture]
    public class AuthorizationTests
    {
        [Test]
        public void GetUserId_ValidClaims_ReturnsUserId()
        {
            // Arrange
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Test User")
            };
            ClaimsIdentity identity = new(claims);
            ClaimsPrincipal principal = new(identity);

            // Act
            int userId = Authorization.GetUserId(principal);

            // Assert
            Assert.That(userId, Is.EqualTo(1));
        }

        [Test]
        public void GetUserId_MissingClaim_ThrowsAsmtException()
        {
            // Arrange
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, "Test User")
            };
            ClaimsIdentity identity = new(claims);
            ClaimsPrincipal principal = new(identity);

            // Act & Assert
            AsmtException? ex = Assert.Throws<AsmtException>(() => Authorization.GetUserId(principal));
            Assert.That(ex.ExceptionType, Is.EqualTo(AsmtExceptionType.Unauthorized));
        }
    }
} 