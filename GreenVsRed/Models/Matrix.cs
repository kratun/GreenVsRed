// <copyright file="Matrix.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide properties X (width) and Y (height) and collection for matrix state.
    /// </summary>
    /// <inheritdoc cref="Point"/>
    /// <inheritdoc cref="IMatrix"/>
    public class Matrix : Point, IMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        public Matrix()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="x">Matrix X (width).</param>
        /// <param name="y">Matrix Y (heught).</param>
        public Matrix(int x, int y)
            : base(x, y)
        {
            this.State = new List<List<int>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="x">Matrix X (width).</param>
        /// <param name="y">Matrix Y (heught).</param>
        /// <param name="state">Matrix collection.</param>
        public Matrix(int x, int y, List<List<int>> state)
            : this(x, y)
        {
            this.State = state;
        }

        /// <summary>
        /// Gets or sets collection of greens and reds.
        /// </summary>
        public List<List<int>> State { get; set; }
    }
}
