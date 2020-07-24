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
    public class CommonMethods
    {
        /// <exception cref="ArgumentException">Thrown when line 
        /// contains not allowed character.</exception>
        /// <remarks>
        /// This class can add, subtract, multiply and divide.
        /// </remarks>
        public static IState InitialState()
        {
            try
            {
                var matrixDimention = GetMatrixDimention();
                var matrixHeight = matrixDimention.CoordY;
                var matrixWidth = matrixDimention.CoordX;
                var point = new Point(3, 4);
                var state = new State(matrixWidth, matrixHeight, point, 5);
                state.Generation = new int[matrixHeight][];
                for (int i = 0; i < matrixHeight; i++)
                {
                    var inputRow = Console.ReadLine().Trim();

                    if (Regex.IsMatch(inputRow, RegXPattern.AllowedDigitsInMatrix))
                    {
                        var listItem = inputRow.Split("", StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();
                        state.Generation[i] = listItem;
                    }
                    else
                    {
                        var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, i + 1, inputRow, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                        Console.WriteLine(errMsg);
                        i--;
                    }
                }
                return state;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        /// <exception cref="ArgumentException">Thrown when line 
        /// contains not enougth parameters.</exception>
        private static Point GetMatrixDimention()
        {
            // Read first line and trim it. 
            var input = Console.ReadLine().Trim();
            var separators = new char[] { ',', ' ' };

            var args = input.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToArray();

            //Validate first line that contain two argument 
            if (!Regex.IsMatch(input, RegXPattern.FirstLineStartWithDigits) || args.Length != GeneralConstants.MatrixDimention)
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

            var result = new Point(width, height);

            return result;
        }
    }
}
