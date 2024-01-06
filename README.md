# UnitService

[![Build and Test](https://github.com/Igben-Nehemiah/UnitService/actions/workflows/build-test.yml/badge.svg)](https://github.com/Igben-Nehemiah/UnitService/actions/workflows/build-test.yml)

<!-- [![NuGet](https://img.shields.io/nuget/v/LibName.svg)](https://www.nuget.org/packages/LibName/) -->
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This is a C# library that provides useful functionalities for unit conversion.

## Design Philosophy
In physics and other sciences, a quantity is a property that can be measured or calculated, such as length, 
time, mass, or temperature. A unit is a standard of measurement used to quantify a particular quantity, 
such as meters for length, seconds for time, kilograms for mass, or degrees Celsius for temperature. 
A dimension is a fundamental physical parameter that describes the nature of a quantity, such as the 
dimension of length, time, mass, or temperature.

For example, the quantity of length can be measured in units of meters, feet, or inches, and has a 
dimension of L (length). The quantity of time can be measured in units of seconds, minutes, or hours, and 
has a dimension of T (time). The quantity of mass can be measured in units of kilograms, grams, or pounds, 
and has a dimension of M (mass). The quantity of temperature can be measured in units of Celsius, 
Fahrenheit, or Kelvin, and has a dimension of Θ (temperature).

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



