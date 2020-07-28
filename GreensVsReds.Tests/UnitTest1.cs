using GreenVsRed.Services;
using Xunit;

namespace GreensVsReds.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var validWidthX = 3;
            var validWidthY = 3;

            var inputArgsStr = $"{validWidthX}, {validWidthY}";

            var matrixService = new MatrixService();

            matrixService.GetMatrixDimensions(inputArgsStr);

            Assert.Equal(validWidthX,matrixService.GetMatrixWidth());

        }
    }
}
