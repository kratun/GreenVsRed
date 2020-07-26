using GreenVsRed.Models;
using System.Collections.Generic;

namespace GreenVsRed.Services
{
    public interface IStateService
    {
        List<List<int>> CreateMatrix(int matrixWidth, int matrixHeight);
        IPoint GetMatrixDimention();
        ITargetConditions GetTargetConditions(int matrixWidth, int matrixHeight);
        void WriteExpectedResult(int result);
    }
}