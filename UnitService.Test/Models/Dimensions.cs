using UnitService.Library.Models;

namespace UnitService.Test.Models
{
    public class Dimensions
    {
        [Fact]
        public void Parse_ShouldParseSuccessfully_When_ParsedStringIsCorrect()
        {
            var dim = Dimension.Parse(@"[Length]/[Time]");

            Assert.IsType<Dimension>(dim);
            Assert.True(dim.LengthExp == 1 && dim.TimeExp == -1);
        }

        [Fact]
        public void Parse_ShouldFail_When_ParsedStringContainsUnrecognisedDimension()
        {
            var ex = Assert.Throws<Exception>(() => Dimension.Parse(@"[Length]/[Fake]"));
            Assert.Equal("Unregistered dimension specified", ex.Message);
        }

        [Theory]
        [InlineData(1,1,1,1,1)]
        [InlineData(1,-1,-1,0,1)]
        [InlineData(0,0,0,0,0)]
        [InlineData(1,-1,0,0,0)]
        public void DimensionCtor_ShouldConstructDimensionWithParsedParameters(
            double lengthExp,
            double timeExp,
            double massExp,
            double currentExp,
            double tempExp)
        {
            var dim = new Dimension(lengthExp: lengthExp, timeExp: timeExp,
                massExp: massExp, currentExp: currentExp, tempExp: tempExp);

            Assert.True(dim.LengthExp == lengthExp 
                && dim.TimeExp == timeExp
                && dim.MassExp == massExp
                && dim.CurrentExp == currentExp
                && dim.TempExp == tempExp);
        }
    }
}