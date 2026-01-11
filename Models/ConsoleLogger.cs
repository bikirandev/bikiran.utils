using System;

namespace Bikiran.Utils.Models
{
    public class ConsoleLogger
    {
        // Basic colored output methods
        public static void Green(string message)
        {
            WriteLine(message, ConsoleColor.Green);
        }

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
        public static void Success(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ✓ SUCCESS: {message}" : $"✓ SUCCESS: {message}";
            Green(output);
        }

        public static void Info(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ℹ INFO: {message}" : $"ℹ INFO: {message}";
            WriteLine(output, ConsoleColor.Cyan);
        }

        public static void Warning(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ⚠ WARNING: {message}" : $"⚠ WARNING: {message}";
            WriteLine(output, ConsoleColor.Yellow);
        }

        public static void Error(string message, bool includeTimestamp = true)
        {
            string output = includeTimestamp ? $"[{DateTime.Now:yyyy-MM-dd HH: mm:ss}] ✗ ERROR: {message}" : $"✗ ERROR: {message}";
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
