// <copyright file="GeneralConstants.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GreenVsRed.Common.Constants
{
    /// <summary>
    /// Static Class GeneralConstants contains all constants except error constants.
    /// </summary>
    public static class GeneralConstants
    {
        // Game Colors

        /// <summary>
        /// GreenNumber is an integer and hold constant for green number.
        /// </summary>
        public const int GreenNumber = 1;

        /// <summary>
        /// RedNumber is an integer and hold constant for red number.
        /// </summary>
        public const int RedNumber = 0;

        // Matrix dimensions

        /// <summary>
        /// EnterMatrixDimensions is an string for message: "Enter matrix dimensions "width, height": ".
        /// </summary>
        public const string EnterMatrixDimensions = "Enter matrix dimensions \"width, height\": ";

        /// <summary>
        /// EnterMatrix is an string for message: "Please enter on each next {X-th} lines, {Y-th} digits({GreenNumber} for green or {RedNumber} for red)".
        /// </summary
        public const string EnterMatrix = "Please enter on each next {0} lines, {1} digits({2} for green or {3} for red)";

        /// <summary>
        /// EnterMatrixRow is an string for message: "Enter line currentLine}: ".
        /// </summary
        public const string EnterMatrixRow = "Enter line {0}: ";

        /// <summary>
        /// MatrixDimension is an integer and hold constant for count of the matrix dimensions.
        /// </summary>
        public const int MatrixDimension = 2;

        /// <summary>
        /// MinMatrixSize is an integer and hold constant for minimum size of the matrix.
        /// </summary>
        public const int MinMatrixSize = 1;

        /// <summary>
        /// MinMatrixSize is an integer and hold constant for maximum size of the matrix.
        /// </summary>
        public const int MaxMatrixSize = 999;

        // Target conditions

        /// <summary>
        /// EnterTargetConditions is a constant string for message: "Please write three integers separated by comma ",": first targe point "X", then target point "Y" and then rounds (how many times you want to change matrix): ".
        /// </summary>
        public const string EnterTargetConditions = "Please write three integers separated by comma \",\": first targe point \"X\", then target point \"Y\" and then rounds (how many times you want to recalulate matrix): ";

        /// <summary>
        /// EnterTargetConditions is a constant string for message: "All inputs are correct.".
        /// </summary>
        public const string CorrectArgsStr = "All inputs are correct.";

        /// <summary>
        /// MinTargetPointX is an integer and hold constant for minimum X for target point.
        /// </summary>
        public const int MinTargetPointX = 0;

        /// <summary>
        /// MinTargetPointY is an integer and hold constant for minimum Y for target point.
        /// </summary>
        public const int MinTargetPointY = 0;

        /// <summary>
        /// MinRounds is an integer and hold constant for minimum Rounds which is used to recalculate matrix.
        /// </summary>
        public const int MinRounds = 0;

        /// <summary>
        /// MaxRounds is an integer and hold constant for maximum Rounds which is used to recalculate matrix.
        /// </summary>
        public const int MaxRounds = int.MaxValue;

        /// <summary>
        /// TargetConditionsCount is an constant integer. It represent count of last arguments.
        /// </summary>
        public const int TargetConditionsCount = 3;

        // Next Color constraints

        /// <summary>
        /// GreenPointStayGreenCondition is a constant string used for rule "green cell stay green" which contains numbers separated by comma ",".
        /// </summary>
        public const string GreenPointStayGreenCondition = "2, 3, 6";

        /// <summary>
        /// RedPointBecomeGreenCondition is a constant string used for rule "red cell become green" which contains numbers separated by comma ",".
        /// </summary>
        public const string RedPointBecomeGreenCondition = "3, 6";

        // Others

        /// <summary>
        /// ExpectedResult is a constant string used to show the result of the game.
        /// </summary>
        public const string ExpectedResult = "# expected result: ";

        // Want to proceed "Please wait for calculations!"

        /// <summary>
        /// WaitCalculations is a constant string for message: "Please wait calculations!".
        /// </summary>
        public const string WaitCalculations = "Please wait calculations!";

        /// <summary>
        /// WantToProceedStr is a constant string for message: "Do you want to proceed? (Yes/No)".
        /// </summary>
        public const string WantToProceedStr = "Do you want to proceed? (Yes/No)";

        /// <summary>
        /// Yes is a constant string for answer: "yes".
        /// </summary>
        public const string Yes = "yes";

        /// <summary>
        /// No is a constant string for answer: "no".
        /// </summary>
        public const string No = "no";

        /// <summary>
        /// Repeat is a constant string if you want tot start game again.
        /// </summary>
        public const string RepeatProcess = "repeat";

        /// <summary>
        /// Restart is a constant string to restart game.
        /// </summary>
        public const string RestartGame = "restart";

        /// <summary>
        /// End is a constant string to canceling game.
        /// </summary>
        public const string EndGame = "end";

        /// <summary>
        /// Repeat is a constant string if you want tot start game again.
        /// </summary>
        public const int StartPositionIndex = -1;
    }
}
