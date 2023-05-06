using System;

namespace UnitService.Library.Models
{
    public struct Unit : IEquatable<Unit>
    {
        public Unit(string name, 
            string symbol,
            (double M, double C) baseUnitRelationship = default,
            Dimension dimension = default)
        {
            Name = name;
            Symbol = symbol;
            Dimension = dimension;
            BaseUnitRelationship = baseUnitRelationship;
        }

        #region Properties
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Dimension Dimension { get; set; }
        public (double M, double C) BaseUnitRelationship { get; }
        #endregion

        #region Methods
        public bool HasSameDimensionAs(Unit unit) => Dimension == unit.Dimension;
        #endregion

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
