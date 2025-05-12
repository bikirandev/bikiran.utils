using System;

namespace Bikiran.Utils.ApiResp
{
    /// <summary>
    /// Provides factory methods for creating standardized API responses
    /// </summary>
    /// <remarks>
    /// This handler follows the convention:
    /// - Success responses contain meaningful data payloads
    /// - Error responses contain troubleshooting references
    /// - All factory methods ensure consistent response structure
    /// </remarks>
    public static class ApiResponseHandler
    {
        /// <summary>
        /// Creates a success response without data payload
        /// </summary>
        /// <param name="message">Human-readable success message</param>
        /// <returns>ApiResponse with Error=false and empty Data</returns>
        /// <example>
        /// <code>
        /// return ApiResponseHandler.Success("Operation completed");
        /// </code>
        /// </example>
        public static ApiResponse Success(string message)
        {
            return new ApiResponse
            {
                Error = false,
                Message = message,
                Data = new object(),
                ReferenceName = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// Creates a success response with complex data payload
        /// </summary>
        /// <param name="message">Descriptive success message</param>
        /// <param name="data">Business object or DTO to return</param>
        /// <returns>ApiResponse with populated Data property</returns>
        public static ApiResponse Success(string message, object data)
        {
            return new ApiResponse
            {
                Error = false,
                Message = message,
                Data = data ?? new object(),
                ReferenceName = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// Creates a success response with string data payload
        /// </summary>
        /// <param name="message">Success notification message</param>
        /// <param name="data">String-based result (e.g., token, identifier)</param>
        /// <returns>ApiResponse with string Data property</returns>
        public static ApiResponse Success(string message, string data)
        {
            return new ApiResponse
            {
                Error = false,
                Message = message,
                Data = data ?? string.Empty,
                ReferenceName = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// Creates an error response with additional context data
        /// </summary>
        /// <param name="message">Error description</param>
        /// <param name="referenceName">Tracking identifier (e.g., correlation ID)</param>
        /// <param name="data">Diagnostic information or error details</param>
        /// <returns>Error response with full context</returns>
        /// <remarks>
        /// Use this for errors requiring additional troubleshooting context
        /// </remarks>
        public static ApiResponse ErrorWithData(string message, string referenceName, object data)
        {
            return new ApiResponse
            {
                Error = true,
                Message = message,
                Data = data ?? new object(),
                ReferenceName = string.IsNullOrWhiteSpace(referenceName)
                    ? Guid.NewGuid().ToString()
                    : referenceName
            };
        }

        /// <summary>
        /// Creates a generic error response
        /// </summary>
        /// <param name="message">Error description (default: "Error")</param>
        /// <param name="referenceName">Optional tracking identifier</param>
        /// <returns>Basic error response</returns>
        public static ApiResponse Error(string message = "Error", string referenceName = "")
        {
            return new ApiResponse
            {
                Error = true,
                Message = message,
                Data = new object(),
                ReferenceName = string.IsNullOrWhiteSpace(referenceName)
                    ? Guid.NewGuid().ToString()
                    : referenceName
            };
        }

        /// <summary>
        /// Creates an error response from existing ApiResponse
        /// </summary>
        /// <param name="apiResponse">Original response to convert to error</param>
        /// <returns>New error response preserving message and reference</returns>
        /// <remarks>
        /// Note: Does not carry over original response data
        /// </remarks>
        public static ApiResponse Error(ApiResponse apiResponse)
        {
            return new ApiResponse
            {
                Error = true,
                Message = apiResponse.Message,
                Data = new object(),
                ReferenceName = string.IsNullOrWhiteSpace(apiResponse.ReferenceName)
                    ? Guid.NewGuid().ToString()
                    : apiResponse.ReferenceName
            };
        }

        /// <summary>
        /// Creates a "Not Found" error response
        /// </summary>
        /// <param name="message">Resource not found description</param>
        /// <returns>Error response with generated reference ID</returns>
        /// <example>
        /// return ApiResponseHandler.NotFound($"User {userId} not found");
        /// </example>
        public static ApiResponse NotFound(string message)
        {
            return new ApiResponse
            {
                Error = true,
                Message = message,
                Data = new object(),
                ReferenceName = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// Creates a bad request response from exception
        /// </summary>
        /// <param name="ex">Exception containing error details</param>
        /// <returns>Error response with exception message</returns>
        /// <remarks>
        /// In production systems, consider sanitizing exception messages
        /// </remarks>
        public static ApiResponse BadRequest(Exception ex)
        {
            return new ApiResponse
            {
                Error = true,
                Message = ex?.Message ?? "Invalid request",
                Data = new object(),
                ReferenceName = Guid.NewGuid().ToString()
            };
        }
    }
}