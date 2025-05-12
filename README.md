# Bikiran.Utils

![NuGet Version](https://img.shields.io/nuget/v/Bikiran.Utils.svg?style=flat-square)
![License](https://img.shields.io/github/license/bikirandev/Bikiran.Utils.svg?style=flat-square)
[![API Docs](https://img.shields.io/badge/docs-API%20Reference-blue.svg)](https://github.com/bikirandev/Bikiran.Utils/wiki)

A comprehensive utility library for .NET development with debugging and API support

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

## Core Components

### `C` Class (Debugging)
```csharp
// Print values with headers
C.Print("Configuration", configValue);

// Immediate error termination
C.X("Critical failure", errorContext);
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

### Response Factory Methods
| Method | Purpose | Example |
|--------|---------|---------|
| `Success()` | Successful operation | `Success("Created", data)` |
| `Error()` | Generic error | `Error("Validation failed")` |
| `NotFound()` | 404 equivalent | `NotFound("User missing")` |
| `BadRequest()` | 400 equivalent | `BadRequest(exception)` |

## Documentation

- **IntelliSense Support:** Full XML documentation included
- **API Reference:** [GitHub Wiki](https://github.com/bikirandev/Bikiran.Utils/wiki)
- **Samples:** [Example Projects](/examples) directory

## Contributing

We welcome contributions! Please:
1. Fork the repository
2. Create your feature branch
3. Add tests for new functionality
4. Submit a pull request

[View contribution guidelines](CONTRIBUTING.md)

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for full terms.