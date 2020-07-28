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
            this.WriteService = new WriteService();
            this.ReadService = new ReadService();
            this.TargetPointColors = new List<int>();
            this.Matrix = new List<List<int>>();
            this.TargetConditions = new TargetConditions();
        }

        private ITargetConditions TargetConditions { get; set; }

        /// <summary>
        /// Service for writing text.
        /// </summary>
        /// <inheritdoc cref="IWrite"/>
        private IWrite WriteService { get; set; }

        /// <summary>
        /// Service for reading text.
        /// </summary>
        /// <inheritdoc cref="IRead"/>
        private IRead ReadService { get; set; }

        /// <summary>
        /// Gets or sets collection of each target point color during recalculation.
        /// </summary>
        /// <inheritdoc cref="List{T}"/>
        private List<int> TargetPointColors { get; set; }

        /// <summary>
        /// Gets or sets current matrix state.
        /// </summary>
        /// <inheritdoc cref="List{T}"/>
        private List<List<int>> Matrix { get; set; }

        /// <summary>
        /// Get matrix width and height.
        /// </summary>
        /// <returns>Matrix dementions X and Y.</returns>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        public IPoint GetMatrixDimensions()
        {
            this.WriteService.Write(GeneralConstants.EnterMatrixDimensions);

            var input = this.ReadService.ReadLine();

            try
            {
                var result = this.TryGetPoint(input, GeneralConstants.MatrixDimension, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                return result;
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    this.WriteService.WriteLine(e.Message);
                    throw e;
                }
                else
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Create matrix from the input.
        /// </summary>
        public void CreateMatrix()
        {
            var matrixDemention = this.GetMatrixDimensions();
            var matrixHeight = matrixDemention.CoordX();
            var matrixWidth = matrixDemention.CoordY();
            var matrix = new List<List<int>>();
            this.WriteService.WriteLine(string.Format(GeneralConstants.EnterMatrix, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));
            for (int i = 0; i < matrixHeight; i++)
            {
                this.WriteService.Write(string.Format(GeneralConstants.EnterMatrixRow, i + 1));
                var inputRow = this.ReadService.ReadLine();

                // Validate allowed digits in matrix
                if (!Regex.IsMatch(inputRow, RegXPattern.AllowedDigitsInMatrix))
                {
                    var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, i + 1, inputRow, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                    this.WriteService.WriteLine(errMsg);
                    i--;
                    continue;
                }

                var listItem = inputRow
                     .ToCharArray()
                     .Select(ch => ch - '0')
                     .ToList();

                // Validate row width
                if (listItem.Count != matrixWidth)
                {
                    var errMsg = string.Format(ErrMsg.NotCorrectDigitsCount, i + 1, inputRow, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                    this.WriteService.WriteLine(errMsg);
                    i--;
                    continue;
                }

                this.Matrix.Add(listItem);
            }
        }

        /// <summary>
        /// Get target point dimensions and rounds to recalculate matrix.
        /// </summary>
        public void GetTargetConditions()
        {
            var matrixHeight = this.Matrix.Count;
            var matrixWidth = this.Matrix[0].Count;

            while (true)
            {
                try
                {
                    this.WriteService.Write(string.Format(GeneralConstants.EnterTargetConditions, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));

                    var input = this.ReadService.ReadLine();

                    this.TargetConditions = this.ValidateTargetConditions(input, matrixWidth, matrixHeight);

                    this.WriteAllConditionsOk();

                    break;
                }
                catch (Exception e)
                {
                    if ((e is ArgumentException) || (e is ArgumentOutOfRangeException))
                    {
                        this.WriteService.WriteLine(e.Message);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// Method who recalculate Matrix N rounds and return how many times the target point become green.
        /// </summary>
        /// <param name="coordX">Point coordinate X.</param>
        /// <param name="coordY">Point coordinate Y.</param>
        /// <param name="rounds">Matrix recalculation rounds.</param>
        public void RecalculateMatrixNRounds()
        {
            this.WriteService.WriteLine(GeneralConstants.WaitCalculations);

            int coordX = this.TargetConditions.CoordX();
            int coordY = this.TargetConditions.CoordY();
            int rounds = this.TargetConditions.Rounds;

            var pointColor = this.GetPointColor(coordX, coordY);
            this.TargetPointColors.Add(pointColor);
            for (int i = 0; i < rounds; i++)
            {
                this.Matrix = this.RoundNextMatrix();

                pointColor = this.GetPointColor(coordX, coordY);
                this.TargetPointColors.Add(pointColor);
            }
        }

        /// <summary>
        /// Write expected result.
        /// </summary>
        public void WriteExpectedResult()
        {
            var result = this.TargetPointColors.Where(c => c == GeneralConstants.GreenNumber).ToList().Count;
            var strResult = GeneralConstants.ExpectedResult + result;
            this.WriteService.WriteLine(strResult);
        }

        private IPoint TryGetPoint(string input, int validArgsCount, int minSize, int maxSize)
        {
            var separators = new char[] { ',', ' ' };

            var args = input
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            // Validate first line that contain two argument
            if (!Regex.IsMatch(input, RegXPattern.FirstLine) || args.Length != validArgsCount)
            {
                var errMsg = ErrMsg.MatrixDimentionException;
                throw new ArgumentException(errMsg);
            }

            int height;

            // Validate Height that is number between Min and Max possible
            if (!int.TryParse(args[1], out height) || height < minSize || height > maxSize)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectHeight, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            int width;

            // Validate Width that is number between Min and Max possible
            if (!int.TryParse(args[0], out width) || width < GeneralConstants.MinMatrixSize || width > GeneralConstants.MaxMatrixSize || width > height)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            var point = new Point(width, height);

            return point;
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

        private void WriteAllConditionsOk()
        {
            this.WriteService.WriteLine(GeneralConstants.CorrectArgsStr);
        }

        // Receive point and return point color number.
        private int GetPointColor(int coordX, int coordY)
        {
            var pointColor = this.Matrix[coordY][coordX];

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
            var maxRowIndex = this.Matrix.Count - 1;
            var maxColIndex = this.Matrix[0].Count - 1;
            var rowBefore = row - 1;
            var rowAfter = row + 1;
            var colBefore = col - 1;
            var colAfter = col + 1;
            var countSurroundedGreenPoints = 0;

            // Get point color if exist for row before and col before
            if (rowBefore >= GeneralConstants.MinTargetPointX && colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Matrix[rowBefore][colBefore];
            }

            // Get point color if exist for row before and current col
            if (rowBefore >= GeneralConstants.MinTargetPointX)
            {
                countSurroundedGreenPoints += this.Matrix[rowBefore][col];
            }

            // Get point color if exist for row before and col afer
            if (rowBefore >= GeneralConstants.MinTargetPointX && colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Matrix[rowBefore][colAfter];
            }

            // Get point color if exist for row and col before
            if (colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Matrix[row][colBefore];
            }

            // Get point color if exist for row and col afer
            if (colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Matrix[row][colAfter];
            }

            // Get point color if exist for row afer and col before
            if (rowAfter <= maxRowIndex && colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Matrix[rowAfter][colBefore];
            }

            // Get point color if exist for row afer and col
            if (rowAfter <= maxRowIndex)
            {
                countSurroundedGreenPoints += this.Matrix[rowAfter][col];
            }

            // Get point color if exist for row afer and col after
            if (rowAfter <= maxRowIndex && colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Matrix[rowAfter][colAfter];
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
            return this.Matrix[x][y] == GeneralConstants.GreenNumber;
        }

        private List<List<int>> RoundNextMatrix()
        {
            var nextGeneration = new List<List<int>>();
            var endRow = this.Matrix.Count;
            for (int row = 0; row < endRow; row++)
            {
                var tempNextRow = new List<int>();
                var endCol = this.Matrix[row].Count;
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
