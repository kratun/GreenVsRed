using GreenVsRed.Common.Constants;
using GreenVsRed.Common.Validations;
using GreenVsRed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenVsRed.Common
{
    public class MatrixState
    {
        public MatrixState()
        {
            this.State = this.InitialState();
        }
        public IState State { get; private set; }
        /// <exception cref="ArgumentException">Thrown when line 
        /// contains not allowed character.</exception>
        /// <remarks>
        /// This class can add, subtract, multiply and divide.
        /// </remarks>
        public IState InitialState()
        {
            try
            {
                var matrixDimention = GetMatrixDimention();
                var matrixHeight = matrixDimention.CoordY;
                var matrixWidth = matrixDimention.CoordX;
                var generation = ReadMatrix(matrixWidth, matrixHeight);
                var args = ReadTargetConditions(matrixWidth, matrixHeight);


                var point = new Point(3, 4);
                var state = new State(matrixWidth, matrixHeight, point, 5);
                return state;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private List<int> ReadTargetConditions(int matrixWidth, int matrixHeight)
        {
            var separators = new char[] { ',', ' ' };
            var input = Console.ReadLine().Trim();

            var args = input
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            //Validate first line that contain two argument 
            if (!Regex.IsMatch(input, RegXPattern.TargetConditions) || args.Count != GeneralConstants.MatrixDimension)
            {
                var errMsg = ErrMsg.MatrixDimentionException;
                throw new ArgumentException(errMsg);
            }


            int height;
            //Validate Height that is number between Min and Max possible
            if (!int.TryParse(args[1], out height))
            {
                var errMsg = string.Format(ErrMsg.NotCorrectHeight, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            int width;
            //Validate Width that is number between Min and Max possible
            if (!int.TryParse(args[0], out width) || width < GeneralConstants.MinMatrixSize || width > GeneralConstants.MaxMatrixSize || width > height)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            return new List<int>();
        }

        private List<List<int>> ReadMatrix(int matrixWidth, int matrixHeight)
        {
            var matrix = new List<List<int>>();
            Console.WriteLine(string.Format(GeneralConstants.EnterMatrix, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));
            for (int i = 0; i < matrixHeight; i++)
            {
                Console.Write(string.Format(GeneralConstants.EnterMatrixRow, i + 1));
                var inputRow = Console.ReadLine().Trim();

                //Validate allowed digits in matrix
                if (!Regex.IsMatch(inputRow, RegXPattern.AllowedDigitsInMatrix))
                {
                    var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, i + 1, inputRow, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                    Console.WriteLine(errMsg);
                    i--;
                    continue;
                }

               var listItem = inputRow
                    .ToCharArray()
                    .Select(ch => ch - '0')
                    .ToList();


                //Validate row width
                if (listItem.Count != matrixWidth)
                {
                    var errMsg = string.Format(ErrMsg.NotCorrectDigitsCount, i + 1, inputRow, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                    Console.WriteLine(errMsg);
                    i--;
                    continue;
                }

                matrix.Add(listItem);
            }

            return matrix;
        }

        /// <exception cref="ArgumentException">Thrown when line 
        /// contains not enougth parameters.</exception>
        private static Point GetMatrixDimention()
        {
            Console.Write(GeneralConstants.EnterMatrixDimensions);
            // Read first line and trim it. 
            var input = Console.ReadLine().Trim();
            var separators = new char[] { ',', ' ' };

            var args = input
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            //Validate first line that contain two argument 
            if (!Regex.IsMatch(input, RegXPattern.FirstLine) || args.Length != GeneralConstants.MatrixDimension)
            {
                var errMsg = ErrMsg.MatrixDimentionException;
                throw new ArgumentException(errMsg);
            }


            int height;
            //Validate Height that is number between Min and Max possible
            if (!int.TryParse(args[1], out height) || height < GeneralConstants.MinMatrixSize || height > GeneralConstants.MaxMatrixSize)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectHeight, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            int width;
            //Validate Width that is number between Min and Max possible
            if (!int.TryParse(args[0], out width) || width < GeneralConstants.MinMatrixSize || width > GeneralConstants.MaxMatrixSize || width > height)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            var result = new Point(width, height);

            return result;
        }
    }
}
