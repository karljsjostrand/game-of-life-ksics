namespace GameOfLife_KSICS.Models
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Field
  {
    Cell[,] Cells { get; set; }

    public Field(int width, int height)
    {
      Cells = new Cell[width, height];
    }
  }
}
