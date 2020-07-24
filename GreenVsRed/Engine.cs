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
            var point = new Point();
            state = new State(0, 0, point, 10);
        }

        public IState state { get; set; }

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
                    if (count == 5)
                    {
                        return;
                    }
                    this.state = CommonMethods.InitialState();
                    string m = "";
                    foreach (var g in state.Generation)
                    {
                        if (g.Length > 0)
                        {
                            m += string.Join(',', g);
                        }

                    }

                    Console.WriteLine(state.MatrixWidth + " - " + state.MatrixHeight + " - " + m + "-" + state.Point.CoordX + "-" + state.Point.CoordY + "-" + state.Rounds);
                    count++;
                    //Initialize matrix
                    //Console.WriteLine("Engine");
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
