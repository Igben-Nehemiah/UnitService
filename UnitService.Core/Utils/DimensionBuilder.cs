using UnitService.Core.Models;

namespace UnitService.Core.Utils
{
    /// <summary>
    /// A util struct for building a dimension fluently.
    /// </summary>
    public struct DimensionBuilder
    {
        private static Dimension _dimension = new Dimension();

        /// <summary>
        /// Adds temperature exponent to the built dimension.
        /// </summary>
        /// <param name="temperatureExponent"></param>
        /// <returns>DimensionBuilder</returns>
        public DimensionBuilder AddTemperature(double temperatureExponent)
        {
            _dimension.TempExp += temperatureExponent;
            return this;
        }

        /// <summary>
        /// Adds time exponent to the built dimension.
        /// </summary>
        /// <param name="timeExponent"></param>
        /// <returns>DimensionBuilder</returns>
        public DimensionBuilder AddTime(double timeExponent)
        {
            _dimension.TimeExp += timeExponent;
            return this;
        }

        /// <summary>
        /// Adds mass exponent to the built dimension.
        /// </summary>
        /// <param name="massExponent"></param>
        /// <returns>DimensionBuilder</returns>
        public DimensionBuilder AddMass(double massExponent)
        {
            _dimension.MassExp += massExponent;
            return this;
        }

        /// <summary>
        /// Adds current exponent to the built dimension.
        /// </summary>
        /// <param name="currentExponent"></param>
        /// <returns>DimensionBuilder</returns>
        public DimensionBuilder AddCurrent(double currentExponent)
        {
            _dimension.CurrentExp += currentExponent;
            return this;
        }

        /// <summary>
        /// Adds amount of substance exponent to the built dimension.
        /// </summary>
        /// <param name="amountOfSubstanceExponent"></param>
        /// <returns>DimensionBuilder</returns>
        public DimensionBuilder AddAmountOfSubstance(double amountOfSubstanceExponent)
        {
            _dimension.AmountOfSubstanceExp += amountOfSubstanceExponent;
            return this;
        }

        /// <summary>
        /// Adds luminous intensity exponent to the built dimension.
        /// </summary>
        /// <param name="luminousIntensityExponent"></param>
        /// <returns>DimensionBuilder</returns>
        public DimensionBuilder AddLuminousIntensity(double luminousIntensityExponent)
        {
            _dimension.LuminousIntensityExp += luminousIntensityExponent;
            return this;
        }

        /// <summary>
        /// Adds length exponent to the built dimension.
        /// </summary>
        /// <param name="lengthExponent"></param>
        /// <returns>DimensionBuilder</returns>
        public DimensionBuilder AddLength(double lengthExponent)
        {
            _dimension.LengthExp += lengthExponent;
            return this;
        }

        /// <summary>
        /// Builds a dimension from preset exponents
        /// </summary>
        /// <returns></returns>
        public Dimension Build() => _dimension;
    }
}