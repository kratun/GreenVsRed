using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Common.Constants
{
    public static class ErrMsg
    {
        
        public const string NotAllowedCharacterInLine = "Error: There are not allowed charecters in line {0} - \"{1}\". You have to write {2} digits - {3} for green or {4} for red";
        public const string MatrixDimentionException = "Error: Please write width and height of the matrix in pattern \"width, height\"";
        public const string NotCorrectWidth = "Error: Width must be integer between {0} and {1} and can not be greater then height - \"width, height\"";
        public const string NotCorrectHeight = "Error: Height must be integer between {0} and {1} - \"width, height\"";
        public const string NotCorrectDigitsCount = "Error: The line {0} - \"{1}\" must contains {2} digits ({3} for green or {4} for red) without space between";

        public const string TargetConditionsException = "Error: Tagret condition missmatch. Please write three integers separated by comma \",\" - first targe point \"X\", then target point \"Y\" and rounds (how many times you want to change matrix)";
        public const string NotCorrectTargetPointY = "Error: Target point Y must be an integer between {0} and {1}";
        public const string NotCorrectTargetPointX = "Error: Target point X must be an integer between {0} and {1}";


    }
}
