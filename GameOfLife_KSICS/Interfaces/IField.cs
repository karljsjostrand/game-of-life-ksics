namespace GameOfLife_KSICS.Interfaces
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public interface IField
  {
    /// <summary>
    /// Current generation of cells.
    /// </summary>
    public Cell[,] Cells { get; set; }

    /// <summary>
    /// Next generation of cells.
    /// </summary>
    public Cell[,] NextCells { get; set; }

    /// <summary>
    /// Width in number of cells.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Height in number of cells.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Initializes a two-dimensional array of cells with default property values.
    /// </summary>
    /// <returns>Initialized array of cells.</returns>
    public Cell[,] InitializedCells();

    /// <summary>
    /// Get a positions number of living neighbours. 
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <returns>
    /// Amount of neighbours alive next to this position, 
    /// vertically, horizontally, or diagonally, 0 to 8. 
    /// </returns>
    public int NeighboursCount(int x, int y);

    /// <summary>
    /// Get the next cell at this position based on current cells alive state
    /// and count of living neighbours.
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <param name="neighboursCount">
    /// This position's count of living neighbours.
    /// </param>
    /// <returns>Next generations cell at this position.</returns>
    public Cell NextCell(int x, int y, int neighboursCount);
  }
}
