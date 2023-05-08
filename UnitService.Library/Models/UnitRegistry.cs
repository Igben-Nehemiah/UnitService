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
