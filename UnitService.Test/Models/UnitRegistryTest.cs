namespace UnitService.Test.Models
{
    public class UnitRegistryTest : IDisposable
    {
        private readonly string METER = "METER";

        // Tear down logic
        public void Dispose()
        {
            UnitRegistry.UnregisterUnit(METER);
        }

        [Fact]
        public void WhenRegisterUnitIsCalled_ShouldRegisterUnit()
        {
            var meterUnit = new Unit($"__{METER}__", "m", (1, 0), Dimensions.LENGTH);
            UnitRegistry.RegisterUnit(METER, meterUnit);

            Assert.True(UnitRegistry.GetUnit(METER) == meterUnit);
        }

        [Fact]
        public void WhenUnregisterUnitIsCalledWithUnitInRegistry_ShouldReturnTrue()
        {
            var meterUnit = new Unit($"__{METER}__", "m", (1, 0), Dimensions.LENGTH);
            UnitRegistry.RegisterUnit(METER, meterUnit);
            bool successful = UnitRegistry.UnregisterUnit($"__{METER}__");

            Assert.True(successful);
        }

        [Fact]
        public void WhenUnregisterUnitIsCalledWithUnitNotInRegistry_ShouldNotReturnFalse()
        {
            bool successful = UnitRegistry.UnregisterUnit(METER + "__");

            Assert.False(successful);
        }
    }
}