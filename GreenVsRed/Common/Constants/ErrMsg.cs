// <copyright file="ErrMsg.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Common.Constants
{
    /// <summary>
    /// Static Class ErrMsg contains all error messages.
    /// </summary>
    public static class ErrMsg
    {
        /// <summary>
        /// Error: There are not allowed charecters in N-th line.
        /// </summary>
        public const string NotAllowedCharacterInLine = "Error: There are not allowed charecters in line {0} - \"{1}\". You have to write {2} digits - {3} for green or {4} for red";

        /// <summary>
        /// Error: Please write width and height of the matrix in pattern "width, height".
        /// </summary>
        public const string MatrixDimentionException = "Error: Please write width and height of the matrix in pattern \"width, height\"";

        /// <summary>
        /// Error: Width must be an integer between MIN and MAX and can not be greater then height - "width, height".
        /// </summary>
        public const string NotCorrectWidth = "Error: Width must be an integer between {0} and {1} and can not be greater then height - \"width, height\"";

        /// <summary>
        /// Error: Width is out of range. It must be an integer between MIN and MAX and can not be greater then height - "width, height".
        /// </summary>
        public const string OutOfRangeWidth = "Error: Width is out of range. It must be an integer between {0} and {1} and can not be greater then height";

        /// <summary>
        /// Error: Height must be an integer between MIN and MAX.
        /// </summary>
        public const string NotCorrectHeight = "Error: Height must be an integer between {0} and {1} - \"width, height\"";

        /// <summary>
        /// Error: Height is out of range. It must be an integer between MIN and MAX and can not be greater then height - "width, height".
        /// </summary>
        public const string OutOfRangeHeight = "Error: Height is out of range. It must be an integer between {0} and {1}";

        /// <summary>
        /// Error: The line must contains digits equal to the Matrix Width digits (1 for green or 0 for red) without space between.
        /// </summary>
        public const string NotCorrectDigitsCount = "Error: The line {0} - \"{1}\" must contains {2} digits ({3} for green or {4} for red) without space between";

        /// <summary>
        /// Error: Tagret condition missmatch. Please write three integers separated by comma "," - first targe point "X", then target point "Y" and rounds (how many times you want to change matrix).
        /// </summary>
        public const string TargetConditionsException = "Error: Tagret condition missmatch. Please write three integers separated by comma \",\" - first targe point \"X\", then target point \"Y\" and rounds (how many times you want to change matrix)";

        /// <summary>
        /// Error: Point Y must be an integer between MIN and MAX.
        /// </summary>
        public const string NotCorrectTargetPointY = "Error: Point Y must be an integer between {0} and {1}";

        /// <summary>
        /// Error: Point X must be an integer between MIN and MAX.
        /// </summary>
        public const string NotCorrectTargetPointX = "Error: Point X must be an integer between {0} and {1}";

        /// <summary>
        /// Error: Target point [X,Y] is out of range.
        /// </summary>
        public const string OutOfRangeTargetPoint = "Error: Target point [{0},{1}] is out of range";

        /// <summary>
        /// Error: Target point [X,Y] is out of range.
        /// </summary>
        public const string MatrixIsFull = "Error: Matrix is full. It is not possible to enter more rows.";
    }
}
