// <copyright file="MatrixService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using GreenVsRed.Common.Constants;
    using GreenVsRed.Common.Validations;
    using GreenVsRed.Models;

    /// <summary>
    /// Provides properties and methods that create and recalculating matrix N times.
    /// </summary>
    /// <inheritdoc cref="IMatrixService"/>
    public class MatrixService : IMatrixService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixService"/> class.
        /// </summary>
        public MatrixService()
        {
            this.TargetPointColors = new List<int>();
            this.Matrix = new Matrix();
            this.TargetConditions = new TargetConditions();
        }

        private ITargetConditions TargetConditions { get; set; }

        /// <summary>
        /// Gets or sets collection of each target point color during recalculation.
        /// </summary>
        /// <inheritdoc cref="List{T}"/>
        private List<int> TargetPointColors { get; set; }

        /// <summary>
        /// Gets or sets current matrix state.
        /// </summary>
        /// <inheritdoc cref="List{T}"/>
        private IMatrix Matrix { get; set; }

        /// <summary>
        /// Get matrix X (width) and Y (height).
        /// </summary>
        /// <param name="inputArgsStr">A string contains two integers separated by comma.</param>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range.</exception>
        public void GetMatrixDimensions(string inputArgsStr)
        {
            var result = this.TryGetPoint(inputArgsStr);
            this.Matrix = new Matrix(result.X, result.Y);
        }

        /// <summary>
        /// Gets matrix Y (height).
        /// </summary>
        /// <returns>Matrix height.</returns>
        public int GetMatrixHeight()
        {
            return this.Matrix.Y;
        }

        /// <summary>
        /// Gets matrix Y (width).
        /// </summary>
        /// <returns>Matrix width.</returns>
        public int GetMatrixWidth()
        {
            return this.Matrix.X;
        }

        /// <summary>
        /// Create matrix from the input.
        /// </summary>
        /// <param name="inputArgsStr">Matrix row as string.</param>
        /// <returns>True if matrix row is add.</returns>
        /// <exception cref="ArgumentException">Throw when input contains not allowed charakters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw when the row constains less/more characters or the matrix is full.</exception>
        public bool CreateMatrixRow(string inputArgsStr)
        {
            var currentMatrixWidth = this.Matrix.State.Count;
            if (currentMatrixWidth == this.Matrix.Y)
            {
                throw new ArgumentOutOfRangeException(ErrMsg.MatrixIsFull, new Exception());
            }

            var errMsg = string.Empty;

            // Validate allowed digits in matrix
            if (!Regex.IsMatch(inputArgsStr, RegXPattern.AllowedDigitsInMatrix))
            {
                errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, currentMatrixWidth + 1, inputArgsStr, this.Matrix.X, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                throw new ArgumentException(errMsg);
            }

            var args = inputArgsStr
                    .ToCharArray()
                    .Select(ch => ch - '0')
                    .ToList();

            // Validate row width
            if (args.Count != this.Matrix.X)
            {
                errMsg = string.Format(ErrMsg.NotCorrectDigitsCount, currentMatrixWidth + 1, inputArgsStr, this.Matrix.X, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                throw new ArgumentOutOfRangeException(errMsg, new Exception());
            }

            this.Matrix.State.Add(args);

            return true;
        }

        /// <summary>
        /// Get target point dimensions X and Y and rounds to recalculate matrix.
        /// </summary>
        /// <param name="inputArgsStr">three integers separated with comma: target point X and Y and N rounds to recalculate matrix.</param>
        /// <returns>Returns true if all arguments are Ok.</returns>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not enougth or correct parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when
        /// point with coordX and coordY does not exist.</exception>
        public bool GetGameConditions(string inputArgsStr)
        {
            this.TargetConditions = this.ValidateTargetConditions(inputArgsStr, this.Matrix.X, this.Matrix.Y);

            return true;
        }

        /// <summary>
        /// Method who recalculate Matrix N rounds and return how many times the target point become green.
        /// </summary>
        /// <param name="coordX">Point coordinate X.</param>
        /// <param name="coordY">Point coordinate Y.</param>
        /// <param name="rounds">Matrix recalculation rounds.</param>
        /// <returns>Return true if recalslation finished.</returns>
        public bool RecalculateMatrixNRounds()
        {
            int coordX = this.TargetConditions.X;
            int coordY = this.TargetConditions.Y;
            int rounds = this.TargetConditions.Rounds;

            var pointColor = this.GetPointColor(coordX, coordY);
            this.TargetPointColors.Add(pointColor);
            for (int i = 0; i < rounds; i++)
            {
                this.Matrix.State = this.NextMatrixRound();

                pointColor = this.GetPointColor(coordX, coordY);
                this.TargetPointColors.Add(pointColor);
            }

            return true;
        }

        /// <summary>
        /// Gets how many times target point become green.
        /// </summary>
        /// <returns>How many time taget point become green.</returns>
        public int GetTargetPointGreenColorsCount()
        {
            var result = this.TargetPointColors.Where(c => c == GeneralConstants.GreenNumber).ToList().Count;

            return result;
        }

        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range or X>Y.</exception>
        private IPoint TryGetPoint(string inputArgsStr)
        {
            var separators = new char[] { ',', ' ' };

            var args = inputArgsStr
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var errMsg = string.Empty;

            // Validate first line that contain two argument
            errMsg = ErrMsg.MatrixDimentionException;
            var inputArgsPattern = RegXPattern.FirstLine;
            this.ValidateInputArgsStr(inputArgsStr, GeneralConstants.MatrixDimension, args, errMsg, inputArgsPattern);

            // Validate that Width is intereger
            errMsg = string.Format(ErrMsg.NotCorrectWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
            int width = this.TryGetIntValue(args[0], errMsg);

            // Validate that Height is intereger
            errMsg = string.Format(ErrMsg.NotCorrectHeight, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
            int height = this.TryGetIntValue(args[1], errMsg);

            // Validate Height that is an integer between Min and Max possible
            errMsg = string.Format(ErrMsg.OutOfRangeHeight, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
            this.IsNotDimensionOutOfRange(errMsg, height, height);

            // Validate Width that is an integer between Min and Max possible
            errMsg = string.Format(ErrMsg.OutOfRangeWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
            this.IsNotDimensionOutOfRange(errMsg, width, height);

            var point = new Point(width, height);

            return point;
        }

        private bool IsNotDimensionOutOfRange(string errMsg, int x, int y)
        {
            if (x < GeneralConstants.MinMatrixSize || x > GeneralConstants.MaxMatrixSize || !(x <= y))
            {
                throw new ArgumentOutOfRangeException(errMsg, new Exception());
            }

            return true;
        }

        private int TryGetIntValue(string arg, string errMsg)
        {
            int value;
            if (!int.TryParse(arg, out value))
            {
                throw new ArgumentException(errMsg);
            }

            return value;
        }

        private bool ValidateInputArgsStr(string inputArgsStr, int validArgsCount, string[] args, string errMsg, string inputArgsPattern)
        {
            if (!Regex.IsMatch(inputArgsStr, inputArgsPattern) || args.Length != validArgsCount)
            {
                throw new ArgumentException(errMsg);
            }

            return true;
        }

        /// <exception cref="ArgumentException">Thrown when line
        /// contains not enougth or correct parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when
        /// point with coordX and coordY does not exist.</exception>
        private ITargetConditions ValidateTargetConditions(string inputArgsStr, int matrixWidth, int matrixHeight)
        {
            var separators = new char[] { ',', ' ' };
            var args = inputArgsStr
                        .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

            // Validate first line that contain three argument
            var errMsg = ErrMsg.TargetConditionsException;
            this.ValidateInputArgsStr(inputArgsStr, GeneralConstants.TargetConditionsCount, args, errMsg, RegXPattern.TargetConditions);

            // Validate Target Point X.
            errMsg = string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth - 1);
            int coordX = this.TryGetIntValue(args[0], errMsg);

            // Validate Target Point Y.
            errMsg = string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinMatrixSize, matrixHeight - 1);
            int coordY = this.TryGetIntValue(args[1], errMsg);

            // Validate Target Point Y. Integer between Min Target Point Y and Matrix Width
            errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY);
            this.ValidatePointOutOfRange(matrixWidth, matrixHeight, errMsg, coordX, coordY, GeneralConstants.MinTargetPointX, GeneralConstants.MinTargetPointY);

            // Validate Rounds. Integer between Min and Max possible rounds
            errMsg = string.Format(ErrMsg.NotCorrectRounds, GeneralConstants.MinRounds, GeneralConstants.MaxRounds);
            int rounds = this.TryGetIntValue(args[2], errMsg);

            // Validate out of range rounds
            errMsg = string.Format(ErrMsg.OutOfRangeRounds);
            this.ValidatePointOutOfRange(rounds, GeneralConstants.MinRounds, GeneralConstants.MaxRounds, errMsg);

            var targetConditions = new TargetConditions(coordX, coordY, rounds);

            return targetConditions;
        }

        // Validate value and return true if is valid
        private bool ValidatePointOutOfRange(int value, int minValue, int maxValue, string errMsg)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException(string.Empty, errMsg);
            }

            return true;
        }

        private bool ValidatePointOutOfRange(int matrixWidth, int matrixHeight, string errMsg, int coordX, int coordY, int minTargetPointX, int minTargetPointY)
        {
            if ((coordX < minTargetPointX || coordX > matrixWidth - 1) && (coordY < minTargetPointY || coordY > matrixHeight - 1))
            {
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointX, minTargetPointX, matrixWidth - 1);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointY, minTargetPointY, matrixHeight - 1);

                throw new ArgumentOutOfRangeException(string.Empty, errMsg);
            }
            else if (coordX < minTargetPointX || coordX > matrixWidth - 1)
            {
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointX, minTargetPointX, matrixWidth - 1);

                throw new ArgumentOutOfRangeException(string.Empty, errMsg);
            }
            else if (coordY < minTargetPointY || coordY > matrixHeight - 1)
            {
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointY, minTargetPointY, matrixHeight - 1);

                throw new ArgumentOutOfRangeException(string.Empty, errMsg);
            }

            return true;
        }

        // Receive point and return point color number.
        private int GetPointColor(int coordX, int coordY)
        {
            var pointColor = this.Matrix.State[coordY][coordX];

            return pointColor;
        }

        // Receive row and col for Matrix and return its value
        private int GetNextPointColor(int row, int col)
        {
            var nextPointColor = 0;

            var countSurroundedGreenColor = this.CountSurroundedGreenColor(row, col);
            var countSurroundedGreenColorStr = countSurroundedGreenColor.ToString();

            if (this.IsGreen(row, col))
            {
                nextPointColor = GeneralConstants.GreenPointStayGreenCondition.Contains(countSurroundedGreenColorStr) ? GeneralConstants.GreenNumber : GeneralConstants.RedNumber;
            }
            else
            {
                nextPointColor = GeneralConstants.RedPointBecomeGreenCondition.Contains(countSurroundedGreenColorStr) ? GeneralConstants.GreenNumber : GeneralConstants.RedNumber;
            }

            return nextPointColor;
        }

        private int CountSurroundedGreenColor(int row, int col)
        {
            var maxRowIndex = this.Matrix.Y - 1;
            var maxColIndex = this.Matrix.X - 1;
            var rowBefore = row - 1;
            var rowAfter = row + 1;
            var colBefore = col - 1;
            var colAfter = col + 1;
            var countSurroundedGreenPoints = 0;

            // Get point color if exist for row before and col before
            if (rowBefore >= GeneralConstants.MinTargetPointX && colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Matrix.State[rowBefore][colBefore];
            }

            // Get point color if exist for row before and current col
            if (rowBefore >= GeneralConstants.MinTargetPointX)
            {
                countSurroundedGreenPoints += this.Matrix.State[rowBefore][col];
            }

            // Get point color if exist for row before and col afer
            if (rowBefore >= GeneralConstants.MinTargetPointX && colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Matrix.State[rowBefore][colAfter];
            }

            // Get point color if exist for row and col before
            if (colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Matrix.State[row][colBefore];
            }

            // Get point color if exist for row and col afer
            if (colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Matrix.State[row][colAfter];
            }

            // Get point color if exist for row afer and col before
            if (rowAfter <= maxRowIndex && colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Matrix.State[rowAfter][colBefore];
            }

            // Get point color if exist for row afer and col
            if (rowAfter <= maxRowIndex)
            {
                countSurroundedGreenPoints += this.Matrix.State[rowAfter][col];
            }

            // Get point color if exist for row afer and col after
            if (rowAfter <= maxRowIndex && colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Matrix.State[rowAfter][colAfter];
            }

            return countSurroundedGreenPoints;
        }

        /// <remarks>
        /// This method receive two integer (point[x,y]) and return boolean is it green or not.
        /// </remarks>
        /// <value>
        /// Point coordination [row,col] and return boolean is it green or not.
        /// </value>
        /// <param name="x">An integer which represent point vector X.</param>
        /// <param name="y">An integer which represent point vector Y.</param>
        /// <return>
        /// Boolean true if point color is green.
        /// </return>
        private bool IsGreen(int x, int y)
        {
            return this.Matrix.State[x][y] == GeneralConstants.GreenNumber;
        }

        private List<List<int>> NextMatrixRound()
        {
            var nextGeneration = new List<List<int>>();
            var endRow = this.Matrix.State.Count;
            for (int row = 0; row < endRow; row++)
            {
                var tempNextRow = new List<int>();
                var endCol = this.Matrix.State[row].Count;
                for (int col = 0; col < endCol; col++)
                {
                    var nextColor = this.GetNextPointColor(row, col);
                    tempNextRow.Add(nextColor);
                }

                nextGeneration.Add(tempNextRow);
            }

            return nextGeneration;
        }
    }
}
