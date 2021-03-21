namespace GameOfLife_KSICS.Abstracts
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public abstract class CellFormation
  {
    public abstract (int Width, int Height) Size { get; }
    public List<(int x, int y)> Cells { get; } = new List<(int x, int y)>();

    public CellFormation()
    {
      AddCells();
    }

    /// <summary>
    /// Called in constructor.
    /// Define and add cells of the formation to Cells list.
    /// </summary>
    public abstract void AddCells();
  }
}
