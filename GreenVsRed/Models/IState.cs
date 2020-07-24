using System.Collections.Generic;

namespace GreenVsRed.Models
{
    public interface IState
    {
        int MatrixWidth { get; }
        int MatrixHeight { get; }
        IPoint Point { get; }

        int Rounds { get; }

        List<List<int>> Generation { get; set; }
        List<List<int>> GenerationNext { get; set; }
    }
}