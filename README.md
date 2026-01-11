# Bikiran.Utils

![NuGet Version](https://img.shields.io/nuget/v/Bikiran.Utils.svg?style=flat-square)
![License](https://img.shields.io/github/license/bikirandev/Bikiran.Utils.svg?style=flat-square)
[![API Docs](https://img.shields.io/badge/docs-API%20Reference-blue.svg)](https://github.com/bikirandev/Bikiran.Utils/wiki)

A comprehensive utility library for .NET development with debugging, API support, and configuration management

## Installation

```bash
dotnet add package Bikiran.Utils
```

## Features

### Console Logging

- 🎨 Colored console output with 8 color methods
- 📝 Log level methods (Success, Info, Warning, Error, Debug)
- ⏱️ Optional timestamps on log messages
- 🎭 Multi-color output on same line
- 📊 Headers, separators, and banners
- 📈 Progress indicators for long-running tasks
- 🔑 Key-value formatted output

### Debugging Utilities

- 🖨️ Formatted console output with index tracking
- 🚨 Exception generation with JSON-formatted context
- 🔍 NuGet package version inspection
- 🎯 Simplified debugging workflows

### API Response Handling

- ✅ Structured response models for REST APIs
- 🏭 Factory methods for success/error responses
- 🔗 Request correlation tracking
- 🛠️ Exception-to-response mapping
- 🎯 Generic typed responses with field-level error handling (V3)

### Exception Handling

- 🎯 Simplified exception creation with metadata
- 🏷️ Reference tracking for debugging context
- 🔧 Consistent exception formatting
- 📋 Field-specific error collection support (V3)

### IP Address Utilities

- 🌐 Client IP extraction from HTTP context
- 🔍 Proxy and CDN header support (Cloudflare, Nginx)
- 🔢 IP address format conversion (string/long)
- 🛡️ Robust fallback mechanisms

### Configuration Management

- 🔑 Key-value pair modeling with metadata
- 🏷️ Typed configuration objects with defaults
- 🔒 Public/private access control
- 📋 ToString() override for diagnostics

## Quick Start

### Console Logging

```csharp
using Bikiran.Utils.Models;

// Basic colored output
ConsoleLogger.Green("Success message");
ConsoleLogger.Red("Error message");
ConsoleLogger.Yellow("Warning message");

// Log levels with timestamps
ConsoleLogger.Success("Operation completed successfully");
ConsoleLogger.Info("Processing data...");
ConsoleLogger.Warning("Resource usage is high");
ConsoleLogger.Error("Failed to connect to database");
ConsoleLogger.Debug("Variable x = 42");

// Multi-color output on same line
ConsoleLogger.WriteMultiColor(
    ("Status: ", ConsoleColor.Gray),
    ("Active", ConsoleColor.Green),
    (" | Users: ", ConsoleColor.Gray),
    ("1,234", ConsoleColor.Cyan)
);

// Headers and separators
ConsoleLogger.WriteHeader("Application Started");
ConsoleLogger.WriteSeparator();

// Progress indicator
for (int i = 1; i <= 100; i++)
{
    ConsoleLogger.WriteProgress("Processing", i, 100);
    Thread.Sleep(50);
}

// Key-value pairs
ConsoleLogger.WriteKeyValue("Server", "192.168.1.1");
ConsoleLogger.WriteKeyValue("Port", "8080");

// Banner with background
ConsoleLogger.WriteBanner("SYSTEM ALERT", ConsoleColor.White, ConsoleColor.DarkRed);
```

### Debugging Helpers

```csharp
using Bikiran.Utils;

// Print indexed values
C.Print("App started", DateTime.UtcNow, Environment.Version);

// Throw with context
try {
    riskyOperation();
}
catch {
    C.X("Operation failed", "User ID: 1234", errorDetails);
}
```

### Exception Creation

```csharp
using Bikiran.Utils.Models;

// Create exception with reference
var exception = Excep.Create("Invalid user input", "UserService.ValidateUser");
throw exception;

// Exception will contain reference data for debugging
// exception.Data["Reference"] will be "UserService.ValidateUser"

// Create exception with field-specific errors (V3)
var errors = new List<ApiErrorV3>
{
    new ApiErrorV3 { Field = "Email", Message = "Invalid email format" },
    new ApiErrorV3 { Field = "Password", Message = "Password must be at least 8 characters" }
};
var validationException = Excep.CreateV3("Validation failed", errors);
throw validationException;
```

### IP Address Operations

```csharp
using Bikiran.Utils.Models;

// In your ASP.NET Core controller or middleware
public IActionResult GetUserInfo()
{
    // Get client IP as string (supports Cloudflare, Nginx proxies)
    string clientIp = IpOperation.GetIpString(HttpContext);

    // Get IP as long integer for database storage/comparison
    long ipAsLong = IpOperation.GetIpLong(HttpContext);

    return Ok(new { ClientIP = clientIp, IpNumeric = ipAsLong });
}

// Handles:
// - CF-Connecting-IP (Cloudflare)
// - X-Forwarded-For (Nginx/Proxy)
// - Direct connection IP fallback
```

### API Response Handling

```csharp
using Bikiran.Utils.Models.ApiResp;

// Successful response
return ApiResponseHandler.Success("User created", new { Id = 42, Username = "john" });

// Error response
return ApiResponseHandler.NotFound("User not found");

// Exception handling
try {
    ProcessOrder();
}
catch (Exception ex) {
    return ApiResponseHandler.BadRequest(ex);
}
```

### API Response V3 (Strongly-Typed with Field Errors)

```csharp
using Bikiran.Utils.ApiResp;

// Success response with strongly-typed data
var successResponse = new ApiResponseV3<User>
{
    Error = false,
    Message = "User retrieved successfully",
    Data = new User { Id = 1, Name = "John Doe" }
};

// Validation error response with field-specific errors
var errorResponse = new ApiResponseV3<object>
{
    Error = true,
    Message = "Validation failed",
    Errors = new List<ApiErrorV3>
    {
        new ApiErrorV3 { Field = "Email", Message = "Invalid email format" },
        new ApiErrorV3 { Field = "Age", Message = "Must be 18 or older" }
    }
};

// Use in ASP.NET Core controller
[HttpPost]
public ActionResult<ApiResponseV3<User>> CreateUser(UserDto dto)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState.SelectMany(x => x.Value.Errors)
            .Select(e => new ApiErrorV3
            {
                Field = e.Exception?.Source ?? "Unknown",
                Message = e.ErrorMessage
            }).ToList();

        return BadRequest(new ApiResponseV3<object>
        {
            Error = true,
            Message = "Validation failed",
            Errors = errors
        });
    }

    var user = userService.Create(dto);
    return Ok(new ApiResponseV3<User>
    {
        Error = false,
        Message = "User created successfully",
        Data = user,
        ReferenceName = Guid.NewGuid().ToString()
    });
}
```

### Key-Value Configuration

```csharp
using Bikiran.Utils.Models;

// Create a configuration entry
var config = new KeyObj
{
    Key = "App.Theme.Color",
    Title = "Application Color Scheme",
    DefaultValue = "dark",
    IsPublic = false
};

Console.WriteLine(config);
// Output: App.Theme.Color (Application Color Scheme) - Default: dark [Private]
```

## Core Components

### `ConsoleLogger` Class (Console Output)

```csharp
// Basic colored output methods (8 colors)
ConsoleLogger.Green("message");    // Green text
ConsoleLogger.Red("message");      // Red text
ConsoleLogger.Yellow("message");   // Yellow text
ConsoleLogger.Blue("message");     // Blue text
ConsoleLogger.Cyan("message");     // Cyan text
ConsoleLogger.Magenta("message");  // Magenta text
ConsoleLogger.Gray("message");     // Gray text
ConsoleLogger.White("message");    // White text

// Log level methods with optional timestamps
ConsoleLogger.Success("Operation completed", includeTimestamp: true);
ConsoleLogger.Info("Processing started", includeTimestamp: true);
ConsoleLogger.Warning("Low disk space", includeTimestamp: false);
ConsoleLogger.Error("Connection failed", includeTimestamp: true);
ConsoleLogger.Debug("Variable state", includeTimestamp: false);

// Generic colored output
ConsoleLogger.WriteLine("Custom message", ConsoleColor.Magenta);
ConsoleLogger.Write("Inline text ", ConsoleColor.Yellow);

// Multi-color output on same line
ConsoleLogger.WriteMultiColor(
    ("User: ", ConsoleColor.Gray),
    ("admin", ConsoleColor.Green),
    (" | Status: ", ConsoleColor.Gray),
    ("Online", ConsoleColor.Cyan)
);

// Headers and separators
ConsoleLogger.WriteHeader("System Report", ConsoleColor.Cyan);
ConsoleLogger.WriteSeparator('-', 50, ConsoleColor.Gray);
ConsoleLogger.WriteSeparator('=', 80, ConsoleColor.Blue);

// Key-value formatted output
ConsoleLogger.WriteKeyValue("Database", "Connected",
    ConsoleColor.Yellow, ConsoleColor.Green);
ConsoleLogger.WriteKeyValue("Cache", "Disabled",
    ConsoleColor.Yellow, ConsoleColor.Red);

// Progress indicator for loops
for (int i = 1; i <= total; i++)
{
    ConsoleLogger.WriteProgress("Uploading files", i, total, ConsoleColor.Green);
    // ... processing logic
}

// Banner with background color
ConsoleLogger.WriteBanner("PRODUCTION ENVIRONMENT",
    ConsoleColor.White, ConsoleColor.DarkRed);

// Clear console with optional background color
ConsoleLogger.Clear(ConsoleColor.DarkBlue);
```

### `C` Class (Debugging)

```csharp
// Print values with headers
C.Print("Configuration", configValue);

// Immediate error termination
C.X("Critical failure", errorContext);
```

### `Excep` Class (Exception Creation)

```csharp
// Create exception with optional reference
var exception = Excep.Create("Error message", "Optional.Reference.Context");

// The exception will have:
// - Message: "Error message"
// - Data["Reference"]: "Optional.Reference.Context"

// Create exception with field-specific errors (V3)
var errors = new List<ApiErrorV3>
{
    new ApiErrorV3 { Field = "Username", Message = "Already exists" }
};
var exception = Excep.CreateV3("Validation failed", errors);
// The exception will have:
// - Message: "Validation failed"
// - Data["Reference"]: List<ApiErrorV3> collection
```

### `IpOperation` Class (IP Address Utilities)

```csharp
// Get client IP address as string
string clientIp = IpOperation.GetIpString(httpContext);

// Get client IP address as long integer
long ipNumeric = IpOperation.GetIpLong(httpContext);

// Priority order for IP detection:
// 1. CF-Connecting-IP (Cloudflare)
// 2. X-Forwarded-For (Nginx/Proxy)
// 3. RemoteIpAddress (Direct connection)
```

### `ApiResponse` Model

```csharp
public class ApiResponse {
    public bool Error { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public string ReferenceName { get; set; }
}
```

### `ApiResponseV3<T>` Model (Strongly-Typed)

```csharp
public class ApiResponseV3<T> {
    public bool Error { get; set; }
    public List<ApiErrorV3>? Errors { get; set; }  // Field-specific errors
    public string Message { get; set; }
    public T? Data { get; set; }                   // Generic typed data
    public string ReferenceName { get; set; }
}

public class ApiErrorV3 {
    public string Field { get; set; }     // Field name
    public string Message { get; set; }    // Error message
}
```

### `KeyObj` Configuration Model

```csharp
public class KeyObj {
    public string Key { get; set; }         // Unique identifier
    public string Title { get; set; }        // Human-readable name
    public object DefaultValue { get; set; } // Default value
    public bool IsPublic { get; set; }       // Visibility flag
}
```

### Factory Methods Reference

| Component         | Method              | Purpose                            | Example                                 |
| ----------------- | ------------------- | ---------------------------------- | --------------------------------------- |
| **ConsoleLogger** | `Green()`           | Green colored output               | `Green("Success")`                      |
|                   | `Red()`             | Red colored output                 | `Red("Error")`                          |
|                   | `Yellow()`          | Yellow colored output              | `Yellow("Warning")`                     |
|                   | `Blue()`            | Blue colored output                | `Blue("Info")`                          |
|                   | `Cyan()`            | Cyan colored output                | `Cyan("Notice")`                        |
|                   | `Magenta()`         | Magenta colored output             | `Magenta("Debug")`                      |
|                   | `Gray()`            | Gray colored output                | `Gray("Disabled")`                      |
|                   | `White()`           | White colored output               | `White("Normal")`                       |
|                   | `Success()`         | Success log with timestamp         | `Success("Done", true)`                 |
|                   | `Info()`            | Info log with timestamp            | `Info("Starting", true)`                |
|                   | `Warning()`         | Warning log with timestamp         | `Warning("Check this", false)`          |
|                   | `Error()`           | Error log with timestamp           | `Error("Failed", true)`                 |
|                   | `Debug()`           | Debug log with timestamp           | `Debug("x=5", false)`                   |
|                   | `WriteLine()`       | Custom colored line                | `WriteLine("Text", Color.Red)`          |
|                   | `Write()`           | Custom colored inline text         | `Write("Text", Color.Blue)`             |
|                   | `WriteMultiColor()` | Multi-color line                   | `WriteMultiColor(segments)`             |
|                   | `WriteHeader()`     | Formatted header                   | `WriteHeader("Title")`                  |
|                   | `WriteSeparator()`  | Separator line                     | `WriteSeparator('-', 50)`               |
|                   | `WriteKeyValue()`   | Key-value pair                     | `WriteKeyValue("Key", "Value")`         |
|                   | `WriteProgress()`   | Progress indicator                 | `WriteProgress("Task", 5, 10)`          |
|                   | `WriteBanner()`     | Banner with background             | `WriteBanner("ALERT")`                  |
|                   | `Clear()`           | Clear console                      | `Clear(ConsoleColor.Black)`             |
| **IpOperation**   | `GetIpString()`     | Get client IP as string            | `GetIpString(HttpContext)`              |
|                   | `GetIpLong()`       | Get client IP as long integer      | `GetIpLong(HttpContext)`                |
| **Excep**         | `Create()`          | Create exception with reference    | `Create("Error", "Module.Method")`      |
|                   | `CreateV3()`        | Create exception with field errors | `CreateV3("Validation failed", errors)` |
| **ApiResponse**   | `Success()`         | Successful operation               | `Success("Created", data)`              |
|                   | `Error()`           | Generic error                      | `Error("Validation failed")`            |
|                   | `NotFound()`        | 404 equivalent                     | `NotFound("User missing")`              |
|                   | `BadRequest()`      | 400 equivalent                     | `BadRequest(exception)`                 |
| **ApiResponseV3** | (Constructor)       | Create typed API response          | `new ApiResponseV3<User> { ... }`       |
| **ApiErrorV3**    | (Constructor)       | Create field-level error           | `new ApiErrorV3 { ... }`                |
| **KeyObj**        | (Constructor)       | Create configuration entry         | `new KeyObj { ... }`                    |

## Key Differences: ApiResponse vs ApiResponseV3

| Feature           | ApiResponse                    | ApiResponseV3<T>                        |
| ----------------- | ------------------------------ | --------------------------------------- |
| **Data Type**     | `object` (requires casting)    | Generic `T` (type-safe)                 |
| **Error Details** | Single message only            | Collection of field-specific errors     |
| **Null Safety**   | Standard C#                    | Nullable reference types enabled        |
| **Use Case**      | Simple success/error responses | Complex validation with multiple errors |
| **Type Safety**   | Runtime checks needed          | Compile-time type checking              |

## Real-World Use Cases

### Console Application Logging

```csharp
// Application startup sequence
ConsoleLogger.WriteHeader("MyApp v1.0.0");
ConsoleLogger.Info("Initializing application...");
ConsoleLogger.Success("Configuration loaded");
ConsoleLogger.Success("Database connected");
ConsoleLogger.WriteSeparator();

// Processing with progress
var items = GetItemsToProcess();
for (int i = 0; i < items.Count; i++)
{
    ConsoleLogger.WriteProgress("Processing items", i + 1, items.Count);
    ProcessItem(items[i]);
}

// System status display
ConsoleLogger.WriteSeparator('=', 60);
ConsoleLogger.WriteKeyValue("Server", "Running", ConsoleColor.Yellow, ConsoleColor.Green);
ConsoleLogger.WriteKeyValue("Memory Usage", "45%", ConsoleColor.Yellow, ConsoleColor.Cyan);
ConsoleLogger.WriteKeyValue("Active Users", "127", ConsoleColor.Yellow, ConsoleColor.White);
ConsoleLogger.WriteSeparator('=', 60);
```

### API Development with Combined Utilities

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public ActionResult<ApiResponseV3<User>> CreateUser(CreateUserDto dto)
    {
        // Log incoming request
        var clientIp = IpOperation.GetIpString(HttpContext);
        ConsoleLogger.Info($"User creation request from {clientIp}");

        // Validate input
        var errors = ValidateUser(dto);
        if (errors.Any())
        {
            ConsoleLogger.Warning($"Validation failed for user: {dto.Email}");
            return BadRequest(new ApiResponseV3<object>
            {
                Error = true,
                Message = "Validation failed",
                Errors = errors
            });
        }

        try
        {
            var user = _userService.Create(dto);
            ConsoleLogger.Success($"User created: {user.Email} (ID: {user.Id})");

            return Ok(new ApiResponseV3<User>
            {
                Error = false,
                Message = "User created successfully",
                Data = user,
                ReferenceName = Guid.NewGuid().ToString()
            });
        }
        catch (Exception ex)
        {
            ConsoleLogger.Error($"Failed to create user: {ex.Message}");
            return StatusCode(500, ApiResponseHandler.Error(ex));
        }
    }
}
```

### Background Job Monitoring

```csharp
public class DataSyncJob
{
    public async Task RunAsync()
    {
        ConsoleLogger.WriteBanner("DATA SYNC JOB STARTED",
            ConsoleColor.White, ConsoleColor.DarkBlue);

        var startTime = DateTime.UtcNow;
        ConsoleLogger.WriteKeyValue("Start Time", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
        ConsoleLogger.WriteSeparator();

        try
        {
            // Sync operation
            var records = await FetchRecordsAsync();
            ConsoleLogger.Info($"Fetched {records.Count} records");

            for (int i = 0; i < records.Count; i++)
            {
                await SyncRecord(records[i]);
                ConsoleLogger.WriteProgress("Syncing", i + 1, records.Count);
            }

            var duration = DateTime.UtcNow - startTime;
            ConsoleLogger.WriteSeparator();
            ConsoleLogger.Success($"Sync completed in {duration.TotalSeconds:F2} seconds");
            ConsoleLogger.WriteKeyValue("Records Synced", records.Count.ToString());
        }
        catch (Exception ex)
        {
            ConsoleLogger.Error($"Sync failed: {ex.Message}");
            ConsoleLogger.Debug($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}
```

## Documentation

- **IntelliSense Support:** Full XML documentation included
- **API Reference:** [GitHub Wiki](https://github.com/bikirandev/Bikiran.Utils/wiki)
- **Samples:** [Example Projects](/examples) directory
- **Configuration Guide:** [KeyObj Usage](https://github.com/bikirandev/Bikiran.Utils/wiki/KeyObj-Usage)

## Contributing

We welcome contributions! Please:

1. Fork the repository
2. Create your feature branch
3. Add tests for new functionality
4. Submit a pull request

[View contribution guidelines](CONTRIBUTING.md)

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for full terms.
