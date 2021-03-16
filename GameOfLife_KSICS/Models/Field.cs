namespace GameOfLife_KSICS.Models
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Field
  {
    public Cell[,] Cells { get; private set; }

    public Field(int width, int height)
    {
      SetupCells(width, height);
    }

    public void SetupCells(int width, int height)
    {
      Cells = new Cell[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Cells[x, y] = new Cell();
        }
      }
    }
  }
}
