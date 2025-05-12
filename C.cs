using System;
using NuGet.Protocol;

namespace Bikiran.Utils;

/// <summary>
/// Provides console output utilities with enhanced formatting and debugging capabilities
/// <remarks>
/// This class serves as a shorthand utility for common debugging and output scenarios.
/// Name 'C' was chosen for brevity in usage (e.g., C.Print() instead of Console.Write())
/// </remarks>
/// </summary>
public static class C
{
    /// <summary>
    /// Prints formatted values to the console with index information
    /// </summary>
    /// <param name="values">Values to print (null values are allowed)</param>
    /// <example>
    /// <code>
    /// C.P("test", null, "another value");
    /// // Output:
    /// // [0] test
    /// // [1] (null)
    /// // [2] another value
    /// </code>
    /// </example>
    public static void P(params string[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            var val = values[i];
            Console.WriteLine(" ");
            Console.WriteLine($"++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++[{i}]");
            Console.WriteLine(val ?? "(null)");
            Console.WriteLine(" ");
        }
    }

    /// <summary>
    /// Prints values to console and throws an exception containing JSON-formatted values
    /// </summary>
    /// <param name="values">Values to log and include in exception</param>
    /// <exception cref="Exception">
    /// Always throws exception containing JSON-formatted input values
    /// </exception>
    /// <remarks>
    /// Combines debug logging with immediate failure indication. Useful for critical error scenarios.
    /// </remarks>
    public static void X(params string[] values)
    {
        P(values);
        throw new Exception(values.ToJson());
    }

    /// <summary>
    /// Primary method for formatted console output
    /// </summary>
    /// <param name="values">Values to display in console</param>
    /// <remarks>
    /// This is the recommended method for normal output operations.
    /// For error handling with immediate termination, use <see cref="X"/>
    /// </remarks>
    public static void Print(params string[] values)
    {
        P(values);
    }
}