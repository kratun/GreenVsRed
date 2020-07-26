using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Models
{
    public class Matrix : Point
    {
        
        public Matrix(int width, int height) : base(width, height) { }

        public int Width => base.CoordX();
        public int Height => base.CoordY();
       
    }
}
