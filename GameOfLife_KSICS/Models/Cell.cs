namespace GameOfLife_KSICS.Models
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class Cell
  {
    public bool Alive { get; set; } = false;
    public int Age { get; set; } = 0;
    public int X { get; private set; } // TODO: remove?
    public int Y { get; private set; } // TODO: remove?

    public Cell(int x, int y)
    {
      X = x;
      Y = y;
    }
  }
}
