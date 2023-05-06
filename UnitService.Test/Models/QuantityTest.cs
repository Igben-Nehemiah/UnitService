using UnitService.Library.Models;

namespace UnitService.Test.Models
{
    public class QuantityTest
    {
        private readonly string METER = "METER";
        private readonly string KILOMETER = "KILOMETER";

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
    }
}