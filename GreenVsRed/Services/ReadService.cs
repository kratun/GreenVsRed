using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed.Services
{
    public class ReadService : IRead
    {
        public string ReadLine()
        {
            return Console.ReadLine().Trim();
        }
    }
}
