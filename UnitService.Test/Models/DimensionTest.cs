namespace UnitService.Test.Models
{
    public class DimensionTest
    {
        [Fact]
        public void Parse_ShouldParseSuccessfully_When_ParsedStringIsCorrect()
        {
            Dimension dim = $"{Dimensions.LENGTH}/{Dimensions.TIME}";

            string dimAsString = dim;

            Assert.True(dim.LengthExp == 1 
                && dim.TimeExp == -1
                && dim.MassExp == 0
                && dim.TempExp == 0
                && dim.CurrentExp == 0);
            Assert.True(dimAsString == Dimensions.LENGTH/Dimensions.TIME);
        }

        [Fact]
        public void Parse_ShouldFail_When_ParsedStringContainsUnrecognisedDimension()
        {
            Assert.Throws<DimensionParseException>(() => Dimension.Parse(@$"{Dimensions.LENGTH}/[Fake]"));
        }

        [Fact]
        public void ToString_ShouldReturnStringRepresentationOfDimensionThatCanBeParsedIntoDimension()
        {
            string dimensionAsString = $@"{Dimensions.LENGTH}/{Dimensions.TIME * Dimensions.TIME}";

            Dimension dimension = dimensionAsString;

            Assert.True(dimension.LengthExp == 1
                && dimension.TimeExp == -2
                && dimension.MassExp == 0
                && dimension.TempExp == 0
                && dimension.CurrentExp == 0);
        }

        [Fact]
        public void TryParse_ShouldParseSuccessfully_When_ParsedStringIsCorrect()
        {
            var isSuccessful = Dimension.TryParse(Dimensions.LENGTH/Dimensions.TIME, out Dimension dim);

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