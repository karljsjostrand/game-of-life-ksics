namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Beacon : CellFormation
  {
    public override (int Width, int Height) Size { get; } = (4, 4);

    public override void AddCells()
    {
      // coordinates for a beacon formation
      CellPositions.AddRange(
        new List<(int x, int y)>()
        {
          (0, 0),
          (1, 0),
          (0, 1),
          (3, 2),
          (2, 3),
          (3, 3),
        });
    }
  }
}
