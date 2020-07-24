using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Models
{
    class State : IState
    {
        public State(IPoint point)
        {
            this.Point = point;

            this.Generation = new int[0][];
            this.GenerationNext = new List<List<int>>();
        }
        public State(int matrixWidth, int matrixHeight, IPoint point, int rounds)
        {
            this.MatrixWidth = matrixWidth;
            this.MatrixHeight = matrixHeight;
            this.Point = point;
            this.Rounds = rounds;
            this.Generation = new int[matrixHeight][];
            this.GenerationNext = new List<List<int>>();
        }


        public int MatrixWidth { get; private set; }

        public int MatrixHeight { get; private set; }

        public IPoint Point { get; private set; }

        public int Rounds { get; private set; }

        public int[][] Generation { get; set; }
        public List<List<int>> GenerationNext { get; set; }

    }
}
