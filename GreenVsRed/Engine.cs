// <copyright file="Engine.cs" company="PlaceholderCompany">
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
            this.MatrixService = new MatrixService();
        }

        public IMatrixService MatrixService { get; set; }

        public void Run()
        {
            while (true)
            {
                try
                {
                    this.MatrixService.CreateMatrix();

                    this.MatrixService.GetTargetConditions();

                    this.MatrixService.RecalculateMatrixNRounds();

                    var totalTargetBecomeGreen = this.MatrixService.TargetPointColors.Where(c => c == GeneralConstants.GreenNumber).ToList().Count;

                    this.MatrixService.WriteExpectedResult(totalTargetBecomeGreen);

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

                this.MatrixService.TargetPointColors = new List<int>();
            }
        }
    }
}
