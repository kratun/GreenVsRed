using GreenVsRed.Common;
using GreenVsRed.Models;
using System;
using System.Collections.Generic;

namespace GreenVsRed
{
    public class Engine : IEngine
    {
        public Engine()
        {
            
        }


        public void Run()
        {
            //
            //var point = new Point(1, 2);
            //var state = new State(3,3,point,10);

            //string m = "";
            //foreach (var g in state.Generation)
            //{
            //    if (g.Count>0)
            //    {
            //        m += string.Join(',', g);
            //    }

            //}
            //Console.WriteLine(state.MatrixWidth + " - " + state.MatrixHeight + " - " + m + "-"+state.Point.CoordX+"-"+state.Point.CoordY+"-"+state.Rounds);
            var count = 0;
            while (true)
            {
                try
                {
                    count++;
                   var state = new MatrixState();

                    
                    if (count == 5)
                    {
                        return;
                    }
                    
                    Console.WriteLine("All input is correct. Please wait for calculations!");
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        throw ex;
                    }


                }

            }

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
