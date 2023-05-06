using UnitService.Library.Constants;
using UnitService.Library.Models;

namespace UnitService.Test.Models
{
    public class DimensionTest
    {
        [Fact]
        public void Parse_ShouldParseSuccessfully_When_ParsedStringIsCorrect()
        {
            var dim = Dimension.Parse(@$"{Dimensions.LENGTH}/{Dimensions.TIME}");

            Assert.IsType<Dimension>(dim);
            Assert.True(dim.LengthExp == 1 && dim.TimeExp == -1);
        }

        [Fact]
        public void Parse_ShouldFail_When_ParsedStringContainsUnrecognisedDimension()
        {
            var ex = Assert.Throws<Exception>(() => Dimension.Parse(@$"{Dimensions.LENGTH}/[Fake]"));
            Assert.Equal("Unregistered dimension specified", ex.Message);
        }

        [Fact]
        public void TryParse_ShouldParseSuccessfully_When_ParsedStringIsCorrect()
        {
            var isSuccessful = Dimension.TryParse(@$"{Dimensions.LENGTH}/{Dimensions.TIME}", out Dimension dim);

            Assert.True(dim.LengthExp == 1 && dim.TimeExp == -1);
            Assert.True(isSuccessful);
        }

        [Fact]
        public void TryParse_ShouldFailToParse_When_ParsedStringIsIncorrect()
        {
            var isSuccessful = Dimension.TryParse(@$"{Dimensions.LENGTH}/[Something]", out Dimension dim);

            Assert.True(dim == default);
            Assert.False(isSuccessful);
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