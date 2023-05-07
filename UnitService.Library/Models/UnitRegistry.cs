using System;
using System.Collections.Generic;
using System.Linq;
using UnitService.Library.Constants;

namespace UnitService.Library.Models
{
    public static class UnitRegistry
    {
        private static readonly UnitRegistryOptions _opts;
        private static readonly Dictionary<string, Unit>
            units = new Dictionary<string, Unit>();

        static UnitRegistry()
        {
            _opts = new UnitRegistryOptions();

            //TODO: This should be read from a file and generated automatically
            units.Add("METER", new Unit("METER", "m", (1, 0), Dimensions.LENGTH));
            units.Add("KILOMETER", new Unit("KILOMETER", "km", (0.001, 0), Dimensions.LENGTH));
            units.Add("SECOND", new Unit("SECOND", "s", (1, 0), Dimensions.TIME));
            units.Add("METER_PER_SECOND", new Unit("METER_PER_SECOND", "m/s", (1, 0), Dimensions.LENGTH));
            units.Add("KILOMETER_PER_SECOND", new Unit("KILOMETER_PER_SECOND", "km/s", (0.001, 0), Dimensions.SPEED));
            units.Add("KILOMETER_PER_HOUR", new Unit("KILOMETER_PER_HOUR", "km/h", (0.277778, 0), Dimensions.SPEED));
            units.Add("KELVIN", new Unit("KELVIN", "K", (1, 0), Dimensions.TEMPERATURE));
            units.Add("CELCIUS", new Unit("CELCIUS", "°C", (1, 273), Dimensions.TEMPERATURE));
        }
        public static void RegisterUnit(string name, Unit unit)
        {
            if (units.ContainsKey(name)) return;
            units.Add(name, unit);
        }

        public static bool UnregisterUnit(string name)
        {
            return units.Remove(name);
        }

        public static Unit GetUnit(string unitName)
        {
            try
            {
                return units[unitName];
            }
            catch (KeyNotFoundException knfe)
            {
                throw knfe;
            }
        }

        public static Unit GetBaseUnit(Dimension dimension)
        {
            return units.First(u => u.Value.IsBaseUnit && u.Value.Dimension == dimension).Value;
        }
    }
}
