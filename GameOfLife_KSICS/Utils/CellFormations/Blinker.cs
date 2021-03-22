namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

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
