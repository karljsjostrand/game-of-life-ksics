namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Snake : CellFormation
  {
    public override (int Width, int Height) Size { get; } = (4, 2);

    public override void AddCells()
    {
      // coordinates for a snake formation
      CellPositions.AddRange(
        new List<(int x, int y)>()
        {
          (0, 0),
          (2, 0),
          (3, 0),
          (0, 1),
          (1, 1),
          (3, 1),
        });
    }
  }
}
