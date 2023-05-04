﻿using System;
using System.Collections.Generic;

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
        public string Dimensionality => CurrentUnit.Dimensionality;
        #endregion

        public Quantity ConvertTo(Unit unit)
        {
            // Check if dimensions are consistent
            if (!CurrentUnit.HasSameDimensionAs(unit)) throw new Exception();

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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }

    public readonly struct Dimension : IEquatable<Dimension>
    {
        private const string LENGTH = "[Length]", TIME = "[Time]",
            MASS = "[Mass]", TEMPERATURE = "[Temperature]", CURRENT = "[Current]";
        private readonly double? _lengthExp, _timeExp, _massExp, _currentExp, _tempExp;

        public Dimension(double? lengthExp, 
            double? timeExp,
            double? massExp, 
            double? currentExp, 
            double? tempExp)
        {
            _lengthExp = lengthExp;
            _timeExp = timeExp;
            _massExp = massExp;
            _currentExp = currentExp;
            _tempExp = tempExp;
        }

        public Dictionary<string, double?> GetComponentDimensions()
        {
            Dictionary<string, double?> dim = new Dictionary<string, double?>
            {
                [LENGTH] = _lengthExp,
                [TIME] = _timeExp,
                [MASS] = _massExp,
                [TEMPERATURE] = _tempExp,
                [CURRENT] = _currentExp
            };
            return dim;
        }

        #region Operators
        public static bool operator ==(Dimension dim1, Dimension dim2)
        {
            return dim1.Equals(dim2);
        }

        public static bool operator !=(Dimension dim1, Dimension dim2)
        {
            return !(dim1 == dim2);
        }
        #endregion

        #region Equality
        public bool Equals(Dimension other) => ToString() == other.ToString();

        public override bool Equals(object obj)
        {
            if (obj is Dimension d) 
            { 
                return Equals(obj);
            }
            return false;
        }
        #endregion
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string dim = _lengthExp.HasValue && _lengthExp.Value != 0 ? LENGTH+"^" + _lengthExp.Value : "";
            dim += _timeExp.HasValue && _timeExp.Value != 0 ? TIME+"^" + _timeExp.Value : "";
            dim += _massExp.HasValue && _massExp.Value != 0 ? MASS+"^" + _massExp.Value : "";
            dim += _currentExp.HasValue && _currentExp.Value != 0 ? CURRENT+"^" + _currentExp.Value : "";
            dim += _tempExp.HasValue && _tempExp.Value != 0 ? TEMPERATURE+"^" + _tempExp.Value : "";

            return dim;
        }
    }
}