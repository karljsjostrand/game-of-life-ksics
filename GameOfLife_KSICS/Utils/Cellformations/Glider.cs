﻿namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;
  using Utils;

  class Glider : CellFormation
  {
    public override (int Width, int Height) Size { get; } = (3, 3);

    public override void AddCells()
    {
      // coordinates shaping a glider formation
      Cells.AddRange(
        new List<(int x, int y)>()
        {
          (0, 1),
          (1, 2),
          (2, 0),
          (2, 1),
          (2, 2),
        });
    }
  }
}