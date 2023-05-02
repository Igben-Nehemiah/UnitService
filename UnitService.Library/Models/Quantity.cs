using System;

namespace UnitService.Library.Models
{
    public class Quantity
    {
        public Quantity(double? value, Unit unit)
        {
            CurrentUnit = unit;
            Value = value;
        }

        public Unit CurrentUnit { get; set; }
        public double? Value { get; set; }
        public string Dimensionality => CurrentUnit.Dimensionality;

        public Quantity To(Unit unit)
        {
            // Check if dimensions are consistent
            if (!CurrentUnit.HasSameDimensionAs(unit)) throw new Exception();

            throw new NotImplementedException();
        }


        public static Quantity operator *(Quantity quantity, double number)
        {
            return new Quantity(quantity.Value.GetValueOrDefault() * number,
                quantity.CurrentUnit);
        }

        public static Quantity operator *(double number, Quantity quantity) => quantity * number;
        
        public Quantity ToBaseUnit()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"Quantity(value:{Value}, unit:{CurrentUnit})";
    }
}
