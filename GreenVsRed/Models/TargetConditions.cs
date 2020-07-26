using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Models
{
    public class TargetConditions : Point, ITargetConditions
    {
        public TargetConditions() : base() { }
        public TargetConditions(int x, int y) : base(x, y)
        {
            this.Rounds = 0;
        }

        public TargetConditions(int x, int y, int rounds) : this(x, y)
        {
            this.Rounds = rounds;
        }
        public int Rounds { get; private set; }
    }
}
