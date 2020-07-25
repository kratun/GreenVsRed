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
                
                var pointCoordX =0;
                var pointCoordY = 0;
                var rounds = 0;
                while (true)
                {
                    try
                    {
                        var args = GetTargetConditions(matrixWidth, matrixHeight);

                        pointCoordX = args[0];
                        pointCoordY = args[1];
                        rounds = args[2];

                        break;
                    }
                    catch (Exception e)
                    {
                        if (e is ArgumentException)
                        {
                            Console.WriteLine(e.Message);
                        }
                        else { throw e; }
                        
                    }
                }
                
                var point = new Point(pointCoordX,pointCoordY);
                var state = new State(matrixWidth, matrixHeight, point, rounds);
                return state;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    Console.WriteLine(ex.Message);
                    
                }
                
                throw ex;
                
            }


        }

        /// <exception cref="ArgumentException">Thrown when line 
        /// contains not enougth or correct parameters.</exception>
        private List<int> GetTargetConditions(int matrixWidth, int matrixHeight)
        {
            Console.Write(string.Format(GeneralConstants.EnterTargetConditions, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));

            var separators = new char[] { ',', ' ' };
            var input = Console.ReadLine().Trim();
            
            var args = input
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            //Validate first line that contain three argument 
            if (!Regex.IsMatch(input, RegXPattern.TargetConditions) || args.Count != GeneralConstants.MatrixDimension)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                throw new ArgumentException(errMsg);
            }

            int coordX;
            //Validate Target Point X. Integer between Min Target Point X and Matrix Width
            if (!int.TryParse(args[0], out coordX) || coordX < GeneralConstants.MinTargetPointX || coordX >= matrixWidth)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth);
                throw new ArgumentOutOfRangeException(nameof(coordX), errMsg);
            }

            int coordY;
            //Validate Target Point Y. Integer between Min Target Point Y and Matrix Width
            if (!int.TryParse(args[1], out coordY) || coordY < GeneralConstants.MinTargetPointY || coordY >= matrixHeight)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinMatrixSize, matrixHeight);
                throw new ArgumentException(errMsg);
            }

            int rounds;
            //Validate Rounds. Integer between Min and Max possible rounds
            if (!int.TryParse(args[2], out rounds) || rounds < GeneralConstants.MinRounds || rounds > GeneralConstants.MaxRounds)
            {
                var errMsg = string.Format(ErrMsg.NotCorrectWidth, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                throw new ArgumentException(errMsg);
            }

            var result = new List<int>() { coordX, coordY, rounds };

            return result;
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
