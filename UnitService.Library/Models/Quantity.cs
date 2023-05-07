using System;

namespace UnitService.Library.Models
{
    public struct Quantity : ICloneable, IEquatable<Quantity>
    {
        public Quantity(double? magnitude, Unit unit)
        {
            Unit = unit;
            Magnitude = magnitude;
        }

        #region Properties
        public Unit Unit { get; set; }
        public double? Magnitude { get; set; }
        public Dimension Dimension => Unit.Dimension;
        #endregion

        #region Methods
        public Quantity ConvertToBaseUnit()
        {
            if (Unit.IsBaseUnit) return this;

            var quantityMagnitudeInBaseUnit = 1 / Unit.BaseUnitRelationship.M *
                (Magnitude - Unit.BaseUnitRelationship.C);

            var baseUnit = UnitRegistry.GetBaseUnit(Dimension);

            return new Quantity(quantityMagnitudeInBaseUnit, baseUnit);
        }
        public Quantity ConvertTo(Unit toUnit)
        {
            if (!Unit.HasSameDimensionAs(toUnit)) throw new Exception();

            var quantityInBaseUnit = ConvertToBaseUnit();

            var magnitudeInToUnit = toUnit.BaseUnitRelationship.M * quantityInBaseUnit.Magnitude +
                toUnit.BaseUnitRelationship.C;

            return new Quantity(magnitudeInToUnit, toUnit);
        }

        public Quantity ConvertTo(string unitAsString)
        {
            var toUnit = UnitRegistry.GetUnit(unitAsString);
            return ConvertTo(toUnit);
        }

        public bool TryConvertTo(Unit unit, out Quantity qty)
        {
            try
            {
                qty = ConvertTo(unit);
                return true;
            }
            catch (Exception)
            {
                qty = default;
                return false;
            }
        }

        public object Clone() => new Quantity(Magnitude, Unit);

        public override string ToString() => $"{Magnitude} {Unit.Symbol}";
        #endregion

        #region Operators
        public static Quantity operator *(Quantity quantity, double number)
        {
            return new Quantity(quantity.Magnitude.GetValueOrDefault() * number,
                quantity.Unit);
        }

        public static Quantity operator *(double number, Quantity quantity) => quantity * number;
        
        public static Quantity operator +(Quantity qty1, Quantity qty2)
        {
            // Not possible to add quantities of different dimensions
            if (!qty1.Unit.HasSameDimensionAs(qty2.Unit)) throw new Exception();

            // Check if the quantities both have valid magnitudes
            if (!qty1.Magnitude.HasValue || !qty2.Magnitude.HasValue) throw new Exception();

            // if quantities have the same unit, there is no need for a conversion
            if (qty1.Unit == qty2.Unit)
            {
                return new Quantity(qty1.Magnitude + qty2.Magnitude, qty1.Unit);
            }

            // Use unit of left qty1 left hand for now
            return qty1 + qty2.ConvertTo(qty1.Unit);
        }

        public static Quantity operator -(Quantity qty)
        {
            return new Quantity(-qty.Magnitude, qty.Unit);
        }

        public static Quantity operator -(Quantity qty1, Quantity qty2) => qty1 + (-qty2);

        public static bool operator ==(Quantity qty1, Quantity qty2) => qty1.Equals(qty2);

        public static bool operator !=(Quantity qty1, Quantity qty2) => !(qty1 == qty2);

        public static implicit operator double(Quantity quantity)
        {
            return quantity.Magnitude.GetValueOrDefault();
        }
        #endregion

        #region Equality
        public bool Equals(Quantity otherQty)
        {
            if (!Magnitude.HasValue || !otherQty.Magnitude.HasValue) return false;

            return Magnitude.Value == otherQty.Magnitude.Value &&
                Dimension == otherQty.Dimension &&
                Unit == otherQty.Unit;
        }

        public override bool Equals(object obj)
        {
            if (obj is Quantity q)
            {
                q.Equals(this); 
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Magnitude, Unit);
        }
        #endregion
    }
}
