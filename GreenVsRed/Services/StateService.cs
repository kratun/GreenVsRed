using GreenVsRed.Common.Constants;
using GreenVsRed.Common.Validations;
using GreenVsRed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenVsRed.Services
{
    /// <exception cref="ArgumentException">Thrown when line 
    /// contains not allowed character.</exception>
    /// <remarks>
    public class StateService : IStateService
    {
        public StateService()
        {
            this.WriteService = new WriteService();
            this.ReadService = new ReadService();
        }

        public IWrite WriteService { get; set; }
        public IRead ReadService { get; set; }
        /// <exception cref="ArgumentException">Thrown when line 
        /// contains not allowed character.</exception>
        /// <exception cref="ArgumentException">Thrown when line 
        /// contains not enougth parameters.</exception>
        public IPoint GetMatrixDimensions()
        {
            WriteService.Write(GeneralConstants.EnterMatrixDimensions);
            // Read first line and trim it. 
            var input = Console.ReadLine();

            try
            {
                var result = this.TryGetPoint(input, GeneralConstants.MatrixDimension, GeneralConstants.MinMatrixSize, GeneralConstants.MaxMatrixSize);
                return result;
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    WriteService.WriteLine(e.Message);
                    throw e;
                }
                else
                {
                    throw e;
                }
            }

        }

        public List<List<int>> CreateMatrix(int matrixWidth, int matrixHeight)
        {
            var matrix = new List<List<int>>();
            WriteService.WriteLine(string.Format(GeneralConstants.EnterMatrix, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));
            for (int i = 0; i < matrixHeight; i++)
            {
                WriteService.Write(string.Format(GeneralConstants.EnterMatrixRow, i + 1));
                var inputRow = ReadService.ReadLine();

                //Validate allowed digits in matrix
                if (!Regex.IsMatch(inputRow, RegXPattern.AllowedDigitsInMatrix))
                {
                    var errMsg = string.Format(ErrMsg.NotAllowedCharacterInLine, i + 1, inputRow, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber);
                    WriteService.WriteLine(errMsg);
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
                    WriteService.WriteLine(errMsg);
                    i--;
                    continue;
                }

                matrix.Add(listItem);
            }

            return matrix;
        }

        public ITargetConditions GetTargetConditions(int matrixWidth, int matrixHeight)
        {
            ITargetConditions targetConditions = new TargetConditions();
            while (true)
            {
                try
                {
                    WriteService.Write(string.Format(GeneralConstants.EnterTargetConditions, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));

                    var input = ReadService.ReadLine();

                    targetConditions = ValidateTargetConditions(input, matrixWidth, matrixHeight);

                    WriteAllConditionsOk();

                    return targetConditions;
                }
                catch (Exception e)
                {
                    if ((e is ArgumentException)|| (e is ArgumentOutOfRangeException))
                    {
                        WriteService.WriteLine(e.Message);
                    }
                    else { throw e; }

                }
            }

        }

        private IPoint TryGetPoint(string input, int validArgsCount, int minSize, int maxSize)
        {
            var separators = new char[] { ',', ' ' };

            var args = input
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            //Validate first line that contain two argument 
            if (!Regex.IsMatch(input, RegXPattern.FirstLine) || args.Length != validArgsCount)
            {
                var errMsg = ErrMsg.MatrixDimentionException;
                throw new ArgumentException(errMsg);
            }


            int height;
            //Validate Height that is number between Min and Max possible
            if (!int.TryParse(args[1], out height) || height < minSize || height > maxSize)
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

            //Validate first line that contain three argument 
            var isValidInput = Regex.IsMatch(input, RegXPattern.TargetConditions);
            if (!isValidInput || args.Count != GeneralConstants.TargetConditionsCount)
            {
                var errMsg = ErrMsg.TargetConditionsException;
                throw new ArgumentException(errMsg);
            }

            int coordX;
            //Validate Target Point X. Integer between Min Target Point X and Matrix Width
            if (!int.TryParse(args[0], out coordX))
            {
                var errMsg = string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth);
                throw new ArgumentException(nameof(coordX), errMsg);
            }

            int coordY;
            //Validate Target Point Y. Integer between Min Target Point Y and Matrix Width
            if (!int.TryParse(args[1], out coordY))
            {
                var errMsg = string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinMatrixSize, matrixHeight);
                throw new ArgumentException(errMsg);
            }

            if ((coordX < GeneralConstants.MinTargetPointX || coordX >= matrixWidth) && (coordY < GeneralConstants.MinTargetPointY || coordY >= matrixHeight))
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY, GeneralConstants.MinTargetPointX, matrixWidth - 1, GeneralConstants.MinTargetPointY, matrixHeight - 1);
                errMsg += ("\n" + string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth - 1));
                errMsg += ("\n" + string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinTargetPointY, matrixHeight - 1));

                throw new ArgumentOutOfRangeException("",errMsg);
            }
            else if (coordX < GeneralConstants.MinTargetPointX || coordX >= matrixWidth)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY, GeneralConstants.MinTargetPointX, matrixWidth - 1, GeneralConstants.MinTargetPointY, matrixHeight - 1);
                errMsg += ("\n" + string.Format(ErrMsg.NotCorrectTargetPointX, GeneralConstants.MinTargetPointX, matrixWidth - 1));

                throw new ArgumentOutOfRangeException("", errMsg);
            }
            else if (coordY < GeneralConstants.MinTargetPointY || coordY >= matrixHeight)
            {
                var errMsg = string.Format(ErrMsg.OutOfRangeTargetPoint, coordX, coordY);
                errMsg += ("\n" + string.Format(ErrMsg.NotCorrectTargetPointY, GeneralConstants.MinTargetPointY, matrixHeight - 1));

                throw new ArgumentOutOfRangeException("", errMsg);
            }

            int rounds;
            //Validate Rounds. Integer between Min and Max possible rounds
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
            WriteService.WriteLine(GeneralConstants.CorrectArgsStr);
        }

        public void WriteExpectedResult(int result)
        {
            WriteService.WriteLine(GeneralConstants.ExpectedResult + result);
        }
    }
}
