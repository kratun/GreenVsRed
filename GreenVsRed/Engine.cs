using GreenVsRed.Common;
using GreenVsRed.Common.Constants;
using GreenVsRed.Models;
using GreenVsRed.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenVsRed
{
    public class Engine : IEngine
    {
        public Engine()
        {
            this.stateService = new StateService();
            this.matrixService = new MatrixService();
        }

        public IStateService stateService { get; set; }

        public IMatrixService matrixService { get; set; }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var matrixDimensions = stateService.GetMatrixDimensions();

                    this.matrixService.Generation = this.stateService.CreateMatrix(matrixDimensions.CoordX(), matrixDimensions.CoordY());

                    var targetConditions = this.stateService.GetTargetConditions(matrixDimensions.CoordX(), matrixDimensions.CoordY());

                    this.matrixService.ChangeGenerationNRounds(targetConditions.CoordX(), targetConditions.CoordY(), targetConditions.Rounds);

                    var totalTargetBecomeGreen = this.matrixService.TargetPointColors.Where(c => c == GeneralConstants.GreenNumber).ToList().Count;

                    this.stateService.WriteExpectedResult(totalTargetBecomeGreen);

                }
                catch (Exception e)
                {
                    if ((e is ArgumentException)||(e is ArgumentOutOfRangeException))
                    {
                        continue;
                    }
                    throw e;
                }

                var wantToProceed = CommonService.WantToProceed();

                if (!wantToProceed)
                {
                    break;
                }

                this.matrixService.TargetPointColors = new List<int>();
            }

        }

    }
}
