using GreenVsRed.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Services
{
    public static class CommonService
    {
        public static Boolean WantToProceed()
        {
            var wantToProceed = false;
            while (true)
            {
                Console.WriteLine(GeneralConstants.WantToProceedStr);
                var answer = Console.ReadLine().Trim();
                var answerToLower = answer.ToLower();

                if (answerToLower == GeneralConstants.Yes)
                {
                    wantToProceed = true;
                    break;
                }

                if (answerToLower == GeneralConstants.No)
                {
                    wantToProceed = false;
                    break;
                }

            }

            return wantToProceed;
        }
    }
}
