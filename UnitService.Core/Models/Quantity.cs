using System;
using UnitService.Core.Exceptions;

namespace UnitService.Core.Models
{
    /// <summary>
    /// An abstraction of a physical quantity
    /// </summary>
    public struct Quantity : IEquatable<Quantity>
    {
        /// <summary>
        /// This is the constructor used to create a quantity
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="unit"></param>
        public Quantity(double? magnitude, Unit unit)
        {
            Unit = unit;
            Magnitude = magnitude;
        }

        #region Properties
        /// <summary>
        /// This is the Unit of the quantity.
        /// </summary>
        public Unit Unit { get; set; }
        /// <summary>
        /// This is the magnitude of the quantity. 
        /// NB: Not absolute value.
        /// </summary>
        public double? Magnitude { get; set; }
        /// <summary>
        /// This is the dimension of the quantity.
        /// </summary>
        public Dimension Dimension => Unit.Dimension;
        #endregion

        #region Methods
        /// <summary>
        /// This converts the quantity to its equivalent in the base unit.
        /// </summary>
        /// <returns>Quantity in base unit</returns>
        /// <exception cref="DimensionsMismatchException"></exception>
        /// <exception cref="InvalidReferenceUnitRelationshipException"></exception>
        public Quantity ConvertToBaseUnit()
        {
            if (Unit.IsReferenceUnit) return this;

            if (Unit.ReferenceUnitRelationship.Scale == 0) throw new InvalidReferenceUnitRelationshipException(Unit);

            var quantityMagnitudeInBaseUnit = 1 / Unit.ReferenceUnitRelationship.Scale *
                (Magnitude - Unit.ReferenceUnitRelationship.Offset);

            var baseUnit = UnitRegistry.GetBaseUnit(Dimension);

            return new Quantity(quantityMagnitudeInBaseUnit, baseUnit);
        }

        /// <summary>
        /// This converts a quantity to specified 'to unit'.
        /// </summary>
        /// <param name="otherUnit"></param>
        /// <returns>Quantity in 'to unit'</returns>
        /// <exception cref="DimensionsMismatchException"></exception>
        /// <exception cref="InvalidReferenceUnitRelationshipException"></exception>
        public Quantity ConvertTo(Unit otherUnit)
        {
            if (!Unit.IsConvertableTo(otherUnit)) throw new DimensionsMismatchException();

            var quantityInBaseUnit = ConvertToBaseUnit();

            if (otherUnit.IsReferenceUnit) return quantityInBaseUnit;

            var magnitudeInToUnit = otherUnit.ReferenceUnitRelationship.Scale * quantityInBaseUnit.Magnitude +
                otherUnit.ReferenceUnitRelationship.Offset;

            return new Quantity(magnitudeInToUnit, otherUnit);
        }

        /// <summary>
        /// This converts a quantity to specified unit given as string.
        /// </summary>
        /// <param name="unitAsString"></param>
        /// <returns>Quantity in 'to unit'</returns>
        /// <exception cref="DimensionsMismatchException"></exception>
        public Quantity ConvertTo(string unitAsString)
        {
            var toUnit = UnitRegistry.GetUnit(unitAsString);
            return ConvertTo(toUnit);
        }

        /// <summary>
        /// This converts a quantity to a specified unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="qty"></param>
        /// <returns>Quantity in 'to unit'</returns>
        /// <exception cref="InvalidReferenceUnitRelationshipException"></exception>
        public bool TryConvertTo(Unit unit, out Quantity qty)
        {
            try
            {
                qty = ConvertTo(unit);
                return true;
            }
            catch (DimensionsMismatchException)
            {
                qty = default;
                return false;
            }
        }

        /// <summary>
        /// This is used to get the string representation of a quantity.
        /// </summary>
        /// <returns>String representation of the quantity</returns>
        public override string ToString() => $"{Magnitude} {Unit.Symbol}";
        #endregion

        #region Operators
        /// <summary>
        /// This is used for multiplying a quantity and a double.
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="number"></param>
        /// <returns>Quantity</returns>
        public static Quantity operator *(Quantity quantity, double number)
        {
            return new Quantity(quantity.Magnitude.GetValueOrDefault() * number,
                quantity.Unit);
        }

        /// <summary>
        /// This is used for multiplying a double and a quantity.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static Quantity operator *(double number, Quantity quantity) => quantity * number;

        /// <summary>
        /// This is used for adding quantities.
        /// </summary>
        /// <param name="qty1"></param>
        /// <param name="qty2"></param>
        /// <returns>A quantity that is the result of the operation</returns>
        /// <exception cref="DimensionsMismatchException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static Quantity operator +(Quantity qty1, Quantity qty2)
        {
            // Not possible to add quantities of different dimensions
            if (!qty1.Unit.IsConvertableTo(qty2.Unit)) throw new DimensionsMismatchException();

            // Check if the quantities both have valid magnitudes
            if (!qty1.Magnitude.HasValue || !qty2.Magnitude.HasValue) throw new ArgumentNullException();

            // if quantities have the same unit, there is no need for a conversion
            if (qty1.Unit == qty2.Unit)
            {
                return new Quantity(qty1.Magnitude + qty2.Magnitude, qty1.Unit);
            }

            // Use unit of left qty1 left hand for now
            return qty1 + qty2.ConvertTo(qty1.Unit);
        }

        /// <summary>
        /// This is used for subtracting quantities.
        /// </summary>
        /// <param name="qty"></param>
        /// <returns>A quantity that is the result of the operation</returns>
        public static Quantity operator -(Quantity qty)
        {
            return new Quantity(-qty.Magnitude, qty.Unit);
        }

        /// <summary>
        /// This is used for subtracting quantities.
        /// </summary>
        /// <param name="qty1"></param>
        /// <param name="qty2"></param>
        /// <returns>A quantity that is the result of the operation</returns>
        public static Quantity operator -(Quantity qty1, Quantity qty2) => qty1 + -qty2;

        /// <summary>
        /// This is used for testing equality between quantities.
        /// </summary>
        /// <param name="qty1"></param>
        /// <param name="qty2"></param>
        /// <returns>True if quantities are equal else false</returns>
        public static bool operator ==(Quantity qty1, Quantity qty2) => qty1.Equals(qty2);

        /// <summary>
        /// This is used for testing non-equality between quantities
        /// </summary>
        /// <param name="qty1"></param>
        /// <param name="qty2"></param>
        /// <returns>True if quantities are not equal else false</returns>
        public static bool operator !=(Quantity qty1, Quantity qty2) => !(qty1 == qty2);

        /// <summary>
        /// This is used for converting a quantity to a double
        /// </summary>
        /// <param name="quantity"></param>
        public static implicit operator double(Quantity quantity)
        {
            return quantity.Magnitude.GetValueOrDefault();
        }
        #endregion

        #region Equality
        /// <summary>
        /// Checks for object equality
        /// </summary>
        /// <param name="otherQty"></param>
        /// <returns>True if objects are equal</returns>
        public bool Equals(Quantity otherQty)
        {
            if (!Magnitude.HasValue || !otherQty.Magnitude.HasValue) return false;

            return Magnitude.Value == otherQty.Magnitude.Value &&
                Dimension == otherQty.Dimension &&
                Unit == otherQty.Unit;
        }

        /// <summary>
        /// Checks for object equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if objects are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Quantity q)
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
            return HashCode.Combine(Magnitude, Unit);
        }
        #endregion
    }
}
