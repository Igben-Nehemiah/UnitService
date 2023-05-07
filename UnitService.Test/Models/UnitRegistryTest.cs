namespace UnitService.Test.Models
{
    public class UnitRegistryTest : IDisposable
    {
        private readonly string METER = "METER";

        public UnitRegistryTest() => UnitRegistryTestHelper.RegisterUnitsForTesting();

        [Fact]
        public void WhenRegisterUnitIsCalled_ShouldRegisterUnit()
        {
            var meterUnit = new Unit($"__{METER}__", "m", (1, 0), Dimensions.LENGTH);
            UnitRegistry.RegisterUnit($"__{METER}__", meterUnit);

            Assert.True(UnitRegistry.GetUnit($"__{METER}__") == meterUnit);
        }

        [Fact]
        public void WhenUnregisterUnitIsCalledWithUnitInRegistry_ShouldReturnTrue()
        {
            var meterUnit = new Unit($"__{METER}__", "m", (1, 0), Dimensions.LENGTH);
            UnitRegistry.RegisterUnit($"__{METER}__", meterUnit);
            bool successful = UnitRegistry.UnregisterUnit($"__{METER}__");

            Assert.True(successful);
        }

        [Fact]
        public void WhenUnregisterUnitIsCalledWithUnitNotInRegistry_ShouldNotReturnFalse()
        {
            bool successful = UnitRegistry.UnregisterUnit(METER + "__");

            Assert.False(successful);
        }

        public void Dispose() => UnitRegistryTestHelper.UnregisterUnitsForTesting();
    }
}