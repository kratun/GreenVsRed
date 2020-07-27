// <copyright file="TargetConditions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Models
{
    /// <summary>
    /// Provide Target Condition properties: Point and Rounds.
    /// </summary>
    /// <inheritdoc cref="Point"/>
    /// <inheritdoc cref="ITargetConditions"/>
    public class TargetConditions : Point, ITargetConditions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConditions"/> class.
        /// </summary>
        public TargetConditions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConditions"/> class.
        /// </summary>
        /// <param name="x">Target point X.</param>
        /// <param name="y">Target point Y.</param>
        public TargetConditions(int x, int y)
            : base(x, y)
        {
            this.Rounds = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConditions"/> class.
        /// </summary>
        /// <param name="rounds">Matrix recalculation rounds.</param>
        public TargetConditions(int rounds)
            : base()
        {
            this.Rounds = rounds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConditions"/> class.
        /// </summary>
        /// <param name="x">Target point X.</param>
        /// <param name="y">Target point Y.</param>
        /// <param name="rounds">Matrix recalculation rounds.</param>
        public TargetConditions(int x, int y, int rounds)
            : this(x, y)
        {
            this.Rounds = rounds;
        }

        /// <summary>
        /// Gets matrix recalculation rounds.
        /// </summary>
        public int Rounds { get; private set; }
    }
}
