namespace UnitService.Test.Utils
{
    public class DimensionBuilderTest
    { 
        [Fact]
        public void WhenBuilderCallsAllOptions_ShouldCreateDimensionWithAllExponentsEqualToOne()
        {
            Dimension dimension = new DimensionBuilder()
                .AddTemperature(1)
                .AddLength(1)
                .AddMass(1)
                .AddCurrent(1)
                .AddTime(1)
                .AddAmountOfSubstance(1)
                .AddLuminousIntensity(1)
                .Build();

            Assert.True(dimension == (1, 1, 1, 1, 1, 1, 1));
        }
    }
}