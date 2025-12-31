#nullable enable

using System;
using System.Collections.Generic;

namespace Bikiran.Utils.ApiResp
{
    /// <summary>
    /// Represents a field-specific error in API validation or processing
    /// </summary>
    /// <remarks>
    /// This class is used to provide detailed error information for specific fields
    /// in API requests, commonly used in validation scenarios where multiple fields
    /// may have individual errors.
    /// </remarks>
    public class ApiErrorV3
    {
        /// <summary>
        /// Name of the field that contains the error
        /// </summary>
        /// <value>
        /// Field identifier or property name. Default: empty string
        /// </value>
        public string Field { get; set; } = "";

        /// <summary>
        /// Human-readable error message describing the validation or processing error
        /// </summary>
        /// <value>
        /// Error description specific to the field. Default: empty string
        /// </value>
        public string Message { get; set; } = "";
    }

    /// <summary>
    /// Represents a strongly-typed API response format with enhanced error handling capabilities
    /// </summary>
    /// <typeparam name="T">The type of data payload returned in the response</typeparam>
    /// <remarks>
    /// <para>
    /// This is version 3 of the API response model, featuring:
    /// - Generic type support for data payload
    /// - Collection of field-specific errors
    /// - Nullable reference types for improved null safety
    /// - Request correlation tracking via reference name
    /// </para>
    /// <para>
    /// The <see cref="Errors"/> collection allows returning multiple validation errors
    /// in a single response, improving client-side error handling.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Success response with data
    /// var response = new ApiResponseV3&lt;User&gt;
    /// {
    ///     Error = false,
    ///     Message = "User retrieved successfully",
    ///     Data = new User { Id = 1, Name = "John" }
    /// };
    /// 
    /// // Error response with field-specific errors
    /// var errorResponse = new ApiResponseV3&lt;object&gt;
    /// {
    ///     Error = true,
    ///     Message = "Validation failed",
    ///     Errors = new List&lt;ApiErrorV3&gt;
    ///     {
    ///         new() { Field = "Email", Message = "Invalid email format" },
    ///         new() { Field = "Password", Message = "Password too short" }
    ///     }
    /// };
    /// </code>
    /// </example>
    public class ApiResponseV3<T>
    {
        /// <summary>
        /// Indicates whether the request resulted in an error
        /// </summary>
        /// <value>
        /// <c>true</c> if an error occurred during processing; otherwise, <c>false</c>
        /// </value>
        public bool Error { get; set; }

        /// <summary>
        /// Collection of field-specific errors encountered during validation or processing
        /// </summary>
        /// <value>
        /// List of <see cref="ApiErrorV3"/> objects containing field-level error details.
        /// <c>null</c> when no errors exist. Default: <c>null</c>
        /// </value>
        /// <remarks>
        /// Typically populated during validation failures where multiple fields may have errors.
        /// Should be <c>null</c> or empty for successful responses.
        /// </remarks>
        public List<ApiErrorV3>? Errors { get; set; } = null;

        /// <summary>
        /// Human-readable message describing the operation result
        /// </summary>
        /// <value>
        /// Success message or general error description. Default: empty string
        /// </value>
        public string Message { get; set; } = "";

        /// <summary>
        /// Primary data payload of the response
        /// </summary>
        /// <value>
        /// Strongly-typed data object. Can be <c>null</c> for error responses. Default: <c>default(T)</c>
        /// </value>
        /// <remarks>
        /// The generic type parameter <typeparamref name="T"/> allows type-safe data handling
        /// and eliminates the need for casting in client code.
        /// </remarks>
        public T? Data { get; set; } = default;

        /// <summary>
        /// Unique identifier for tracking requests across systems
        /// </summary>
        /// <value>
        /// Correlation ID, transaction reference, or tracing identifier. Default: empty string
        /// </value>
        /// <remarks>
        /// Used for debugging, logging, and distributed tracing scenarios to correlate
        /// requests across multiple services or API calls.
        /// </remarks>
        public string ReferenceName { get; set; } = string.Empty;
    }
}