// <copyright file="IMatrix.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide properties X (width) and Y (height) and collection for matrix state.
    /// </summary>
    /// <inheritdoc cref="IPoint"/>
    public interface IMatrix : IPoint
    {
        /// <summary>
        /// Gets or sets collection for matrix state.
        /// </summary>
        List<List<int>> State { get; set; }
    }
}
