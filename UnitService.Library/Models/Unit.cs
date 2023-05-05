using System;

namespace UnitService.Library.Models
{
    public struct Unit : IEquatable<Unit>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Dimension Dimension { get; set; }
        public Unit(string name, string symbol, Dimension dimension = default)
        {
            Name = name;
            Symbol = symbol;
            Dimension = dimension;    
        }

        public bool HasSameDimensionAs(Unit unit) => Dimension == unit.Dimension;


        public static bool TryParse(string unitAsString, out Unit unit)
        {
            throw new NotImplementedException();
        }

        #region Operators
        public static Quantity operator *(double number, Unit unit) => new Quantity(number, unit);

        public static bool operator ==(Unit unit1, Unit unit2) => unit1.Equals(unit2);

        public static bool operator !=(Unit unit1, Unit unit2) => !(unit1 == unit2);

        public static Unit operator *(Unit left, Unit right)
        {
            //return new Unit();
            throw new NotImplementedException();
        }
        #endregion

        #region Equality
        public bool Equals(Unit other)
        {
            return Name == other.Name
                && Symbol == other.Symbol
                && Dimension == other.Dimension;
        }

        public override bool Equals(object obj)
        {
            if (obj is Unit q)
            {
                q.Equals(this);
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Symbol, Dimension);
        }
        #endregion
    }
}
