using System;

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
    }
}
