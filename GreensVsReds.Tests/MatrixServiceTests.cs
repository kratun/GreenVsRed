namespace GreensVsReds.Tests
{
    using System;
    using Xunit;

    using GreenVsRed.Services;
    using GreenVsRed.Common.Constants;

    public class MatrixServiceTests
    {
        [Fact]
        public void GetMatrixDimensionsWorks()
        {
            var validWidthX = 3;
            var validHeightY = 3;

            var inputArgsStr = $"{validWidthX}, {validHeightY}";

            var matrixService = new MatrixService();

            matrixService.GetMatrixDimensions(inputArgsStr);

            Assert.Equal(validWidthX, matrixService.GetMatrixWidth());
        }

        [Fact]
        public void GetMatrixDimensionsErrorMissingXY()
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
        public void GetMatrixDimensionsErrorMissingX()
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
                Assert.Equal(errMsg,e.Message);
            }
        }

        [Fact]
        public void GetMatrixDimensionsErrorMissingY()
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
        public void GetMatrixDimensionsErrorOutOfRangeX()
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
        public void GetMatrixDimensionsErrorOutOfRangeXGreaterY()
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
        public void GetMatrixDimensionsErrorOutOfRangeY()
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
