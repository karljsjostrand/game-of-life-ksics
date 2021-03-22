namespace GameOfLife_KSICS.Abstracts
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public abstract class CellFormation
  {
    public abstract (int Width, int Height) Size { get; }
    public List<(int x, int y)> CellPositions { get; } = new List<(int x, int y)>();

    public CellFormation()
    {
      AddCells();
    }

    /// <summary>
    /// Called in constructor.
    /// Set and add cell positions.
    /// </summary>
    public abstract void AddCells();
  }
}
