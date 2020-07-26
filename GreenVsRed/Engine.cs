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
                var matrixDimensions = stateService.GetMatrixDimention();

                this.matrixService.Generation = this.stateService.CreateMatrix(matrixDimensions.CoordX(), matrixDimensions.CoordY());

                //TODO Refactor this
                var point = new Point();
                var rounds = 0;
                while (true)
                {
                    try
                    {
                        var args = this.stateService.GetTargetConditions(matrixDimensions.CoordX(), matrixDimensions.CoordY());


                        var pointCoordX = args[0];
                        var pointCoordY = args[1];

                        point = new Point(pointCoordX, pointCoordY);
                        rounds = args[2];

                        break;
                    }
                    catch (Exception e)
                    {
                        if (e is ArgumentException)
                        {
                            Console.WriteLine(e.Message);
                        }
                        else { throw e; }

                    }
                }//Refactor Up

                this.matrixService.ChangeGenerationNRounds(point, rounds);

                var totalTargetBecomeGreen = this.matrixService.TargetPointColors.Where(c => c == GeneralConstants.GreenNumber).ToList().Count;

                this.stateService.WriteExpectedResult(totalTargetBecomeGreen);

                var wantToProceed = CommonService.WantToProceed();

                if (!wantToProceed)
                {
                    break;
                }

            }

        }

    }
}
