using System;

namespace UnitService.Library.Models
{
    public class Unit : IEquatable<Unit>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Dimensionality { get; set; }
        public Unit(string name, string symbol, string dimensionality)
        {
            Name = name;
            Symbol = symbol;
            Dimensionality = dimensionality;    
        }

        public bool HasSameDimensionAs(Unit unit) => Dimensionality == unit.Dimensionality;

        
        public static Unit operator *(Unit left, Unit right)
        {
            //return new Unit();
            throw new NotImplementedException();
        }

        public static Quantity operator *(double number, Unit unit) => new Quantity(number, unit);

        #region Equality
        public bool Equals(Unit other)
        {
            return Name == other.Name
                && Symbol == other.Symbol
                && Dimensionality == other.Dimensionality;
        }
        #endregion
    }
}
