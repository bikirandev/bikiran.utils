# Bikiran.Utils

![NuGet Version](https://img.shields.io/nuget/v/Bikiran.Utils.svg?style=flat-square)
![License](https://img.shields.io/github/license/yourusername/Bikiran.Utils.svg?style=flat-square)

A utility library for common .NET operations with enhanced debugging capabilities

## Installation
```bash
dotnet add package Bikiran.Utils
```

## Features
- Formatted console output with index tracking
- Exception generation with JSON-formatted context
- NuGet package version inspection
- Simplified debugging workflows

## Quick Start
```csharp
using Bikiran.Utils;

// Print values with index markers
C.Print("Application started", DateTime.Now.ToString());

// Throw exception with context
try {
    // Your code
}
catch {
    C.X("Error occurred", "Additional context", someVariable);
}
```

## Documentation
Full API reference available at [GitHub Wiki](https://github.com/yourusername/Bikiran.Utils/wiki)

## Contributing
Pull requests welcome! Please follow our [contribution guidelines](CONTRIBUTING.md)

## License
MIT License - See [LICENSE](LICENSE) for details