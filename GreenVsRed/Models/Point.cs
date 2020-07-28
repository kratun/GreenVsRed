﻿// <copyright file="Point.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Models
{
    /// <summary>
    /// Hold point X and Y provided by two methods.
    /// </summary>
    /// <inheritdoc cref="IPoint"/>
    public class Point : IPoint
    {
        private readonly int x;
        private readonly int y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        public Point()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="coordX">Point coordinate X.</param>
        /// <param name="coordY">Point coordinate Y.</param>
        public Point(int coordX, int coordY)
            : this()
        {
            this.x = coordX;
            this.y = coordY;
        }

        /// <inheritdoc cref="x"/>
        public int X => this.x;

        /// <inheritdoc cref="y"/>
        public int Y => this.y;
    }
}
