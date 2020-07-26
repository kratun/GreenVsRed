using GreenVsRed.Models;
using System.Collections.Generic;

namespace GreenVsRed.Services
{
    public interface IMatrixService
    {
        List<List<int>> Generation { get; set; }
        List<int> TargetPointColors { get; set; }

        void ChangeGenerationNRounds(int coordX, int coordY, int rounds);
    }
}