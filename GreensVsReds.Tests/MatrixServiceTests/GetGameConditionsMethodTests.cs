namespace GreensVsReds.Tests.MatrixServiceTests
{
    using System;
    using Xunit;

    using GreenVsRed.Services;
    using GreenVsRed.Common.Constants;

    public class GetGameConditionsMethodTests : IDisposable
    {

        public GetGameConditionsMethodTests()
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
            var validWidthX = 1;
            var validHeightY = 0;
            var validRounds = 10;

            var inputArgsStr = $"{validWidthX}, {validHeightY}, {validRounds}";

            var result = this.MatrixService.GetGameConditions(inputArgsStr);

            Assert.True(result);
        }

        [Fact]
        public void ReturnErrorNotValidArguments()
        {
            var validWidthX = "";
            var validHeightY = 0;
            var validRounds = 10;

            var inputArgsStr = $"{validWidthX}, {validHeightY}, {validRounds}";

            try
            {
                var result = this.MatrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorMissingArgs()
        {
            var inputArgsStr = $",,1";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorMissingPointXY()
        {
            var inputArgsStr = $",,1";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorMissingPointX()
        {
            var inputArgsStr = $",1,1";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorMissingPointY()
        {
            var inputArgsStr = $"1,,1";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeXY()
        {
            var minTargetPointX = GeneralConstants.MinTargetPointX;
            var minTargetPointY = GeneralConstants.MinTargetPointY;
            var matrixWidth = this.MatrixService.GetMatrixWidth();
            var matrixHeight = this.MatrixService.GetMatrixHeight();

            var coordX = -1;
            var coordY = 1000;
            var rounds = 1;
            var inputArgsStr = $"{coordX},{coordY},{rounds}";

            try
            {
                this.MatrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointX, minTargetPointX, matrixWidth - 1);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointY, minTargetPointY, matrixHeight - 1);

                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeX()
        {
            var minTargetPointX = GeneralConstants.MinTargetPointX;
            var matrixWidth = this.MatrixService.GetMatrixWidth();

            var coordX = -1;
            var coordY = 2;
            var rounds = 1;
            var inputArgsStr = $"{coordX},{coordY},{rounds}";

            try
            {
                this.MatrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointX, minTargetPointX, matrixWidth - 1);

                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeY()
        {
            var minTargetPointY = GeneralConstants.MinTargetPointY;
            var matrixHeight = this.MatrixService.GetMatrixHeight();

            var coordX = 0;
            var coordY = -1;
            var rounds = 1;
            var inputArgsStr = $"{coordX},{coordY},{rounds}";

            try
            {
                this.MatrixService.GetGameConditions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointY, minTargetPointY, matrixHeight - 1);

                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeRounds()
        {
            var coordX = 0;
            var coordY = 1;
            var rounds = -1;
            var inputArgsStrXY = $"{coordX},{coordY},{rounds}";

            try
            {
                this.MatrixService.GetGameConditions(inputArgsStrXY);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeRounds);

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

            var rowStr1 = "000";
            var rowStr2 = "111";
            var rowStr3 = "000";

            martixService.CreateMatrixRow(rowStr1);
            martixService.CreateMatrixRow(rowStr2);
            martixService.CreateMatrixRow(rowStr3);

            return martixService;
        }
    }
}
