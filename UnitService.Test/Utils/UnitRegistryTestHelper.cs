namespace UnitService.Test.Utils
{
    internal sealed class UnitRegistryTestHelper
    {
        public static void RegisterUnitsForTesting()
        {
            var t = new object();
            lock (t)
            {
                UnitRegistry.RegisterUnit("METER", new Unit("METER", "m", (1, 0), Dimensions.LENGTH));
                UnitRegistry.RegisterUnit("KILOMETER", new Unit("KILOMETER", "km", (0.001, 0), Dimensions.LENGTH));
                UnitRegistry.RegisterUnit("SECOND", new Unit("SECOND", "s", (1, 0), Dimensions.TIME));
                UnitRegistry.RegisterUnit("MINUTE", new Unit("MINUTE", "min", (1 / 60d, 0), Dimensions.TIME));
                UnitRegistry.RegisterUnit("HOUR", new Unit("HOUR", "hr", (1 / 3600d, 0), Dimensions.TIME));
                UnitRegistry.RegisterUnit("METER_PER_SECOND", new Unit("METER_PER_SECOND", "m/s", (1, 0), Dimensions.LENGTH));
                UnitRegistry.RegisterUnit("KILOMETER_PER_SECOND", new Unit("KILOMETER_PER_SECOND", "km/s", (0.001, 0), Dimensions.SPEED));
                UnitRegistry.RegisterUnit("KILOMETER_PER_HOUR", new Unit("KILOMETER_PER_HOUR", "km/hr", (0.277778, 0), Dimensions.SPEED));
                UnitRegistry.RegisterUnit("KELVIN", new Unit("KELVIN", "K", (1, 0), Dimensions.TEMPERATURE));
                UnitRegistry.RegisterUnit("CELCIUS", new Unit("CELCIUS", "°C", (1, -273.15), Dimensions.TEMPERATURE));
            }
        }

        public static void UnregisterUnitsForTesting()
        {
            var t = new object();
            lock (t)
            {
                UnitRegistry.UnregisterUnit("METER");
                UnitRegistry.UnregisterUnit("KILOMETER");
                UnitRegistry.UnregisterUnit("SECOND");
                UnitRegistry.UnregisterUnit("MINUTE");
                UnitRegistry.UnregisterUnit("HOUR");
                UnitRegistry.UnregisterUnit("METER_PER_SECOND");
                UnitRegistry.UnregisterUnit("KILOMETER_PER_SECOND");
                UnitRegistry.UnregisterUnit("KILOMETER_PER_HOUR");
                UnitRegistry.UnregisterUnit("KELVIN");
                UnitRegistry.UnregisterUnit("CELCIUS");
            }
        }
    }
}
