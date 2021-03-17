namespace GameOfLife_KSICS.Models
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  class Field
  {
    public Cell[,] Cells { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Field(int width, int height)
    {
      Width = width;
      Height = height;
      SetupCells(width, height);
    }

    private void SetupCells(int width, int height) // TODO: should this be in controller?
    {
      Cells = new Cell[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Cells[x, y] = new Cell(x, y);
        }
      }
    }

    public int GetNeighourCount(Cell cell)
    {
      throw new NotImplementedException();
    }
  }
}
