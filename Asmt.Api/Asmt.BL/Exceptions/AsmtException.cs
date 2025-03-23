using System;

namespace Asmt.BL.Exceptions;

/// <summary>
/// The type of exception.
/// </summary>
public enum AsmtExceptionType
{
    /// <summary>
    /// The exception type for a not found exception.
    /// </summary>
    NotFound,

    /// <summary>
    /// The exception type for a validation error.
    /// </summary>  
    ValidationError,

    /// <summary>
    /// The exception type for an internal server error.
    /// </summary>
    InternalServerError
}

/// <summary>
/// Custom exception class for Asmt.
/// </summary>
public class AsmtException : ApplicationException
{
    /// <summary>
    /// The type of exception.
    /// </summary>
    public AsmtExceptionType ExceptionType { get; set; }

    /// <summary>
    /// Constructor for AsmtException.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    /// <param name="exceptionType">The type of exception.</param>
    public AsmtException(string message, AsmtExceptionType exceptionType) : base(message)
    {
        ExceptionType = exceptionType;
    }

    /// <summary>
    /// Constructor for AsmtException.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    /// <param name="exceptionType">The type of exception.</param>
    /// <param name="innerException">The inner exception.</param>
    public AsmtException(string message, AsmtExceptionType exceptionType, Exception innerException) : base(message, innerException)
    {
        ExceptionType = exceptionType;
    }
}
