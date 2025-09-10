using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Bikiran.Utils.Models
{
    /// <summary>
    /// Utility class for extracting and manipulating IP addresses from HTTP requests.
    /// Handles various proxy scenarios including Cloudflare and Nginx forwarding.
    /// </summary>
    public class IpOperation
    {
        /// <summary>
        /// Extracts the client's IP address from the HTTP request context.
        /// Prioritizes headers in the following order:
        /// 1. CF-Connecting-IP (Cloudflare)
        /// 2. X-Forwarded-For (Nginx/other proxies)
        /// 3. Connection RemoteIpAddress (direct connection)
        /// </summary>
        /// <param name="hContext">The HTTP context containing the request information.</param>
        /// <returns>
        /// The client's IP address as a string, or an empty string if the IP cannot be determined or an error occurs.
        /// </returns>
        /// <example>
        /// <code>
        /// var clientIp = IpOperation.GetIpString(httpContext);
        /// if (!string.IsNullOrEmpty(clientIp))
        /// {
        ///     // Process the IP address
        /// }
        /// </code>
        /// </example>
        public static string GetIpString(HttpContext hContext)
        {
            try
            {
                var request = hContext?.Request;
                if (request == null)
                {
                    return "";
                }

                // Check if Cloudflare's CF-Connecting-IP header is available
                if (request.Headers.ContainsKey("CF-Connecting-IP"))
                {
                    return request.Headers["CF-Connecting-IP"].ToString();
                }

                // Check X-Forwarded-For header set by Nginx or other proxies
                var forwardedIp = request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(forwardedIp))
                {
                    // Handle multiple IP addresses in X-Forwarded-For (the first one is the original)
                    return forwardedIp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First().Trim();
                }

                // Fall back to the remote IP address from connection
                return request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Converts the client's IP address to a 32-bit integer representation.
        /// This method only works with IPv4 addresses (4 bytes).
        /// </summary>
        /// <param name="hContext">The HTTP context containing the request information.</param>
        /// <returns>
        /// The IP address as a 32-bit integer (long), or 0 if the IP cannot be determined,
        /// is not IPv4, or an error occurs during conversion.
        /// </returns>
        /// <remarks>
        /// The conversion uses bit shifting to pack the 4 bytes of an IPv4 address into a single long value.
        /// IPv6 addresses are not supported and will return 0.
        /// </remarks>
        /// <example>
        /// <code>
        /// var ipLong = IpOperation.GetIpLong(httpContext);
        /// if (ipLong > 0)
        /// {
        ///     // Process the numeric IP representation
        /// }
        /// </code>
        /// </example>
        public static long GetIpLong(HttpContext hContext)
        {
            try
            {
                var request = hContext?.Request;
                if (request == null)
                {
                    return 0;
                }

                var forwardedIp = GetIpString(hContext);
                var ip = IPAddress.Parse(forwardedIp);
                var ipBytes = ip.GetAddressBytes();
                
                // Only process IPv4 addresses (4 bytes)
                if (ipBytes.Length == 4)
                {
                    // Convert bytes to long using bit shifting
                    return ipBytes[0] << 24 | ipBytes[1] << 16 | ipBytes[2] << 8 | ipBytes[3];
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
