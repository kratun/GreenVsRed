using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Models
{
    public class Point:IPoint
    {
        public Point() { }

        public Point(int coordX, int coordY)
        {
            this.CoordX = coordX;
            this.CoordY = coordY;
        }

        public int CoordX { get; private set; }

        public int CoordY { get; private set; }
    }
}
