using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Common.Validations
{
    public static class RegXPattern
    {
        public const string FirstLineStartWithDigits = @"^[1-9]{1}[\W]+[0-9]+$";

        public const string AllowedDigitsInMatrix = "^[01]+$";
    }
}
