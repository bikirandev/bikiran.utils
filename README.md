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
| Component          | Method            | Purpose                          | Example                          |
|--------------------|-------------------|----------------------------------|----------------------------------|
| **IpOperation**    | `GetIpString()`   | Get client IP as string          | `GetIpString(HttpContext)`       |
|                    | `GetIpLong()`     | Get client IP as long integer    | `GetIpLong(HttpContext)`         |
| **Excep**          | `Create()`        | Create exception with reference  | `Create("Error", "Module.Method")` |
|                    | `CreateV3()`      | Create exception with field errors | `CreateV3("Validation failed", errors)` |
| **ApiResponse**    | `Success()`       | Successful operation             | `Success("Created", data)`       |
|                    | `Error()`         | Generic error                    | `Error("Validation failed")`     |
|                    | `NotFound()`      | 404 equivalent                   | `NotFound("User missing")`       |
|                    | `BadRequest()`    | 400 equivalent                   | `BadRequest(exception)`          |
| **ApiResponseV3**  | (Constructor)     | Create typed API response        | `new ApiResponseV3<User> { ... }` |
| **ApiErrorV3**     | (Constructor)     | Create field-level error         | `new ApiErrorV3 { ... }`         |
| **KeyObj**         | (Constructor)     | Create configuration entry       | `new KeyObj { ... }`             |

## Key Differences: ApiResponse vs ApiResponseV3

| Feature | ApiResponse | ApiResponseV3<T> |
|---------|-------------|------------------|
| **Data Type** | `object` (requires casting) | Generic `T` (type-safe) |
| **Error Details** | Single message only | Collection of field-specific errors |
| **Null Safety** | Standard C# | Nullable reference types enabled |
| **Use Case** | Simple success/error responses | Complex validation with multiple errors |
| **Type Safety** | Runtime checks needed | Compile-time type checking |

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