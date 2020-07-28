// <copyright file="IPoint.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Models
{
    /// <summary>
    /// Provide vector X and Y or width and height of a matrix.
    /// </summary>
    public interface IPoint
    {
        /// <summary>
        /// Gets a vector X or width of a matrix.
        /// </summary>
        int X { get; }

        /// <summary>
        /// Gets a vector Y or height of a matrix.
        /// </summary>
        int Y { get; }
    }
}