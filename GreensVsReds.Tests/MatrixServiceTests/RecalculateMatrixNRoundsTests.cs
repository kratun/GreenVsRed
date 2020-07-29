namespace GreensVsReds.Tests.MatrixServiceTests
{
    using System;
    using Xunit;

    using GreenVsRed.Services;

    public class RecalculateMatrixNRoundsTests : IDisposable
    {
        public RecalculateMatrixNRoundsTests()
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
            var result = this.MatrixService.RecalculateMatrixNRounds();

            Assert.True(result);
        }

        private IMatrixService InitializeMatrixService()
        {
            var martixService = new MatrixService();

            var matrixWidthX = 3;
            var matrixHeightY = 3;
            var inputArgsStr = $"{matrixWidthX}, {matrixHeightY}";

            martixService.GetMatrixDimensions(inputArgsStr);

            var rowStr1 = "000";
            var rowStr2 = "111";
            var rowStr3 = "000";

            martixService.CreateMatrixRow(rowStr1);
            martixService.CreateMatrixRow(rowStr2);
            martixService.CreateMatrixRow(rowStr3);

            var gameConditionsStr = "1,0,10";
            martixService.GetGameConditions(gameConditionsStr);

            return martixService;
        }
    }
}
