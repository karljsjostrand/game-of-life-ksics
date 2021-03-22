namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Beehive : CellFormation
  {
    public override (int Width, int Height) Size { get; } = (4, 3);

    public override void AddCells()
    {
      // coordinates for a beehive formation
      CellPositions.AddRange(
        new List<(int x, int y)>()
        {
          (0, 1),
          (1, 0),
          (2, 0),
          (3, 1),
          (1, 2),
          (2, 2),
        });
    }
  }
}
