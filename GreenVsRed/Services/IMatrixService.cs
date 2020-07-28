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
        /// Get matrix X (width) and Y (height).
        /// </summary>
        /// <param name="inputArgsStr">A string contains two integers separated by comma.</param>
        /// <exception cref="ArgumentException">Thrown when line
        /// contains not allowed character or not enougth parameters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when X (width) or Y (heght) are out of game range.</exception>
        public void GetMatrixDimensions(string inputArgsStr);

        /// <summary>
        /// Gets matrix Y (height).
        /// </summary>
        /// <returns>Matrix height.</returns>
        int GetMatrixHeight();

        /// <summary>
        /// Gets matrix Y (width).
        /// </summary>
        /// <returns>Matrix width.</returns>
        int GetMatrixWidth();

        /// <summary>
        /// Create matrix row from input.
        /// </summary>
        /// <param name="inputArgsStr">Matrix row as string.</param>
        /// <exception cref="ArgumentException">Return exception if input contains not enogth or not allowed charakters.</exception>
        void CreateMatrixRow(string inputArgsStr);

        /// <summary>
        /// Get target point dimensions X and Y and rounds to recalculate matrix.
        /// </summary>
        /// <param name="inputArgsStr">three integers separated with comma: target point X and Y and N rounds to recalculate matrix.</param>
        void GetGameConditions(string inputArgsStr);

        /// <summary>
        /// Gets how many times target point become green.
        /// </summary>
        /// <returns>How many time taget point become green.</returns>
        int GetTargetPointGreenColorsCount();

        /// <summary>
        /// Method who recalculate Matrix N rounds and return how many times the target point become green.
        /// </summary>
        void RecalculateMatrixNRounds();
    }
}