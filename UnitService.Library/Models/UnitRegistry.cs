using System;
using System.Collections.Generic;
using System.Linq;
using UnitService.Library.Constants;

namespace UnitService.Library.Models
{
    /// <summary>
    /// This is an abstraction fo the units registry.
    /// </summary>
    public static class UnitRegistry
    {
        private static readonly UnitRegistryOptions _opts;
        private static readonly Dictionary<string, Unit>
            units = new Dictionary<string, Unit>();

        static UnitRegistry()
        {
            _opts = new UnitRegistryOptions();
        }

        /// <summary>
        /// Adds a unit with a given name to registry.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        public static void RegisterUnit(string name, Unit unit)
        {
            if (units.ContainsKey(name)) return;
            units.Add(name, unit);
        }

        /// <summary>
        /// Removes a unit with a given name from registry.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True if unit was removed successfully</returns>
        public static bool UnregisterUnit(string name)
        {
            return units.Remove(name);
        }

        /// <summary>
        /// Gets a unit by name from registry.
        /// </summary>
        /// <param name="unitName"></param>
        /// <returns>Unit if unit with name in registry</returns>
        /// <exception cref="KeyNotFoundException"></exception>
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

        /// <summary>
        /// Gets base unit for given dimension.
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns>Base unit of dimension</returns>
        public static Unit GetBaseUnit(Dimension dimension)
        {
            return units.First(u => u.Value.IsReferenceUnit && u.Value.Dimension == dimension).Value;
        }
    }
}
