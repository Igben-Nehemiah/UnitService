using System;

namespace UnitService.Library.Models
{
    public struct Quantity : ICloneable, IEquatable<Quantity>
    {
        public Quantity(double? magnitude, Unit unit)
        {
            CurrentUnit = unit;
            Magnitude = magnitude;
        }

        #region Properties
        public Unit CurrentUnit { get; set; }
        public double? Magnitude { get; set; }
        public Dimension Dimension => CurrentUnit.Dimension;
        #endregion Properties

        #region Methods
        public Quantity ConvertTo(Unit unit)
        {
            // Check if dimensions are consistent
            if (!CurrentUnit.HasSameDimensionAs(unit)) throw new Exception();

            throw new NotImplementedException();
        }

        public static Quantity Convert(Quantity quantity, Unit unit)
        {
            throw new NotImplementedException();
        }

        public Quantity ConvertTo(string unitAsString)
        {
            bool parsedUnit = Unit.TryParse(unitAsString, out Unit unit);
            // Check if dimensions are consistent
            if (!parsedUnit) throw new Exception();

            throw new NotImplementedException();
        }

        public bool TryConvertTo(Unit unit, out Quantity qty)
        {
            throw new NotImplementedException();
        }

        public Quantity ToBaseUnit()
        {
            throw new NotImplementedException();
        }

        public object Clone() => new Quantity(Magnitude, CurrentUnit);

        public override string ToString() => $"Quantity(value:{Magnitude}, unit:{CurrentUnit})";

        #endregion Methods

        #region Operators
        public static Quantity operator *(Quantity quantity, double number)
        {
            return new Quantity(quantity.Magnitude.GetValueOrDefault() * number,
                quantity.CurrentUnit);
        }

        public static Quantity operator *(double number, Quantity quantity) => quantity * number;
        
        public static Quantity operator +(Quantity qty1, Quantity qty2)
        {
            // Not possible to add quantities of different dimensions
            if (!qty1.CurrentUnit.HasSameDimensionAs(qty2.CurrentUnit)) throw new Exception();

            // Check if the quantities both have valid magnitudes
            if (!qty1.Magnitude.HasValue || !qty2.Magnitude.HasValue) throw new Exception();

            // if quantities have the same unit, there is no need for a conversion
            if (qty1.CurrentUnit == qty2.CurrentUnit)
            {
                return new Quantity(qty1.Magnitude + qty2.Magnitude, qty1.CurrentUnit);
            }


            // Use unit of left qty1 left hand for now
            return qty1 + qty2.ConvertTo(qty1.CurrentUnit);
        }

        public static Quantity operator -(Quantity qty)
        {
            return new Quantity(-qty.Magnitude, qty.CurrentUnit);
        }

        public static bool operator ==(Quantity qty1, Quantity qty2) => qty1.Equals(qty2);

        public static bool operator !=(Quantity qty1, Quantity qty2) => !(qty1 == qty2);
        #endregion Operators

        #region Equality
        public bool Equals(Quantity otherQty)
        {
            if (!Magnitude.HasValue || !otherQty.Magnitude.HasValue) return false;

            return Magnitude.Value == otherQty.Magnitude.Value &&
                Dimension == otherQty.Dimension &&
                CurrentUnit == otherQty.CurrentUnit;
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
            return HashCode.Combine(Magnitude, CurrentUnit);
        }
        #endregion Equality
    }
}
