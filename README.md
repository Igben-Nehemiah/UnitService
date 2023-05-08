# UnitService

[![Build and Test](https://github.com/Igben-Nehemiah/UnitService/actions/workflows/build-test.yml/badge.svg)](https://github.com/Igben-Nehemiah/UnitService/actions/workflows/build-test.yml)

<!-- [![NuGet](https://img.shields.io/nuget/v/LibName.svg)](https://www.nuget.org/packages/LibName/) -->
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This is a C# library that provides useful functionalities for unit conversion.

## Design Philosophy

## Installation 

You can install Library Name via [NuGet](https://www.nuget.org/packages/UnitService/):
```csharp
PM> Install-Package UnitService
```

## Usage

To use UnitService, simply add a reference to the library in your C# project and include the appropriate using directive:

```csharp
using UnitService.Core.Constants;
using UnitService.Core.Models;
```

Then, you can use the various classes and methods provided by the library:

```csharp
UnitRegistry.RegisterUnit("METER", new Unit("METER", "m", (1, 0), Dimensions.LENGTH));
UnitRegistry.RegisterUnit("KILOMETER", new Unit("KILOMETER", "km", (0.001, 0), Dimensions.LENGTH));

Quantity distanceInMeter = new Quantity(100, UnitRegistry.GetUnit("METER"));
Quantity distanceInKilometer = distanceInMeter.ConvertTo("KILOMETER");

```

## Documentation

For detailed documentation on how to use Library Name, see the [Wiki](https://github.com/Igben-Nehemiah/UnitService/wiki).

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for more information.

## License

Library Name is licensed under the MIT License. See [LICENSE](LICENSE) for more information.






