using System;

namespace UnitService.Library.Models
{
    public class Unit
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Dimensionality { get; set; }

        public bool HasSameDimensionAs(Unit unit) => Dimensionality == unit.Dimensionality;

        public static Unit operator *(Unit left, Unit right)
        {
            //return new Unit();
            throw new NotImplementedException();
        }

        public static Quantity operator *(double number, Unit unit) => new Quantity(number, unit);
    }
}
