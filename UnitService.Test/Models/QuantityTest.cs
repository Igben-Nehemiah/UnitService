using UnitService.Core.Models;
using UnitService.Test.Fixtures;

namespace UnitService.Test.Models
{
    public class QuantityTest : IClassFixture<QuantityFixture>
    {
        private readonly string METER = "METER";
        private readonly string KILOMETER = "KILOMETER";
        private readonly string SECOND = "SECOND";


        [Fact]
        public void WhenConvertToBaseUnitIsCalledOnNonBaseUnit_ShouldPerformConversion()
        {
            var kilometerUnit = UnitRegistry.GetUnit(KILOMETER);
            Quantity lengthInKilometers = new(1, kilometerUnit);

            var lengthInBaseUnit = lengthInKilometers.ConvertToBaseUnit();

            Assert.True(lengthInBaseUnit.Magnitude == 1000 &&
                lengthInBaseUnit.Unit == UnitRegistry.GetBaseUnit(kilometerUnit.Dimension));
        }

        [Theory]
        [InlineData("METER", "KILOMETER", 1.0d, 0.001d)]
        [InlineData("KILOMETER", "METER", 1.0d, 1000d)]
        [InlineData("SECOND", "HOUR", 1.0d, 1/3600d)]
        [InlineData("HOUR", "SECOND", 1.0d, 3600d)]
        [InlineData("SECOND", "MINUTE", 1.0d, 1/60d)]
        [InlineData("MINUTE", "SECOND", 1.0d, 60d)]
        [InlineData("KELVIN", "CELCIUS", 273.15d, 0d)]
        [InlineData("CELCIUS", "KELVIN", 100d, 373.15d)]
        public void WhenConvertToIsCalledWithValidUnit_ShouldPerformConversion(string fromUnitStr,
            string toUnitStr,
            double fromValue,
            double expectedResult)
        {
            var fromUnit = UnitRegistry.GetUnit(fromUnitStr);
            var toUnit = UnitRegistry.GetUnit(toUnitStr);
            Quantity quantityInFromUnit = new(fromValue, fromUnit);

            var quantityInToUnit = quantityInFromUnit.ConvertTo(toUnit);

            Assert.True(Math.Abs(quantityInToUnit.Magnitude!.Value - expectedResult) < 1e-5  &&
                quantityInToUnit.Unit == toUnit);

        }

        [Fact]
        public void WhenQuantitiesWithSameDimensionsAreAdded_ShouldAddQuantitiesAndTakeUnitOfLeftQuantity()
        {
            var meterLengthUnit = UnitRegistry.GetUnit(METER);
            var kilometerLengthUnit = UnitRegistry.GetUnit(KILOMETER);
            Quantity lengthInMeters = new(1000, meterLengthUnit);
            Quantity lengthInKilometers = new(1, kilometerLengthUnit);

            var lengthResultInMeters = lengthInMeters + lengthInKilometers;
            var lengthResultInKilometers = lengthInKilometers + lengthInMeters;

            Assert.True(lengthResultInMeters.Unit == lengthInMeters.Unit);
            Assert.True(lengthResultInMeters.Magnitude == 2000);

            Assert.True(lengthResultInKilometers.Unit == lengthInKilometers.Unit);
            Assert.True(lengthResultInKilometers.Magnitude == 2);
        }

        [Fact]
        public void WhenQuantitiesWithSameDimensionsAreSubtracted_ShouldPerformSubtractionAndTakeUnitOfLeftQuantity()
        {
            var meterLengthUnit = UnitRegistry.GetUnit(METER);
            var kilometerLengthUnit = UnitRegistry.GetUnit(KILOMETER);
            Quantity lengthInMeters = new(1000, meterLengthUnit);
            Quantity lengthInKilometers = new(1, kilometerLengthUnit);

            var lengthResultInMeters = lengthInMeters - lengthInKilometers;
            var lengthResultInKilometers = lengthInKilometers - lengthInMeters;

            Assert.True(lengthResultInMeters.Unit == lengthInMeters.Unit);
            Assert.True(lengthResultInMeters.Magnitude == 0);

            Assert.True(lengthResultInKilometers.Unit == lengthInKilometers.Unit);
            Assert.True(lengthResultInKilometers.Magnitude == 0);
        }

        [Fact]
        public void WhenQuantitiesOfDifferentDimensionsAreAdded_ShouldRaiseAnException()
        {
            var lengthUnit = UnitRegistry.GetUnit(METER);
            var timeUnit = UnitRegistry.GetUnit(SECOND);
            Quantity lengthInMeters = new(1000, lengthUnit);
            Quantity timeInSeconds = new(10, timeUnit);

            Assert.Throws<Exception>(() => lengthInMeters + timeInSeconds);
        }

        [Fact]
        public void WhenImplicitCastOfQuantityToDouble_ShouldStoreQuantityMagnitudeInDouble()
        {
            var lengthUnit = UnitRegistry.GetUnit(METER);
            Quantity lengthInMeters = new(1000, lengthUnit);
            double lengthMaginitude = lengthInMeters;

            Assert.True(lengthMaginitude == lengthInMeters.Magnitude);
        }

        [Theory]
        [InlineData(1000, 4, 4000)]
        [InlineData(1000, 1, 1000)]
        [InlineData(1000, 0.2, 200)]
        [InlineData(1000, 0.0, 0)]
        public void WhenQuantitiesAreMultipliedWithAConstant_ShouldMultiplyTheMagnitudeOfQuantityByConstant(double length, 
            double constant, 
            double result)
        {
            var lengthUnit = UnitRegistry.GetUnit(METER);
            Quantity lengthInMeters = constant * new Quantity(length, lengthUnit);

            Assert.True(result == lengthInMeters.Magnitude);
        }
    }
}