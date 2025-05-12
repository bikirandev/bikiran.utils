using System;

namespace Bikiran.Utils.ApiResp
{
    /// <summary>
    /// Represents a standardized API response format for consistent client communication
    /// </summary>
    /// <remarks>
    /// This class provides a structured format for API responses containing:
    /// - Error status indicator
    /// - Human-readable message
    /// - Data payload
    /// - Reference identifier for tracking
    /// </remarks>
    public class ApiResponse
    {
        /// <summary>
        /// Indicates whether the request resulted in an error
        /// </summary>
        /// <value>
        /// <c>true</c> if an error occurred during processing; otherwise, <c>false</c>
        /// </value>
        public bool Error { get; set; }

        /// <summary>
        /// Human-readable message describing the operation result
        /// </summary>
        /// <value>
        /// Success message or error description. Default: empty string
        /// </value>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Primary data payload of the response
        /// </summary>
        /// <value>
        /// Can be a single object, collection, or empty. Default: empty object
        /// </value>
        public object Data { get; set; } = new object();

        /// <summary>
        /// Unique identifier for tracking requests across systems
        /// </summary>
        /// <value>
        /// Correlation ID or transaction reference. Default: empty string
        /// </value>
        public string ReferenceName { get; set; } = string.Empty;
    }
}