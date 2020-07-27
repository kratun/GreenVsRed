// <copyright file="MatrixService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Services
{
    using System.Collections.Generic;
    using GreenVsRed.Common.Constants;

    /// <summary>
    /// Provides methods to recalculating matrix. Has collection of each target point color during recalculation.
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
            this.TargetPointColors = new List<int>();
            this.Matrix = new List<List<int>>();
        }

        /// <summary>
        /// Service that write text.
        /// </summary>
        /// <inheritdoc cref="IWrite"/>
        public IWrite WriteService { get; set; }

        /// <summary>
        /// Collection of each target point color during recalculation.
        /// </summary>
        /// <inheritdoc cref="List{T}"/>
        public List<int> TargetPointColors { get; set; }

        /// <inheritdoc cref="List{T}"/>
        public List<List<int>> Matrix { get; set; }

        /// <summary>
        /// Method who recalculate Matrix N rounds and return how many times the target point become green.
        /// </summary>
        /// <param name="coordX">Point coordinate X.</param>
        /// <param name="coordY">Point coordinate Y.</param>
        /// <param name="rounds">Matrix recalculation rounds.</param>
        public void RecalculateMatrixNRounds(int coordX, int coordY, int rounds)
        {
            this.WriteService.WriteLine(GeneralConstants.WaitCalculations);

            var pointColor = this.GetPointColor(coordX, coordY);
            this.TargetPointColors.Add(pointColor);
            for (int i = 0; i < rounds; i++)
            {
                this.Matrix = this.RoundNextMatrix();

                pointColor = this.GetPointColor(coordX, coordY);
                this.TargetPointColors.Add(pointColor);
            }
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
