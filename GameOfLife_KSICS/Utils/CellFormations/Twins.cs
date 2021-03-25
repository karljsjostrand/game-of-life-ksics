namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Twins : CellFormation
  {
    public override (int Width, int Height) Size { get; } = (4, 3);

    public override void AddCells()
    {
      // 1111
      // 1001
      // 1111
      // coordinates for a flower formation
      CellPositions.AddRange(
        new List<(int x, int y)>()
        {
          // row 1
          (0, 0),
          (1, 0),
          (2, 0),
          (3, 0),

          // row 2
          (0, 1),
          (3, 1),

          // row 3
          (0, 2),
          (1, 2),
          (2, 2),
          (3, 2),
        });
    }
  }
}
