using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Common.Constants
{
    public static class ErrMsg
    {
        
        public const string NotAllowedCharacterInLine = "There are not allowed charecters in line {0} - \"{1}\". You have to write {2} digits - {3} for green or {4} for red";
        public const string MatrixDimentionException = "Please write width and height of the matrix in pattern \"width, height\"";
        public const string NotCorrectWidth = "Width must be integer between {0} and {1} and can not be greater then height - \"width, height\"";
        public const string NotCorrectHeight = "Height must be integer between {0} and {1} - \"width, height\"";
        public const string NotCorrectDigitsCount = "The line {0} - \"{1}\" must contains {2} digits ({3} for green or {4} for red) without space between";

    }
}
