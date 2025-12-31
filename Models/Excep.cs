using Bikiran.Utils.ApiResp;
using System;
using System.Collections.Generic;

namespace Bikiran.Utils.Models
{
    /// <summary>
    /// Utility class for creating exceptions with additional metadata.
    /// </summary>
    public class Excep
    {
        /// <summary>
        /// Creates a new <see cref="Exception"/> with the specified message and optional reference.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="reference">Optional reference information to provide additional context for debugging. Defaults to an empty string.</param>
        /// <returns>A new <see cref="Exception"/> instance with the specified message and reference data.</returns>
        /// <example>
        /// <code>
        /// var exception = Excep.Create("User not found", "UserService.GetUser");
        /// throw exception;
        /// </code>
        /// </example>
        public static Exception Create(string message, string reference = "")
        {
            var exception = new Exception(message);
            exception.Data["Reference"] = reference;
            return exception;
        }

        /// <summary>
        /// Creates a new <see cref="Exception"/> with the specified message and a collection of field-specific errors.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errors">Optional collection of <see cref="ApiErrorV3"/> objects containing field-level error details. Defaults to <c>null</c>.</param>
        /// <returns>A new <see cref="Exception"/> instance with the specified message and error collection stored in exception data.</returns>
        /// <remarks>
        /// This method is designed to work with <see cref="ApiResponseV3{T}"/> for scenarios requiring detailed
        /// field-level error information. The error collection is stored in the exception's Data dictionary
        /// with the key "Reference" for consistent error handling and logging.
        /// </remarks>
        /// <example>
        /// <code>
        /// var errors = new List&lt;ApiErrorV3&gt;
        /// {
        ///     new ApiErrorV3 { Field = "Email", Message = "Invalid email format" },
        ///     new ApiErrorV3 { Field = "Password", Message = "Password must be at least 8 characters" }
        /// };
        /// 
        /// var exception = Excep.CreateV3("Validation failed", errors);
        /// throw exception;
        /// </code>
        /// </example>
        public static Exception CreateV3(string message, List<ApiErrorV3> errors = null)
        {
            var exception = new Exception(message);
            exception.Data["Errors"] = errors;
            return exception;
        }
    }
}
