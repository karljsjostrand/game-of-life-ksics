namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using System.Collections.Generic;

  class Blinker : CellFormation
  {
    public override (int Width, int Height) Size { get; } = (3, 1);

    public override void AddCells()
    {
      // coordinates for a horizontal blinker.
      CellPositions.AddRange(
        new List<(int x, int y)>() 
        { 
          (0, 0),
          (1, 0),
          (2, 0),
        });
    }
  }
}
