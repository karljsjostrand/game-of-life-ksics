namespace GameOfLife_KSICS.Interfaces
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  public interface IField
  {
    public ICell[,] Cells { get; set; }
    public ICell[,] NextCells { get; set; }

    public int Width { get; }
    public int Height { get; }

    public ICell[,] InitializeCells(ICell[,] cells, int width, int height);
    public ICell UpdateCell(int x, int y, int neighboursCount);
    public int GetNeighboursCount(int x, int y);
  }
}
