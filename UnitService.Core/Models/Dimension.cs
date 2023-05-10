using System;
using System.Collections.Generic;
using UnitService.Core.Exceptions;

namespace UnitService.Core.Models
{
    /// <summary>
    /// This is an abstraction a Dimension
    /// </summary>
    public struct Dimension : IEquatable<Dimension>
    {
        /// <summary>
        /// Creates a Dimension from exponents of fundamental quantities.
        /// </summary>
        /// <param name="lengthExp"></param>
        /// <param name="timeExp"></param>
        /// <param name="massExp"></param>
        /// <param name="currentExp"></param>
        /// <param name="tempExp"></param>
        /// <param name="luminousIntensityExp"></param>
        /// <param name="amountOfSubstanceExp"></param>
        public Dimension(double lengthExp = 0,
            double timeExp = 0,
            double massExp = 0,
            double currentExp = 0,
            double tempExp = 0,
            double luminousIntensityExp = 0,
            double amountOfSubstanceExp = 0)
        {
            LengthExp = lengthExp;
            TimeExp = timeExp;
            MassExp = massExp;
            CurrentExp = currentExp;
            TempExp = tempExp;
            LuminousIntensityExp = luminousIntensityExp;
            AmountOfSubstanceExp = amountOfSubstanceExp;
        }

        #region Properties and Fields

        /// <summary>
        /// Dimension exponent
        /// </summary>
        public double LengthExp, TimeExp, MassExp, CurrentExp,
            TempExp, LuminousIntensityExp, AmountOfSubstanceExp;
        private static readonly string NONE = "None";
        private static readonly string LENGTH = "[Length]";
        private static readonly string TIME = "[Time]";
        private static readonly string MASS = "[Mass]";
        private static readonly string CURRENT = "[Current]";
        private static readonly string TEMPERATURE = "[Temperature]";
        private static readonly string LUMINOUS_INTENSITY = "[LUMINOUS_INT]";
        private static readonly string AMOUNT_OF_SUBSTANCE = "[Temperature]";
        #endregion

        #region Methods
        /// <summary>
        /// Parses a dimension string to a dimension.
        /// </summary>
        /// <param name="dimensionStr"></param>
        /// <returns>The Parsed Dimension</returns>
        /// <exception cref="DimensionParseException"></exception>
        private static Dimension ParseLiteral(string dimensionStr)
        {
            try
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
                    [LUMINOUS_INTENSITY] = 0,
                    [AMOUNT_OF_SUBSTANCE] = 0
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
                    tempExp: dimensionDictionary[TEMPERATURE],
                    luminousIntensityExp: dimensionDictionary[LUMINOUS_INTENSITY],
                    amountOfSubstanceExp: dimensionDictionary[AMOUNT_OF_SUBSTANCE]);
            }
            catch (Exception e)
            {
                throw new DimensionParseException($"Could not parse string '{dimensionStr}' as dimension", e);
            }

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

        /// <summary>
        /// This is used to Parse a valid string as a dimension.
        /// </summary>
        /// <param name="dim"></param>
        /// <returns>The Parsed Dimension</returns>
        /// <exception cref="DimensionParseException"></exception>
        public static Dimension Parse(string dim)
        {
            string[] parts = dim.Split('/');

            var numerator = ParseLiteral(parts[0]);

            var denominator = parts.Length > 1 ? ParseLiteral(parts[1]) : default;

            return numerator / denominator;
        }

        /// <summary>
        /// This is used to Parse a valid string as a dimension.
        /// </summary>
        /// <param name="dimString"></param>
        /// <param name="dim"></param>
        /// <returns>The Parsed Dimension</returns>
        public static bool TryParse(string dimString, out Dimension dim)
        {
            try
            {
                dim = Parse(dimString);
                return true;
            }
            catch (DimensionParseException)
            {
                dim = default;
                return false;
            }
        }

        /// <summary>
        /// This is used for deconstructing a dimension into a tuple.
        /// <code>
        /// Example:
        /// (double lengthExponent, double timeExp, double massExp, double currentExp, double tempExp) = new Dimension();
        /// </code>
        /// </summary>
        /// <param name="lengthExp"></param>
        /// <param name="timeExp"></param>
        /// <param name="massExp"></param>
        /// <param name="currentExp"></param>
        /// <param name="tempExp"></param>
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

        /// <summary>
        /// Performs multiplication between dimensions.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>The Dimension that is the product of both dimensions</returns>
        #region Operators
        public static Dimension operator *(Dimension first, Dimension second)
        {
            return (
                first.LengthExp + second.LengthExp,
                first.TimeExp + second.TimeExp,
                first.MassExp + second.MassExp,
                first.CurrentExp + second.CurrentExp,
                first.TempExp + second.TempExp,
                first.LuminousIntensityExp + second.LuminousIntensityExp,
                first.AmountOfSubstanceExp + second.AmountOfSubstanceExp);
        }

        /// <summary>
        /// Performs division between dimensions.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>The Dimension that is obtained from the division</returns>
        public static Dimension operator /(Dimension first, Dimension second)
        {
            return (
                first.LengthExp - second.LengthExp,
                first.TimeExp - second.TimeExp,
                first.MassExp - second.MassExp,
                first.CurrentExp - second.CurrentExp,
                first.TempExp - second.TempExp,
                first.LuminousIntensityExp - second.LuminousIntensityExp,
                first.AmountOfSubstanceExp - second.AmountOfSubstanceExp);
        }

        /// <summary>
        /// Checks for equality between dimensions.
        /// </summary>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <returns>True if dimensions are equal else false</returns>
        public static bool operator ==(Dimension dim1, Dimension dim2) => dim1.Equals(dim2);

        /// <summary>
        /// Checks for non-equality between dimensions.
        /// </summary>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <returns>Returns true if dimensions are not equal else false</returns>
        public static bool operator !=(Dimension dim1, Dimension dim2) => !(dim1 == dim2);

        /// <summary>
        /// Converts dimension to string implicitly.
        /// </summary>
        /// <param name="dimension"></param>
        /// <exception cref="DimensionParseException"></exception>
        public static implicit operator string(Dimension dimension) => dimension.ToString();

        /// <summary>
        /// Converts string to dimension implicitly.
        /// </summary>
        /// <param name="dimensionString"></param>
        /// <exception cref="DimensionParseException"></exception>
        public static implicit operator Dimension(string dimensionString) => Parse(dimensionString);

        /// <summary>
        /// Converts dimension to tuple.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator (double LengthExp, double TimeExp,
            double MassExp, double CurrentExp, double TempExp,
            double LuminousIntensity, double AmountOfSubstance)(Dimension value)
        {
            return (value.LengthExp, value.TimeExp, value.MassExp, 
                value.CurrentExp, value.TempExp, value.LuminousIntensityExp, value.AmountOfSubstanceExp);
        }

        /// <summary>
        /// Converts tuple to dimension.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Dimension((double LengthExp, double TimeExp, 
            double MassExp, double CurrentExp, double TempExp, 
            double LuminousIntensity, double AmountOfSubstance) value)
        {
            return new Dimension(value.LengthExp, 
                value.TimeExp, value.MassExp, value.CurrentExp, 
                value.TempExp, value.LuminousIntensity, value.AmountOfSubstance);
        }
        #endregion

        #region Equality
        /// <summary>
        /// Checks for object equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if objects are equal</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Dimension d = (Dimension)obj;
                return Equals(d);
            }
        }

        /// <summary>
        /// Checks for object equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if objects are equal</returns>
        public bool Equals(Dimension other)
        {
            return LengthExp == other.LengthExp &&
                   TimeExp == other.TimeExp &&
                   MassExp == other.MassExp &&
                   CurrentExp == other.CurrentExp &&
                   TempExp == other.TempExp;
        }

        /// <summary>
        /// Gets the hashcode of the quantity
        /// </summary>
        /// <returns>Number representing the hashcode of the quantity</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(LengthExp, TimeExp, MassExp, CurrentExp, TempExp);
        }
        #endregion

        /// <summary>
        /// This is used to get the string representation of a dimension.
        /// </summary>
        /// <returns>A string that represents the dimension</returns>
        public override string ToString()
        {
            if (this == default) return NONE;

            string dim = LengthExp != 0 ? LENGTH + (LengthExp == 1 ? "" : "^" + LengthExp) : "";
            dim += TimeExp != 0 ? TIME + (TimeExp == 1 ? "" : "^" + TimeExp) : "";
            dim += MassExp != 0 ? MASS + (MassExp == 1 ? "" : "^" + MassExp) : "";
            dim += CurrentExp != 0 ? CURRENT + (CurrentExp == 1 ? "" : "^" + CurrentExp) : "";
            dim += TempExp != 0 ? TEMPERATURE + (TempExp == 1 ? "" : "^" + TempExp) : "";

            return dim;
        }
    }
}