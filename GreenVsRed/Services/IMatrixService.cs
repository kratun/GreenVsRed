// <copyright file="IMatrixService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface provides matrix and color collections, and method to recalculate matrix N round. 
    /// </summary>
    public interface IMatrixService
    {
        List<List<int>> Matrix { get; set; }
        List<int> TargetPointColors { get; set; }

        void RecalculateMatrixNRounds(int coordX, int coordY, int rounds);
    }
}