namespace GameOfLife_KSICS.Models
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Cell
  {
    public bool Alive { get; set; } = false;
    public int X { get; private set; }
    public int Y { get; private set; }

    public Cell(int x, int y)
    {
      X = x;
      Y = y;
    }
  }
}
