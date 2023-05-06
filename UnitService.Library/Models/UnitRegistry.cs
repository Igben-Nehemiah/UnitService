using System;
using System.Collections.Generic;
using UnitService.Library.Constants;

namespace UnitService.Library.Models
{
    public class UnitRegistry
    {
        private readonly UnitRegistryOptions _opts;
        private readonly Dictionary<string, Unit>
            units = new Dictionary<string, Unit>();

        public UnitRegistry(UnitRegistryOptions opts)
        {
            _opts = opts;
            units.Add("METER", new Unit("METER", "m", (1, 0), Dimensions.LENGTH));
            units.Add("KILOMETER", new Unit("KILOMETER", "km", (0.001, 0), Dimensions.LENGTH));
            units.Add("SECOND", new Unit("SECOND", "s", (1, 0), Dimensions.LENGTH));
            units.Add("METER_PER_SECOND", new Unit("METER_PER_SECOND", "m/s", (1, 0), Dimensions.LENGTH));
        }
        public void RegisterUnit(string name, Unit unit)
        {
            units.Add(name, unit);
        }

        public Unit GetUnit(string unitName)
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
    }
}
