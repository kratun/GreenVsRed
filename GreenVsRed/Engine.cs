﻿// <copyright file="Engine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GreenVsRed.Common.Constants;
    using GreenVsRed.Services;

    /// <summary>
    /// Provide method to run game.
    /// </summary>
    /// <inheritdoc cref="IEngine"/>
    public class Engine : IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            this.WriteService = new WriteService();
            this.ReadService = new ReadService();
            this.MatrixService = new MatrixService();
        }

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
        /// Gets or sets properties and methods that used to create and recalculating matrix.
        /// </summary>
        private IMatrixService MatrixService { get; set; }

        /// <summary>
        /// Method that run game.
        /// </summary>
        /// <exception cref="Exception">Can throw exception.</exception>
        public void Run()
        {
            while (true)
            {
                try
                {
                    var result = string.Empty;

                    result = this.TryGetMatrixDeimensions();
                    if (result == GeneralConstants.RestartGame)
                    {
                        continue;
                    }
                    else if (result == GeneralConstants.EndGame)
                    {
                        return;
                    }

                    result = this.CreateMatrix();
                    if (result == GeneralConstants.RestartGame)
                    {
                        continue;
                    }
                    else if (result == GeneralConstants.EndGame)
                    {
                        return;
                    }

                    result = this.GetTargetConditions();
                    if (result == GeneralConstants.RestartGame)
                    {
                        continue;
                    }
                    else if (result == GeneralConstants.EndGame)
                    {
                        return;
                    }

                    this.RecalculateMatrixNRounds();

                    this.WriteExpectedResult();
                }
                catch (Exception e)
                {
                    if ((e is ArgumentException) || (e is ArgumentOutOfRangeException))
                    {
                        continue;
                    }

                    throw e;
                }

                var wantToProceed = CommonService.WantToProceed();

                if (!wantToProceed)
                {
                    break;
                }

                this.MatrixService.Reset();
            }
        }

        private void WriteExpectedResult()
        {
            var result = this.MatrixService.GetTargetPointGreenColorsCount();
            var strResult = GeneralConstants.ExpectedResult + result;
            this.WriteService.WriteLine(strResult);
        }

        private void RecalculateMatrixNRounds()
        {
            this.WriteService.WriteLine(GeneralConstants.WaitCalculations);

            this.MatrixService.RecalculateMatrixNRounds();
        }

        private string GetTargetConditions()
        {
            while (true)
            {
                try
                {
                    var matrixHeight = this.MatrixService.GetMatrixHeight();
                    var matrixWidth = this.MatrixService.GetMatrixWidth();

                    this.WriteService.Write(string.Format(GeneralConstants.EnterTargetConditions, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));

                    var inputArgsStr = this.ReadService.ReadLine();
                    if (inputArgsStr.ToLower() == GeneralConstants.EndGame)
                    {
                        return GeneralConstants.EndGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RestartGame)
                    {
                        return GeneralConstants.RestartGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RepeatProcess)
                    {
                        continue;
                    }

                    this.MatrixService.GetGameConditions(inputArgsStr);

                    this.WriteService.WriteLine(GeneralConstants.CorrectArgsStr);

                    return string.Empty;
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

        private string CreateMatrix()
        {
            var matrixHeight = this.MatrixService.GetMatrixHeight();
            var matrixWidth = this.MatrixService.GetMatrixWidth();

            this.WriteService.WriteLine(string.Format(GeneralConstants.EnterMatrix, matrixHeight, matrixWidth, GeneralConstants.GreenNumber, GeneralConstants.RedNumber));

            for (int i = 0; i < matrixHeight; i++)
            {
                try
                {
                    this.WriteService.Write(string.Format(GeneralConstants.EnterMatrixRow, i + 1));
                    var inputArgsStr = this.ReadService.ReadLine();

                    if (inputArgsStr.ToLower() == GeneralConstants.EndGame)
                    {
                        return GeneralConstants.EndGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RestartGame)
                    {
                        return GeneralConstants.RestartGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RepeatProcess)
                    {
                        i = GeneralConstants.StartPositionIndex;

                        inputArgsStr = $"{matrixWidth},{matrixHeight}";
                        this.MatrixService.GetMatrixDimensions(inputArgsStr);

                        continue;
                    }

                    this.MatrixService.CreateMatrixRow(inputArgsStr);
                }
                catch (ArgumentException e)
                {
                    this.WriteService.WriteLine(e.Message);
                    i--;
                }
            }

            return string.Empty;
        }

        private string TryGetMatrixDeimensions()
        {
            while (true)
            {
                this.WriteService.Write(GeneralConstants.EnterMatrixDimensions);

                try
                {
                    var inputArgsStr = this.ReadService.ReadLine();
                    if (inputArgsStr.ToLower() == GeneralConstants.EndGame)
                    {
                        return GeneralConstants.EndGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RestartGame)
                    {
                        return GeneralConstants.RestartGame;
                    }
                    else if (inputArgsStr.ToLower() == GeneralConstants.RepeatProcess)
                    {
                        continue;
                    }

                    this.MatrixService.GetMatrixDimensions(inputArgsStr);
                    return string.Empty;
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
        }
    }
}
