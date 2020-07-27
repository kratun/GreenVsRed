// <copyright file="IPoint.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Models
{
    /// <summary>
    /// IPoint interface with two methods: int CoodX() and int CoordY().
    /// </summary>
    public interface IPoint
    {
        /// <summary>
        /// CoordX() for coordinate X.
        /// </summary>
        /// <returns>Integer.</returns>
        int CoordX();

        /// <summary>
        /// CoordY() for coordinate Y.
        /// </summary>
        /// <returns>Integer.</returns>
        int CoordY();
    }
}