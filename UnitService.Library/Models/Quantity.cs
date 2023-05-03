using System;

namespace UnitService.Library.Models
{
    public class Quantity : ICloneable, IEquatable<Quantity>
    {
        public Quantity(double? magnitude, Unit unit)
        {
            CurrentUnit = unit;
            Magnitude = magnitude;
        }

        public Unit CurrentUnit { get; set; }
        public double? Magnitude { get; set; }
        public string Dimensionality => CurrentUnit.Dimensionality;

        public bool HasSameUnit(Quantity otherQty) => CurrentUnit == otherQty.CurrentUnit;
        public Quantity To(Unit unit)
        {
            // Check if dimensions are consistent
            if (!CurrentUnit.HasSameDimensionAs(unit)) throw new Exception();

            throw new NotImplementedException();
        }

        public Quantity ToBaseUnit()
        {
            throw new NotImplementedException();
        }

        #region Operators
        public static Quantity operator *(Quantity quantity, double number)
        {
            return new Quantity(quantity.Magnitude.GetValueOrDefault() * number,
                quantity.CurrentUnit);
        }

        public static Quantity operator *(double number, Quantity quantity) => quantity * number;
        
        public static Quantity operator +(Quantity qty1, Quantity qty2)
        {
            if (!qty1.CurrentUnit.HasSameDimensionAs(qty2.CurrentUnit)) throw new Exception();

            // Use unit of left qty1 left hand for now
            var qty3 = qty1.To(qty2.CurrentUnit);
            return qty1 + qty3;
        }

        public static Quantity operator -(Quantity qty)
        {
            return new Quantity(-qty.Magnitude, qty.CurrentUnit);
        }

        public static bool operator ==(Quantity qty1, Quantity qty2) => qty1.Equals(qty2);

        public static bool operator !=(Quantity qty1, Quantity qty2) => !(qty1 == qty2);
        #endregion

        public object Clone() => new Quantity(Magnitude, CurrentUnit);

        public override string ToString() => $"Quantity(value:{Magnitude}, unit:{CurrentUnit})";


        #region Equality
        public bool Equals(Quantity otherQty)
        {
            if (!Magnitude.HasValue || !otherQty.Magnitude.HasValue) return false;

            return Magnitude.Value == otherQty.Magnitude.Value &&
                Dimensionality == otherQty.Dimensionality &&
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
        #endregion
    }
}
