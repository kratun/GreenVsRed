using GreenVsRed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed
{
    public interface IEngine
    {
        IState state { get; set; }
        void Run();
    }
}
