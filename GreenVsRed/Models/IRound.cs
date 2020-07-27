// <copyright file="IRound.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Models
{
    /// <summary>
    /// Has int property with getter.
    /// </summary>
    public interface IRound
    {
        /// <summary>
        /// Gets integer Rounds - matrix recalculation counts.
        /// </summary>
        int Rounds { get; }
    }
}