// <copyright file="IMatrixService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Services
{
    using System.Collections.Generic;

    using GreenVsRed.Models;

    /// <summary>
    /// Provides methods that create and recalculate matrix N times.
    /// </summary>
    public interface IMatrixService
    {
        /// <summary>
        /// Create matrix from input.
        /// </summary>
        void CreateMatrix();

        /// <summary>
        /// Get target point dimensions and rounds to recalculate matrix.
        /// </summary>
        void GetTargetConditions();

        /// <summary>
        /// Write expected result.
        /// </summary>
        void WriteExpectedResult();

        /// <summary>
        /// Method who recalculate Matrix N rounds and return how many times the target point become green.
        /// </summary>
        void RecalculateMatrixNRounds();
    }
}