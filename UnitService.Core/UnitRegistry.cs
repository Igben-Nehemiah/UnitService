using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnitService.Core.Extensions;
using UnitService.Core.Models;

namespace UnitService.Core
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
            LoadConstantsAsync().Await();
            LoadPrefixesAsync().Await();
            LoadUnitsAsync().Await();
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

        private static Task<Dictionary<string, string>> LoadAsync(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);

            Dictionary<string, string> registry = new Dictionary<string, string>();

            var line = reader.ReadLine();

            while (line != null) 
            { 
                if (!(string.IsNullOrEmpty(line) || line.StartsWith("#")))
                {
                    ExtractRecords(line);
                }
              
                line = reader.ReadLine();
            }

            return Task.FromResult(registry);

            void ExtractRecords(string line)
            {
                int indexOfHashChar = line.IndexOf('#');

                if (indexOfHashChar != -1) line = line[..indexOfHashChar];

                string[] parts = line.Split('=');

                if (parts.Length == 1) return;

                for (int i = 0; i < parts.Length; i++)
                {
                    if (i != 1)
                    {
                        var key = parts[i].Trim();
                        if (!string.IsNullOrEmpty(key) && key != "_") registry.Add(key, parts[1].Trim());
                    }
                }
            }
        }

        private async static Task LoadConstantsAsync()
        {
            await LoadAsync(_pathToConstantsFile);
        }

        private async static Task LoadPrefixesAsync()
        {
            await LoadAsync(_pathToPrefixesFile);
        }

        private async static Task LoadUnitsAsync()
        {
            await LoadAsync(_pathToDefaultUnitsFile);
        }

        private static string _pathToConstantsFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Constants.txt");
        private static string _pathToDefaultUnitsFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Defaults.txt");
        private static string _pathToPrefixesFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Prefixes.txt");
    }
}