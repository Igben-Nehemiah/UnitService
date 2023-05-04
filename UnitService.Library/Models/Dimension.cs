using System;
using System.Collections.Generic;
using UnitService.Library.Constants;

namespace UnitService.Library.Models
{
    public readonly struct Dimension : IEquatable<Dimension>
    {
        private readonly double? _lengthExp, _timeExp, _massExp, _currentExp, _tempExp;

        public static Dimension Length = new Dimension();
        public static Dimension Mass = new Dimension();
        public static Dimension Time = new Dimension();
        public static Dimension Current = new Dimension();
        public static Dimension Temperature = new Dimension();

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
        
        //public Dimension(string dimensionString)
        //{

        //}

        private string PadWithSquareBracket(string str) => "[" + str + "]";

        private void _ParseLiteral(string dimensionStr)
        {
            dimensionStr = dimensionStr.Trim();
            if (string.IsNullOrEmpty(dimensionStr) || dimensionStr.Length < 5) // Check later
                throw new Exception();

            // [Length]^3[Mass][Time]^(-1/3)

            Dictionary<string, double?> dimensionDictionary = new Dictionary<string, double?>
            {
                [Dimensions.LENGTH] = 0,
                [Dimensions.TIME] = 0,
                [Dimensions.MASS] = 0,
                [Dimensions.TEMPERATURE] = 0,
                [Dimensions.CURRENT] = 0
            };

            int currentLeftSquareBracketIndex = -1, currentRightSquareBracketIndex = -1;

            do
            {
                currentLeftSquareBracketIndex = dimensionStr.IndexOf("[", currentLeftSquareBracketIndex + 1);
                currentRightSquareBracketIndex = dimensionStr.IndexOf("]", currentRightSquareBracketIndex + 1);

                string dimensionKey = PadWithSquareBracket(
                    dimensionStr.Substring(currentLeftSquareBracketIndex + 1,
                    currentRightSquareBracketIndex - currentLeftSquareBracketIndex - 1));

                if (!dimensionDictionary.ContainsKey(dimensionKey))
                    throw new Exception("Unregistered dimension specified");

                if (currentRightSquareBracketIndex > 0 && 
                    currentRightSquareBracketIndex < dimensionStr.Length - 1) // there are still more characters
                {
                    int nextLeftSquareBracketIndex = dimensionStr.IndexOf("[", currentRightSquareBracketIndex + 1);

                    string exponentPart = nextLeftSquareBracketIndex == -1 ?
                        dimensionStr.Substring(currentRightSquareBracketIndex + 1,
                            dimensionStr.Length - currentRightSquareBracketIndex - 2).Trim()
                        : dimensionStr.Substring(currentRightSquareBracketIndex + 1,
                            nextLeftSquareBracketIndex - currentLeftSquareBracketIndex - 1).Trim();

                    double exponent = string.IsNullOrEmpty(exponentPart) ? 1 : double.Parse(exponentPart[1..]);

                    dimensionDictionary[dimensionKey] = exponent;
                }
            }
            while (currentLeftSquareBracketIndex > -1 && currentRightSquareBracketIndex > -1);
        }

        private Dimension _Parse(string dim)
        {
            string[] parts = dim.Split('/');

            if (parts.Length == 1)
            {
                // No denominator
            }

            string numerator = parts[0];
            string? denominator = parts.Length == 2 ? parts[1] : null;


            throw new NotImplementedException();
        }

        private Dimension Parse(string dim)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(string dimString, out Dimension dim)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double?> GetComponentDimensions()
        {
            Dictionary<string, double?> dim = new Dictionary<string, double?>
            {
                [Dimensions.LENGTH] = _lengthExp,
                [Dimensions.TIME] = _timeExp,
                [Dimensions.MASS] = _massExp,
                [Dimensions.TEMPERATURE] = _tempExp,
                [Dimensions.CURRENT] = _currentExp
            };
            return dim;
        }

        #region Operators
        public static Dimension operator /(Dimension dim1, Dimension dim2)
        {
            throw new NotImplementedException();
        }

        public static Dimension operator /(Dimension dim1, double constant)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Dimension dim1, Dimension dim2)
        {
            return dim1.Equals(dim2);
        }

        public static bool operator !=(Dimension dim1, Dimension dim2)
        {
            return !(dim1 == dim2);
        }

        public static implicit operator string(Dimension d)
        {
            return string.Empty;
        }

        public static explicit operator Dimension(string dimensionString)
        {
            return new Dimension();
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
            string dim = _lengthExp.HasValue && _lengthExp.Value != 0 ? Dimensions.LENGTH +"^" + _lengthExp.Value : "";
            dim += _timeExp.HasValue && _timeExp.Value != 0 ? Dimensions.TIME +"^" + _timeExp.Value : "";
            dim += _massExp.HasValue && _massExp.Value != 0 ? Dimensions.MASS +"^" + _massExp.Value : "";
            dim += _currentExp.HasValue && _currentExp.Value != 0 ? Dimensions.CURRENT +"^" + _currentExp.Value : "";
            dim += _tempExp.HasValue && _tempExp.Value != 0 ? Dimensions.TEMPERATURE +"^" + _tempExp.Value : "";

            return dim;
        }
    }
}
