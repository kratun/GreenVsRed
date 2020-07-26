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

                //TODO Refactor to Class TargetCondition in state service
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
                }

                this.matrixService.ChangeGenerationNRounds(point, rounds);

                var totalTargetBecomeGreen = this.matrixService.TargetPointColors.Where(c => c == GeneralConstants.GreenNumber).ToList().Count;

                this.stateService.WriteExpectedResult(totalTargetBecomeGreen);

                Console.WriteLine(GeneralConstants.WantToProceedStr);
                
                var wantToProceed = false;
                while (true)
                {
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

                if (!wantToProceed)
                {
                    break;
                }
                 
            }
            
            //var count = 0;
            //while (true)
            //{
            //    try
            //    {
            //        count++;
            //       var state = new MatrixState();


            //        if (count == 5)
            //        {
            //            return;
            //        }

            //        Console.WriteLine("All input is correct. Please wait for calculations!");
            //    }
            //    catch (Exception ex)
            //    {
            //        if (ex is ArgumentException)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //        else
            //        {
            //            throw ex;
            //        }


            //    }

            //}

        }

        private void InitializeMatrix(List<List<int>> generation)
        {
            throw new NotImplementedException();
        }

        //public List<string> InitializeMatrix()
        //{
        //    Console.WriteLine("InitializeMatrix");
        //    return null;
        //}
    }
}
