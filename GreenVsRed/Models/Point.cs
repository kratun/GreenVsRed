using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Models
{
    public class Point:IPoint
    {
        private readonly int x;
        private readonly int y;

        public Point() { }

        public Point(int coordX, int coordY):this()
        {
            this.x = coordX;
            this.y = coordY;
        }

        public int CoordX() => this.x;

        public int CoordY() => this.y;
    }
}
