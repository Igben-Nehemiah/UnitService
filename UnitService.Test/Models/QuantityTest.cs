using UnitService.Library.Models;

namespace UnitService.Test.Models
{
    public class QuantityTest
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

        [Fact]
        public void WhenConvertToIsCalledWithValidUnit_ShouldPerformConversion()
        {
            var meterUnit = UnitRegistry.GetUnit(METER);
            var kilometerUnit = UnitRegistry.GetUnit(KILOMETER);
            Quantity lengthInMeters = new(1000, meterUnit);

            var lengthInKilometers = lengthInMeters.ConvertTo(kilometerUnit);

            Assert.True(lengthInKilometers.Magnitude == 1 &&
                lengthInKilometers.Unit == kilometerUnit);

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
    }
}