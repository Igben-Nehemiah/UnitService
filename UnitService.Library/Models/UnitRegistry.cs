using System;

namespace UnitService.Library.Models
{
    public class UnitRegistry
    {
        private readonly UnitRegistryOptions _opts;

        public UnitRegistry(UnitRegistryOptions opts)
        {
            _opts = opts;
        }
        public void RegisterUnit(string name, Unit unit)
        {

        }

        public Unit GetUnit(string unitName)
        {
            throw new NotImplementedException();
        }
    }
}
