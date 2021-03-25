namespace GameOfLife_KSICS.Abstracts
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines a formation of cells for a two-dimensional field.
  /// </summary>
  public abstract class CellFormation
  {
    /// <summary>
    /// Width and height of the formation.
    /// </summary>
    public abstract (int Width, int Height) Size { get; }

    /// <summary>
    /// Horizontal and vertical coordinates for the positions that is the formation.
    /// </summary>
    public List<(int x, int y)> CellPositions { get; } = new List<(int x, int y)>();

    /// <summary>
    /// Calls the AddCells() method to set and add cell positions.
    /// </summary>
    public CellFormation()
    {
      AddCells();
    }

    /// <summary>
    /// Called in constructor.
    /// Sets and adds cell positions of the formation.
    /// </summary>
    public abstract void AddCells();
  }
}
