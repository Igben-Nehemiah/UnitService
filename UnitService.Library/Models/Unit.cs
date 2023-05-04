using System;

namespace UnitService.Library.Models
{
    public struct Unit : IEquatable<Unit>
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
                && Dimensionality == other.Dimensionality;
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
            return base.GetHashCode();
        }
        #endregion
    }
}
