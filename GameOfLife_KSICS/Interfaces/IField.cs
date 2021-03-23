namespace GameOfLife_KSICS.Interfaces
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public interface IField
  {
    public Cell[,] Cells { get; set; }
    public Cell[,] NextCells { get; set; }

    public int Width { get; }
    public int Height { get; }

    public Cell[,] InitializeCells(Cell[,] cells, int width, int height);
    public Cell NextCell(int x, int y, int neighboursCount);
    public int GetNeighboursCount(int x, int y);
  }
}
