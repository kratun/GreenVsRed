// <copyright file="RegXPattern.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Common.Validations
{
    /// <summary>
    /// RegXPattern is a static class holds constant string used for regular expressions.
    /// </summary>
    public static class RegXPattern
    {
        /// <summary>
        /// FirstLine is a constant string pattern for matrix dimensions input.
        /// </summary>
        public const string FirstLine = @"^^[\W]*[0-9]+[\W]*[,][\W]*[0-9]+[\W]*$";

        /// <summary>
        /// AllowedDigitsInMatrix is a constant string pattern for matrix row input.
        /// </summary>
        public const string AllowedDigitsInMatrix = @"^[01]+$";

        /// <summary>
        /// TargetConditions is a constant string pattern for the last input - target conditions.
        /// </summary>
        public const string TargetConditions = @"^[\W]*[0-9]+[\W]*[,][\W]*[0-9]+[\W]*[,][\W]*[0-9]+[\W]*$";
    }
}
