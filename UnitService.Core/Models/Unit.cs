using System;

namespace UnitService.Core.Models
{
    /// <summary>
    /// An abstraction of a unit.
    /// </summary>
    public struct Unit : IEquatable<Unit>
    {
        /// <summary>
        /// Constructor of a unit
        /// </summary>
        /// <param name="name"></param>
        /// <param name="symbol"></param>
        /// <param name="baseUnitRelationship"></param>
        /// <param name="dimension"></param>
        public Unit(string name,
            string symbol,
            (double Multiplier, double Offset) baseUnitRelationship = default,
            Dimension dimension = default)
        {
            Name = name;
            Symbol = symbol;
            Dimension = dimension;
            ReferenceUnitRelationship = baseUnitRelationship;
        }

        #region Properties
        /// <summary>
        /// This is the unit's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// This is the unit's symbol.
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// This is the units dimension.
        /// </summary>
        public Dimension Dimension { get; set; }
        /// <summary>
        /// This is defines the relationship between unit and reference unit.
        /// </summary>
        public (double M, double C) ReferenceUnitRelationship { get; }
        /// <summary>
        /// Is true if unit is reference unit.
        /// </summary>
        public bool IsReferenceUnit => ReferenceUnitRelationship == (1, 0);
        #endregion

        #region Methods
        /// <summary>
        /// Checks if unit has same dimension as other unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>True if dimensions are true</returns>
        public bool HasSameDimensionAs(Unit unit) => Dimension == unit.Dimension;

        //public static Unit CreateFrom(string name,
        //    string symbol,
        //    UnitsRelation unitsRelationship)
        //{
        //    return new Unit(name, 
        //        symbol, 
        //        (unitsRelationship.ReferenceUnit.Magnitude, ),
        //        unitsRelationship.ReferenceUnit.Dimension);
        //}

        /// <summary>
        /// Gets the string representation of a unit.
        /// </summary>
        /// <returns>String representation of unit.</returns>
        public override string ToString() => Symbol;
        #endregion

        #region Operators
        /// <summary>
        /// Checks if units are the same.
        /// </summary>
        /// <param name="unit1"></param>
        /// <param name="unit2"></param>
        /// <returns>True if units are the same else false.</returns>
        public static bool operator ==(Unit unit1, Unit unit2) => unit1.Equals(unit2);

        /// <summary>
        /// Checks if units are not the same.
        /// </summary>
        /// <param name="unit1"></param>
        /// <param name="unit2"></param>
        /// <returns>Returns true if units are not the same else false.</returns>
        public static bool operator !=(Unit unit1, Unit unit2) => !(unit1 == unit2);

        /// <summary>
        /// Multiplies two units.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Result unit.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Unit operator *(Unit left, Unit right)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Equality
        /// <summary>
        /// Checks for object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if objects are equal</returns>
        public bool Equals(Unit other)
        {
            return Name == other.Name
                && Symbol == other.Symbol
                && Dimension == other.Dimension;
        }

        /// <summary>
        /// Checks for object equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if objects are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Unit q)
            {
                q.Equals(this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the hashcode of the quantity
        /// </summary>
        /// <returns>Number representing the hashcode of the quantity</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Symbol, Dimension);
        }
        #endregion
    }

    internal struct UnitsRelation
    {

        public UnitsRelation(double multiplier,
            double offset, 
            Unit referenceUnit)
        {
            Multiplier = multiplier;
            Offset = offset;
            ReferenceUnit = referenceUnit;
        }

        public double Multiplier { get; set; }
        public double Offset { get; set; }
        public Unit ReferenceUnit { get; set; }
    }
}
