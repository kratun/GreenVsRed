namespace GreensVsReds.Tests.MatrixServiceTests
{
    using System;
    using Xunit;

    using GreenVsRed.Services;
    using GreenVsRed.Common.Constants;

    public class GetMatrixDimensionsMethodTests
    {
        [Fact]
        public void WorksCorrect()
        {
            var validWidthX = 3;
            var validHeightY = 3;

            var inputArgsStr = $"{validWidthX}, {validHeightY}";

            var matrixService = new MatrixService();

            matrixService.GetMatrixDimensions(inputArgsStr);

            Assert.Equal(validWidthX, matrixService.GetMatrixWidth());
        }

        [Fact]
        public void ReturnErrorMissingXY()
        {
            var inputArgsStr = $"";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetMatrixDimensions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.MatrixDimentionException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ErrorMissingX()
        {
            var validHeightY = 3;

            var inputArgsStr = $", {validHeightY}";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetMatrixDimensions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.MatrixDimentionException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorMissingY()
        {
            var validWidthX = 3;

            var inputArgsStr = $"{validWidthX},";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetMatrixDimensions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = ErrMsg.MatrixDimentionException;
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeX()
        {
            var widthX = 1000;
            var heightY = 3;

            var inputArgsStr = $"{widthX}, {heightY}";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetMatrixDimensions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeXGreaterY()
        {
            var widthX = 5;
            var heightY = 3;

            var inputArgsStr = $"{widthX}, {heightY}";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetMatrixDimensions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                Assert.Equal(errMsg, e.Message);
            }
        }

        [Fact]
        public void ReturnErrorOutOfRangeY()
        {
            var widthX = 3;
            var heightY = 1000;

            var inputArgsStr = $"{widthX}, {heightY}";

            var matrixService = new MatrixService();

            try
            {
                matrixService.GetMatrixDimensions(inputArgsStr);
            }
            catch (Exception e)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeHeight, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                Assert.Equal(errMsg, e.Message);
            }
        }
    }
}
