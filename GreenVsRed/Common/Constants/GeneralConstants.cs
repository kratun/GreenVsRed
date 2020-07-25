using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Common.Constants
{
    public static class GeneralConstants
    {
        //Game Colors
        public const int GreenNumber = 1;
        public const int RedNumber = 0;

        //Matrix dimensions
        public const string EnterMatrixDimensions = "Enter matrix dimensions \"width, height\": ";
        public const string EnterMatrix = "PLease enter on each {0} lines, {1} digits({2} for green or {3} for red)";
        public const string EnterMatrixRow = "Enter line {0}: ";
        public const int MatrixDimension = 2;
        public const int MinMatrixSize = 1;
        public const int MaxMatrixSize = 999;

        //Target conditions
        public const string EnterTargetConditions = "Please write three integers separated by comma \",\": first targe point \"X\", then target point \"Y\" and then rounds (how many times you want to change matrix): ";
        public const int MinTargetPointX = 0;
        public const int MinTargetPointY = 0;
        public const int MinRounds = 0;
        public const int MaxRounds = int.MaxValue;
        public const int TargetConditionsCount = 3;

    }
}
