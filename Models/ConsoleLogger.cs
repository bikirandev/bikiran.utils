using System;

namespace Bikiran.Utils.Models
{
    /// <summary>
    /// Provides colored console output and formatted logging methods for .NET console applications.
    /// Includes basic colored output, log levels with timestamps, progress indicators, headers, and banners.
    /// </summary>
    public class ConsoleLogger
    {
        // Basic colored output methods

        /// <summary>
        /// Writes a message in green color to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void Green(string message)
        {
            WriteLine(message, ConsoleColor.Green);
        }

        /// <summary>
        /// Writes a message in red color to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void Red(string message)
        {
            WriteLine(message, ConsoleColor.Red);
        }

        public static void Yellow(string message)
        {
            WriteLine(message, ConsoleColor.Yellow);
        }

        public static void Blue(string message)
        {
            WriteLine(message, ConsoleColor.Blue);
        }

        public static void Cyan(string message)
        {
            WriteLine(message, ConsoleColor.Cyan);
        }

        public static void Magenta(string message)
        {
            WriteLine(message, ConsoleColor.Magenta);
        }

        public static void Gray(string message)
        {
            WriteLine(message, ConsoleColor.Gray);
        }

        public static void White(string message)
        {
            WriteLine(message, ConsoleColor.White);
        }

        // Log level methods with timestamps

        /// <summary>
        /// Writes a success log message with a checkmark symbol and optional timestamp.
        /// </summary>
        /// <param name="message">The success message to display.</param>
        /// <param name="includeTimestamp">If true, includes a timestamp in the format [yyyy-MM-dd HH:mm:ss].</param>
        public static void Success(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ✓ SUCCESS: {message}" : $"✓ SUCCESS: {message}";
            Green(output);
        }

        /// <summary>
        /// Writes an informational log message with an info symbol and optional timestamp.
        /// </summary>
        /// <param name="message">The info message to display.</param>
        /// <param name="includeTimestamp">If true, includes a timestamp in the format [yyyy-MM-dd HH:mm:ss].</param>
        public static void Info(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ℹ INFO: {message}" : $"ℹ INFO: {message}";
            WriteLine(output, ConsoleColor.Cyan);
        }

        /// <summary>
        /// Writes a warning log message with a warning symbol and optional timestamp.
        /// </summary>
        /// <param name="message">The warning message to display.</param>
        /// <param name="includeTimestamp">If true, includes a timestamp in the format [yyyy-MM-dd HH:mm:ss].</param>
        public static void Warning(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ⚠ WARNING: {message}" : $"⚠ WARNING: {message}";
            WriteLine(output, ConsoleColor.Yellow);
        }

        /// <summary>
        /// Writes an error log message with an error symbol and optional timestamp.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <param name="includeTimestamp">If true, includes a timestamp in the format [yyyy-MM-dd HH:mm:ss].</param>
        public static void Error(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ✗ ERROR: {message}" : $"✗ ERROR: {message}";
            WriteLine(output, ConsoleColor.Red);
        }

        public static void Debug(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 🐛 DEBUG: {message}" : $"🐛 DEBUG: {message}";
            WriteLine(output, ConsoleColor.Gray);
        }

        // Generic colored output method
        public static void WriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        // Multi-color output on same line

        /// <summary>
        /// Writes multiple text segments with different colors on the same line.
        /// </summary>
        /// <param name="segments">An array of tuples containing text and color pairs.</param>
        /// <example>
        /// <code>
        /// ConsoleLogger.WriteMultiColor(
        ///     ("Status: ", ConsoleColor.Gray),
        ///     ("Active", ConsoleColor.Green)
        /// );
        /// </code>
        /// </example>
        public static void WriteMultiColor(params (string text, ConsoleColor color)[] segments)
        {
            foreach (var (text, color) in segments)
            {
                Console.ForegroundColor = color;
                Console.Write(text);
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        // Header and separator methods

        /// <summary>
        /// Writes a formatted header with border lines around the title.
        /// </summary>
        /// <param name="title">The title text to display in the header.</param>
        /// <param name="color">The color to use for the header (default: Cyan).</param>
        public static void WriteHeader(string title, ConsoleColor color = ConsoleColor.Cyan)
        {
            string border = new string('=', title.Length + 4);
            WriteLine(border, color);
            WriteLine($"  {title}  ", color);
            WriteLine(border, color);
        }

        public static void WriteSeparator(char character = '-', int length = 50, ConsoleColor color = ConsoleColor.Gray)
        {
            WriteLine(new string(character, length), color);
        }

        // Table-like output
        public static void WriteKeyValue(string key, string value, ConsoleColor keyColor = ConsoleColor.Yellow, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{key}:  ", keyColor);
            WriteLine(value, valueColor);
        }

        // Progress indicator

        /// <summary>
        /// Displays a progress indicator with percentage completion on the same line.
        /// Automatically adds a newline when current equals total.
        /// </summary>
        /// <param name="message">The progress message to display.</param>
        /// <param name="current">The current progress value.</param>
        /// <param name="total">The total value representing 100% completion.</param>
        /// <param name="color">The color to use for the progress text (default: Green).</param>
        public static void WriteProgress(string message, int current, int total, ConsoleColor color = ConsoleColor.Green)
        {
            double percentage = (double)current / total * 100;
            string output = $"{message} [{current}/{total}] ({percentage:F1}%)";
            Write($"\r{output}".PadRight(Console.WindowWidth - 1), color);

            if (current == total)
            {
                Console.WriteLine();
            }
        }

        // Banner with background color
        public static void WriteBanner(string message, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.DarkBlue)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.WriteLine($" {message.PadRight(Console.WindowWidth - 2)} ");
            Console.ResetColor();
        }

        // Clear console with optional color
        public static void Clear(ConsoleColor? backgroundColor = null)
        {
            if (backgroundColor.HasValue)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }
            Console.Clear();
            Console.ResetColor();
        }
    }
}
