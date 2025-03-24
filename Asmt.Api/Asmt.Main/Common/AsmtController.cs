using Asmt.BL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Asmt.Main.Common
{
    /// <summary>
    /// Base controller for all Asmt controllers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AsmtControllerBase : ControllerBase
    {
        protected readonly ILogger<AsmtControllerBase> _logger;

        /// <summary>
        /// Constructor for the AsmtControllerBase.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public AsmtControllerBase(ILogger<AsmtControllerBase> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles AsmtExceptions.
        /// TODO: This is a temporary method to handle AsmtExceptions.
        /// TODO: This should be moved to a action filter
        /// </summary>
        /// <param name="ex">The AsmtException to handle.</param>
        /// <returns>The IActionResult to return.</returns>
        protected IActionResult HandleAsmtException(AsmtException ex)
        {
            _logger.LogError(ex, "AsmtException occurred: {Message}", ex.Message);
            
            return ex.ExceptionType switch
            {
                AsmtExceptionType.NotFound => NotFound(ex.Message),
                AsmtExceptionType.ValidationError => BadRequest(ex.InnerException?.Message ?? ex.Message),
                AsmtExceptionType.InternalServerError => StatusCode(500, ex.Message),
                _ => StatusCode(500, "An unexpected error occurred while processing your request.")
            };
        }
    }
}
