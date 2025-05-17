using System;

namespace Bikiran.Utils.Models
{
    /// <summary>
    /// Represents a configurable key-value pair with metadata
    /// </summary>
    /// <remarks>
    /// Typical use cases include:
    /// - Application configuration settings
    /// - Feature flag definitions
    /// - Dynamic content properties
    /// - System parameter storage
    /// </remarks>
    public class KeyObj
    {
        /// <summary>
        /// Unique identifier for the key-value pair
        /// </summary>
        /// <value>
        /// Case-sensitive unique string identifier. Default: empty string
        /// </value>
        /// <example>"App.Theme.ColorScheme"</example>
        public string Key { get; set; } = "";


        /// <summary>
        /// Human-readable display name for the key
        /// </summary>
        /// <value>
        /// Localized or user-friendly description. Default: empty string
        /// </value>
        /// <example>"Application Color Theme"</example>
        public string Title { get; set; } = "";


        /// <summary>
        /// Default value to use when no specific value is set
        /// </summary>
        /// <value>
        /// Can be any .NET object type. Null is allowed.
        /// </value>
        /// <example>dark</example>
        public Object DefaultValue { get; set; }


        /// <summary>
        /// Determines visibility and accessibility of the key
        /// </summary>
        /// <value>
        /// True: Accessible publicly without restrictions
        /// False: Requires special permissions to access
        /// Default: true
        /// </value>
        public bool IsPublic { get; set; } = true;
    }
}
