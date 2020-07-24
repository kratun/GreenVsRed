using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenVsRed.Models
{
    class State : IState
    {
        public State()
        {

        }
        public State(int matrixWidth, int matrixHeight, IPoint point, int rounds)
        {
            this.MatrixWidth = matrixWidth;
            this.MatrixHeight = matrixHeight;
            this.Point = point;
            this.Rounds = rounds;
            this.Generation = new List<List<int>>();
            this.GenerationNext = new List<List<int>>();
        }

        
        public int MatrixWidth { get; private set; }

        public int MatrixHeight { get; private set; }

        public IPoint Point { get; private set; }

        public int Rounds { get; private set; }

        public List<List<int>> Generation { get; set; }
        public List<List<int>> GenerationNext { get; set; }

    }
}
