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
        /// <exception cref="ArgumentException">Throw when input contains not enogth or not allowed charakters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throw when matrix is full.</exception>
        public void CreateMatrixRow(string inputArgsStr)
        {
            var currentMatrixWidth = this.Matrix.State.Count;
            if (currentMatrixWidth == this.Matrix.Y)
            {
                throw new ArgumentOutOfRangeException(ErrMsg.MatrixIsFull);
            }

            // Validate allowed digits in matrix
            if (!Regex.IsMatch(inputArgsStr, RegXPattern.AllowedDigitsInMatrix))
            {
                throw new ArgumentException(string.Format(ErrMsg.NotAllowedCharacterInLine, currentMatrixWidth, inputArgsStr, this.Matrix.X, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));
            }

            var args = inputArgsStr
                    .ToCharArray()
                    .Select(ch => ch - '0')
                    .ToList();

            // Validate row width
            if (args.Count != this.Matrix.X)
            {
                throw new ArgumentException(string.Format(ErrMsg.NotCorrectDigitsCount, currentMatrixWidth, inputArgsStr, this.Matrix.X, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));
            }

            this.Matrix.State.Add(args);
        }

        /// <summary>
        /// Get target point dimensions X and Y and rounds to recalculate matrix.
        /// </summary>
        /// <param name="inputArgsStr">three integers separated with comma: target point X and Y and N rounds to recalculate matrix.</param>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not enougth or correct parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when
        /// point with coordX and coordY does not exist.</exception>
        public void GetGameConditions(string inputArgsStr)
        {
            this.TargetConditions = this.ValidateTargetConditions(inputArgsStr, this.Matrix.X, this.Matrix.Y);
        }

        /// <summary>
        /// Method who recalculate Matrix N rounds and return how many times the target point become green.
        /// </summary>
        /// <param name="coordX">Point coordinate X.</param>
        /// <param name="coordY">Point coordinate Y.</param>
        /// <param name="rounds">Matrix recalculation rounds.</param>
        public void RecalculateMatrixNRounds()
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
            int width = this.TryGetDimension(args[0], errMsg);

            // Validate that Height is intereger
            errMsg = string.Format(ErrMsg.NotCorrectHeight, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
            int height = this.TryGetDimension(args[1], errMsg);

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

        private int TryGetDimension(string arg, string errMsg)
        {
            int height;
            if (!int.TryParse(arg, out height))
            {
                throw new ArgumentException(errMsg);
            }

            return height;
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
        private ITargetConditions ValidateTargetConditions(string input, int matrixWidth, int matrixHeight)
        {
            var separators = new char[] { ',', ' ' };
            var args = input
                        .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

            // Validate first line that contain three argument
            var isValidInput = Regex.IsMatch(input, RegXPattern.TargetConditions);
            if (!isValidInput || args.Count != GeneralConstants.TargetConditionsCount)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                throw new ArgumentException(errMsg);
            }

            int coordX;

            // Validate Target Point X. Integer between Min Target Point X and Matrix Width
            if (!int.TryParse(args[0], out coordX))
            {
                var errMsg = string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth);
                throw new ArgumentException(nameof(coordX), errMsg);
            }

            int coordY;

            // Validate Target Point Y. Integer between Min Target Point Y and Matrix Width
            if (!int.TryParse(args[1], out coordY))
            {
                var errMsg = string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinMatrixSize, matrixHeight);
                throw new ArgumentException(errMsg);
            }

            if ((coordX < GeneralConstants.MinTargetPointX || coordX >= matrixWidth) && (coordY < GeneralConstants.MinTargetPointY || coordY >= matrixHeight))
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY, GeneralConstants.MinTargetPointX, matrixWidth - 1, GeneralConstants.MinTargetPointY, matrixHeight - 1);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth - 1);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinTargetPointY, matrixHeight - 1);

                throw new ArgumentOutOfRangeException(string.Empty, errMsg);
            }
            else if (coordX < GeneralConstants.MinTargetPointX || coordX >= matrixWidth)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY, GeneralConstants.MinTargetPointX, matrixWidth - 1, GeneralConstants.MinTargetPointY, matrixHeight - 1);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth - 1);

                throw new ArgumentOutOfRangeException(string.Empty, errMsg);
            }
            else if (coordY < GeneralConstants.MinTargetPointY || coordY >= matrixHeight)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY);
                errMsg += "\n" + string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinTargetPointY, matrixHeight - 1);

                throw new ArgumentOutOfRangeException(string.Empty, errMsg);
            }

            int rounds;

            // Validate Rounds. Integer between Min and Max possible rounds
            if (!int.TryParse(args[2], out rounds) || rounds < GeneralConstants.MinRounds || rounds > GeneralConstants.MaxRounds)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            var targetConditions = new TargetConditions(coordX, coordY, rounds);

            return targetConditions;
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
