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

### Exception Handling
- 🎯 Simplified exception creation with metadata
- 🏷️ Reference tracking for debugging context
- 🔧 Consistent exception formatting

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
| Component          | Method            | Purpose                          | Example                          |
|--------------------|-------------------|----------------------------------|----------------------------------|
| **IpOperation**    | `GetIpString()`   | Get client IP as string          | `GetIpString(HttpContext)`       |
|                    | `GetIpLong()`     | Get client IP as long integer    | `GetIpLong(HttpContext)`         |
| **Excep**          | `Create()`        | Create exception with reference  | `Create("Error", "Module.Method")` |
| **ApiResponse**    | `Success()`       | Successful operation             | `Success("Created", data)`       |
|                    | `Error()`         | Generic error                    | `Error("Validation failed")`     |
|                    | `NotFound()`      | 404 equivalent                   | `NotFound("User missing")`       |
|                    | `BadRequest()`    | 400 equivalent                   | `BadRequest(exception)`          |
| **KeyObj**         | (Constructor)     | Create configuration entry       | `new KeyObj { ... }`             |

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