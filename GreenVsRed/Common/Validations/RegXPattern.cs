using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Common.Validations
{
    public static class RegXPattern
    {
        public const string FirstLine = @"^^[\W]*[0-9]+[\W]*[,][\W]*[0-9]+[\W]*$";

        public const string AllowedDigitsInMatrix = @"^[01]+$";

        public const string TargetConditions = @"^[\W]*[0-9]+[\W]*[,][\W]*[0-9]+[\W]*[,][\W]*[0-9]+[\W]*$";

    }
}
