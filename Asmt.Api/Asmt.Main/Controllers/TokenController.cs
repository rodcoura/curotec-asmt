using Asmt.BL.DTOs;
using Asmt.BL.Exceptions;
using Asmt.BL.Interfaces;
using Asmt.Main.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asmt.Main.Controllers
{
    /// <summary>
    /// Controller for managing tokens.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : AsmtControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for the TokenController.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="logger">The logger.</param>    
        public TokenController(IUserService userService, ILogger<OrdersController> logger) : base(logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Get a token.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The token.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTokenAsync([FromBody] SignInRequest credentials, CancellationToken cancellationToken)
        {
            try
            {
                UserDto user = await _userService.GetUserByEmailAndPasword(credentials.Email, credentials.Password, cancellationToken);
                Authorization.GenerateToken(user);
                return Ok(user);
            }
            catch (AsmtException ex)
            {
                return HandleAsmtException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all orders");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Sign In request information
        /// </summary>
        /// <param name="Email">User Email</param>
        /// <param name="Password">User Password</param>
        public record SignInRequest(string Email, string Password);
    }
}
