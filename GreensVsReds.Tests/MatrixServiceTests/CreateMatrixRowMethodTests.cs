namespace GreensVsReds.Tests.MatrixServiceTests
{
    using System;
    using Xunit;

    using GreenVsRed.Services;
    using GreenVsRed.Common.Constants;

    public class CreateMatrixRowMethodTests : IDisposable
    {
        public CreateMatrixRowMethodTests()
        {
            this.MatrixService = this.InitializeMatrixService();
        }

        public IMatrixService MatrixService { get; set; }

        public void Dispose()
        {
            this.MatrixService = this.InitializeMatrixService();
        }

        [Fact]
        public void WorksCorrect()
        {
            var inputArgsStr = $"101";

            var result = this.MatrixService.CreateMatrixRow(inputArgsStr);

            Assert.True(result);
        }

        [Fact]
        public void ReturnErrorNotAllowedChar()
        {
            var inputArgsStr = $"201";
            var currentMatrixWidth = 1;
            
            try
            {
                var expectedResult = this.MatrixService.CreateMatrixRow(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, currentMatrixWidth, inputArgsStr, this.MatrixService.GetMatrixWidth(), GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeInput()
        {
            var inputArgsStr = $"1101";
            var currentMatrixWidth = 1;

            try
            {
                var expectedResult = this.MatrixService.CreateMatrixRow(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectDigitsCount, currentMatrixWidth, inputArgsStr, this.MatrixService.GetMatrixWidth(), GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorWhenMatrixIsFull()
        {
            var inputArgsStr = $"101";
            
            try
            {
                var expectedResult = this.MatrixService.CreateMatrixRow(inputArgsStr);
                expectedResult = this.MatrixService.CreateMatrixRow(inputArgsStr);
                expectedResult = this.MatrixService.CreateMatrixRow(inputArgsStr);
                expectedResult = this.MatrixService.CreateMatrixRow(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.MatrixIsFull);
                Assert.Equal(errMsg, e.Message);
            }
        }

        private IMatrixService InitializeMatrixService()
        {
            var martixService = new MatrixService();

            var matrixWidthX = 3;
            var matrixHeightY = 3;
            var inputArgsStr = $"{matrixWidthX}, {matrixHeightY}";

            martixService.GetMatrixDimensions(inputArgsStr);

            return martixService;
        }
    }
}
