using System;
using System.Collections.Generic;
using UnitService.Library.Constants;

namespace UnitService.Library.Models
{
    public readonly struct Dimension : IEquatable<Dimension>
    {
        private readonly double? _lengthExp, _timeExp, _massExp, _currentExp, _tempExp;
        private readonly bool? _none;

        public static Dimension Length = new Dimension();
        public static Dimension Mass = new Dimension();
        public static Dimension Time = new Dimension();
        public static Dimension Current = new Dimension();
        public static Dimension Temperature = new Dimension();

        private Dimension(double? lengthExp = default, 
            double? timeExp = default,
            double? massExp = default, 
            double? currentExp = default, 
            double? tempExp = default,
            bool? none = default)
        {
            _lengthExp = lengthExp;
            _timeExp = timeExp;
            _massExp = massExp;
            _currentExp = currentExp;
            _tempExp = tempExp;
            _none = none;
        }

        //public Dimension(st): this(timeExp: 0, massExp: 0, lengthExp: 0, currentExp: 0, tempExp: 0, none: true)
        //{

        //}

        private static Dictionary<string, double> _ParseLiteral(string dimensionStr)
        {
            // [Length]^3[Mass][Time]^(-1/3)

            dimensionStr = dimensionStr.Trim();
            if (string.IsNullOrEmpty(dimensionStr)) // Check later
                throw new Exception();

            Dictionary<string, double> dimensionDictionary = new Dictionary<string, double>
            {
                [Dimensions.LENGTH] = 0,
                [Dimensions.TIME] = 0,
                [Dimensions.MASS] = 0,
                [Dimensions.TEMPERATURE] = 0,
                [Dimensions.CURRENT] = 0,
                [Dimensions.NONE] = 0
            };

            if (dimensionStr == "1")
            {
                dimensionDictionary[Dimensions.NONE] = 1;
                return dimensionDictionary;
            }

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

            return dimensionDictionary;

            static string PadWithSquareBracket(string str) => "[" + str + "]";
        }

        public static Dimension Parse(string dim)
        {
            string[] parts = dim.Split('/');

            var numerator = _ParseLiteral(parts[0]);

            var denominator = parts.Length > 1 ? _ParseLiteral(parts[1]) : null;

            var combined = new Dictionary<string, double>();

            // Check if numerator is one;

            foreach(var key in numerator.Keys)
            {
                combined[key] = numerator.GetValueOrDefault(key);
            }

            if (!(denominator == null))
            {
                foreach (var key in denominator.Keys)
                {
                    combined[key] -= denominator.GetValueOrDefault(key);
                }
            }

            return new Dimension(lengthExp: combined[Dimensions.LENGTH], 
                timeExp: combined[Dimensions.TIME],
                massExp: combined[Dimensions.MASS],
                currentExp: combined[Dimensions.CURRENT],
                tempExp: combined[Dimensions.TEMPERATURE]);
        }

        public static bool TryParse(string dimString, out Dimension dim)
        {
            try
            {
                dim = Parse(dimString);
                return true;
            }
            catch (Exception)
            {
                dim = default;
                return false;
            }
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
