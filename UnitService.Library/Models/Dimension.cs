using System;
using System.Collections.Generic;
using UnitService.Library.Constants;

namespace UnitService.Library.Models
{
    public struct Dimension
    {
        public Dimension(double lengthExp, 
            double timeExp, 
            double massExp, 
            double currentExp, 
            double tempExp)
        {
            LengthExp = lengthExp;
            TimeExp = timeExp;
            MassExp = massExp;
            CurrentExp = currentExp;
            TempExp = tempExp;
        }

        #region Properties and Fields

        public double LengthExp, TimeExp, MassExp, CurrentExp, TempExp;
        private static readonly string NONE = "None";
        private static readonly string LENGTH = "[Length]";
        private static readonly string TIME = "[Time]";
        private static readonly string MASS = "[Mass]";
        private static readonly string CURRENT = "[Current]";
        private static readonly string TEMPERATURE = "[Temperature]";
        #endregion

        #region Methods
        private static Dimension ParseLiteral(string dimensionStr)
        {
            dimensionStr = dimensionStr.Trim();
            if (string.IsNullOrEmpty(dimensionStr)) // Check later
                throw new Exception();

            Dictionary<string, double> dimensionDictionary = new Dictionary<string, double>
            {
                [LENGTH] = 0,
                [TIME] = 0,
                [MASS] = 0,
                [TEMPERATURE] = 0,
                [CURRENT] = 0,
            };

            if (dimensionStr == "1")
                return new Dimension();

            int currentLeftSquareBracketIndex = dimensionStr.IndexOf("[");
            int currentRightSquareBracketIndex = dimensionStr.IndexOf("]");

            while (currentLeftSquareBracketIndex > -1 && currentRightSquareBracketIndex > -1)
            {
                string dimensionKey = PadWithSquareBracket(
                    dimensionStr.Substring(currentLeftSquareBracketIndex + 1,
                    currentRightSquareBracketIndex - currentLeftSquareBracketIndex - 1));

                if (!dimensionDictionary.ContainsKey(dimensionKey))
                    throw new Exception("Unregistered dimension specified");

                if (currentRightSquareBracketIndex > 0 &&
                    currentRightSquareBracketIndex < dimensionStr.Length) // there are still more characters
                {
                    int nextLeftSquareBracketIndex = dimensionStr.IndexOf("[", currentRightSquareBracketIndex + 1);

                    string exponentPart = nextLeftSquareBracketIndex == -1 ?
                        dimensionStr.Substring(currentRightSquareBracketIndex + 1,
                            dimensionStr.Length - currentRightSquareBracketIndex - 1).Trim()
                        : dimensionStr.Substring(currentRightSquareBracketIndex + 1,
                            nextLeftSquareBracketIndex - currentRightSquareBracketIndex - 1).Trim();


                    double exponent = string.IsNullOrEmpty(exponentPart) ? 1 
                        : double.Parse(StripOffBrackets(exponentPart[1..]));

                    dimensionDictionary[dimensionKey] = exponent;
                }

                currentLeftSquareBracketIndex = dimensionStr.IndexOf("[", currentLeftSquareBracketIndex + 1);
                currentRightSquareBracketIndex = dimensionStr.IndexOf("]", currentRightSquareBracketIndex + 1);
            };

            return new Dimension(lengthExp: dimensionDictionary[LENGTH],
                timeExp: dimensionDictionary[TIME],
                massExp: dimensionDictionary[MASS],
                currentExp: dimensionDictionary[CURRENT],
                tempExp: dimensionDictionary[TEMPERATURE]);

            static string PadWithSquareBracket(string str) => "[" + str + "]";
            static string StripOffBrackets(string str)
            {
                if (string.IsNullOrEmpty(str)) return str;
                // This should be re-written
                if (str[0] == '(' && str[^1] == ')')
                {
                    return str[1..(str.Length - 1)];
                }
                return str;
            }
        }

        public static Dimension Parse(string dim)
        {
            string[] parts = dim.Split('/');

            var numerator = ParseLiteral(parts[0]);

            var denominator = parts.Length > 1 ? ParseLiteral(parts[1]) : default;

            return numerator/denominator;
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

        public void Deconstruct(out double lengthExp, 
            out double timeExp, 
            out double massExp, 
            out double currentExp, 
            out double tempExp)
        {
            lengthExp = LengthExp;
            timeExp = TimeExp;
            massExp = MassExp;
            currentExp = CurrentExp;
            tempExp = TempExp;
        }
        #endregion

        #region Operators
        public static Dimension operator *(Dimension first, Dimension second)
        {
            return (
                first.LengthExp + second.LengthExp,
                first.TimeExp + second.TimeExp,
                first.MassExp + second.MassExp,
                first.CurrentExp + second.CurrentExp,
                first.TempExp + second.TempExp);
        }

        public static Dimension operator /(Dimension first, Dimension second)
        {
            return (
                first.LengthExp - second.LengthExp,
                first.TimeExp - second.TimeExp,
                first.MassExp - second.MassExp,
                first.CurrentExp - second.CurrentExp,
                first.TempExp - second.TempExp);
        }

        public static bool operator ==(Dimension dim1, Dimension dim2)
        {
            return dim1.Equals(dim2);
        }

        public static bool operator !=(Dimension dim1, Dimension dim2)
        {
            return !(dim1 == dim2);
        }

        public static implicit operator string(Dimension dimension) => dimension.ToString();

        public static implicit operator Dimension(string dimensionString) => Parse(dimensionString);

        public static implicit operator (double LengthExp, double TimeExp, double MassExp, double CurrentExp, double TempExp)
            (Dimension value)
        {
            return (value.LengthExp, value.TimeExp, value.MassExp, value.CurrentExp, value.TempExp);
        }

        public static implicit operator Dimension((double LengthExp, double TimeExp, double MassExp, double CurrentExp, double TempExp) value)
        {
            return new Dimension(value.LengthExp, value.TimeExp, value.MassExp, value.CurrentExp, value.TempExp);
        }
        #endregion

        #region Equality
        public override bool Equals(object? obj)
        {
            return obj is Dimension other &&
                   LengthExp == other.LengthExp &&
                   TimeExp == other.TimeExp &&
                   MassExp == other.MassExp &&
                   CurrentExp == other.CurrentExp &&
                   TempExp == other.TempExp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LengthExp, TimeExp, MassExp, CurrentExp, TempExp);
        }
        #endregion

        public override string ToString()
        {
            if (this == (0, 0, 0, 0, 0)) return NONE;

            string dim = LengthExp != 0 ? LENGTH + (LengthExp == 1 ? "" : "^" + LengthExp) : "";
            dim += TimeExp != 0 ? TIME + (TimeExp == 1 ? "" : "^" + TimeExp) : "";
            dim += MassExp != 0 ? MASS + (MassExp == 1 ? "" : "^" + MassExp) : "";
            dim += CurrentExp != 0 ? CURRENT + (CurrentExp == 1 ? "" : "^" + CurrentExp) : "";
            dim += TempExp != 0 ? TEMPERATURE + (TempExp == 1 ? "" : "^" + TempExp) : "";

            return dim;
        }
    }
}
