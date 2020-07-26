using GreenVsRed.Common.Constants;
using GreenVsRed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Services
{
    public class MatrixService : IMatrixService
    {
        public MatrixService()
        {
            this.TargetPointColors = new List<int>();
            this.Generation = new List<List<int>>();
        }
        public MatrixService(List<List<int>> generation):this()
        {
            this.Generation = generation;
            
        }

        public List<List<int>> Generation { get; set; }
        public List<int> TargetPointColors { get; set; }

        //Recalculate Generation N rounds and return how many times the target point become green 
        public void ChangeGenerationNRounds(int coordX, int coordY, int rounds)
        {
            WritePleaseWaitCalc();
            var pointColor = GetPointColor(coordX, coordY);
            this.TargetPointColors.Add(pointColor);
            for (int i = 0; i < rounds; i++)
            {

                this.Generation = RoundNextGeneration();

                pointColor = GetPointColor(coordX, coordY);
                this.TargetPointColors.Add(pointColor);
            }
        }

        private void WritePleaseWaitCalc()
        {
            Console.WriteLine(GeneralConstants.WaitCalculations);
        }

        //Receive point and return point color number
        private int GetPointColor(int coordX, int coordY)
        {
            var pointColor = this.Generation[coordY][coordX];

            return pointColor;
        }

        //Receive row and col for Generation and return its value
        private int GetNextPointColor(int row, int col)
        {
            var nextPointColor = 0;

            var countSurroundedGreenColor = CountSurroundedGreenColor(row, col);
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
            var maxRowIndex = this.Generation.Count - 1;
            var maxColIndex = this.Generation[0].Count-1;
            var rowBefore = row - 1;
            var rowAfter = row + 1;
            var colBefore = col - 1;
            var colAfter = col + 1;
            var countSurroundedGreenPoints = 0;

            //Get point color if exist for row before and col before
            if (rowBefore >= GeneralConstants.MinTargetPointX && colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Generation[rowBefore][colBefore];
            }

            //Get point color if exist for row before and current col
            if (rowBefore >= GeneralConstants.MinTargetPointX)
            {
                countSurroundedGreenPoints += this.Generation[rowBefore][col];
            }

            //Get point color if exist for row before and col afer
            if (rowBefore >= GeneralConstants.MinTargetPointX && colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Generation[rowBefore][colAfter];
            }

            //Get point color if exist for row and col before
            if (colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Generation[row][colBefore];
            }

            //Get point color if exist for row and col afer
            if (colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Generation[row][colAfter];
            }

            //Get point color if exist for row afer and col before
            if (rowAfter <= maxRowIndex && colBefore >= GeneralConstants.MinTargetPointY)
            {
                countSurroundedGreenPoints += this.Generation[rowAfter][colBefore];
            }

            //Get point color if exist for row afer and col
            if (rowAfter <= maxRowIndex)
            {
                countSurroundedGreenPoints += this.Generation[rowAfter][col];
            }

            //Get point color if exist for row afer and col after
            if (rowAfter <= maxRowIndex && colAfter <= maxColIndex)
            {
                countSurroundedGreenPoints += this.Generation[rowAfter][colAfter];
            }

            return countSurroundedGreenPoints;
        }

        /// <remarks>
        /// This method receive two integer (point[x,y]) and return boolean is it green or not.
        /// </remarks>
        /// <value>
        /// Point coordination [row,col] and return boolean is it green or not.
        /// </value>
        /// <param name="row">An integer which represent point vector X.</param>
        /// <param name="col">An integer which represent point vector Y.</param>
        /// <return>
        /// Boolean true if point color is green.
        /// </return>
        private bool IsGreen(int row, int col)
        {
            return this.Generation[row][col] == GeneralConstants.GreenNumber;
        }

        private List<List<int>> RoundNextGeneration()
        {
            var nextGeneration = new List<List<int>>();
            var endRow = Generation.Count;
            for (int row = 0; row < endRow; row++)
            {
                var tempNextRow = new List<int>();
                var endCol = Generation[row].Count;
                for (int col = 0; col < endCol; col++)
                {
                    var nextColor = GetNextPointColor(row, col);
                    tempNextRow.Add(nextColor);
                }

                nextGeneration.Add(tempNextRow);
            }

            return nextGeneration;
        }
    }
}
