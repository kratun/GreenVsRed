using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Services
{
    public class WriteService : IWrite
    {
        public void Write(string outputMsg)
        {
            Console.Write(outputMsg.Trim());
        }

        public void WriteLine(string outputMsg)
        {
            Console.WriteLine(outputMsg.Trim());
        }
    }
}
