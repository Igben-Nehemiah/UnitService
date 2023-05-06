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
            units.Add("METER", new Unit("METER", "m", (1, 0), Dimensions.LENGTH));
            units.Add("KILOMETER", new Unit("KILOMETER", "km", (0.001, 0), Dimensions.LENGTH));
            units.Add("SECOND", new Unit("SECOND", "s", (1, 0), Dimensions.LENGTH));
            units.Add("METER_PER_SECOND", new Unit("METER_PER_SECOND", "m/s", (1, 0), Dimensions.LENGTH));
        }
        public static void RegisterUnit(string name, Unit unit)
        {
            units.Add(name, unit);
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
