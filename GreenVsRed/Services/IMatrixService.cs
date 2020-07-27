// <copyright file="IMatrixService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Services
{
    using System.Collections.Generic;

    using GreenVsRed.Models;

    /// <summary>
    /// Provides properties and methods that create and recalculating matrix N times.
    /// </summary>
    public interface IMatrixService
    {
        /// <summary>
        /// Gets or sets current matrix state.
        /// </summary>
        List<List<int>> Matrix { get; set; }

        /// <summary>
        /// Gets or sets collection of each target point color during recalculation.
        /// </summary>
        List<int> TargetPointColors { get; set; }

        /// <summary>
        /// Create matrix from the input.
        /// </summary>
        void CreateMatrix();

        void GetTargetConditions();

        void WriteExpectedResult();

        /// <summary>
        /// Method who recalculate Matrix N rounds and return how many times the target point become green.
        /// </summary>
        void RecalculateMatrixNRounds();
    }
}