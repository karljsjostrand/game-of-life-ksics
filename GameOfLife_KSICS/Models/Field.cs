namespace GameOfLife_KSICS.Models
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class Field
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

    public void NextGeneration()
    {
      foreach (var cell in Cells)
      {
        var neighbourCount = GetNeighbourCount(cell.X, cell.Y);

        UpdateCell(cell.X, cell.Y, neighbourCount); // TODO
      }
    }

    /// <summary>
    /// Get a cells number of living neighbours. (0 to 8)
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public int GetNeighbourCount(int x, int y)
    {
      var count = 0;

      // Clockwise, starting at 12
      if (IsLivingCellAt(x,     y - 1)) count++; // 12
      if (IsLivingCellAt(x + 1, y - 1)) count++; // 13:30
      if (IsLivingCellAt(x + 1, y    )) count++; // 15
      if (IsLivingCellAt(x + 1, y + 1)) count++; // 16:30
      if (IsLivingCellAt(x,     y + 1)) count++; // 18
      if (IsLivingCellAt(x - 1, y + 1)) count++; // 19:30
      if (IsLivingCellAt(x - 1, y    )) count++; // 21
      if (IsLivingCellAt(x - 1, y - 1)) count++; // 22:30

      return count;
    }

    /// <summary>
    /// Gets alive state for a cell at x and y. 
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <returns>
    /// true if cell is alive, false if it isn't or position is out of bounds.
    /// </returns>
    private bool IsLivingCellAt(int x, int y)
    {
      // If position is out of bounds.
      if (x < 0 || x >= Width) return false;
      if (y < 0 || y >= Height) return false;

      return Cells[x, y].Alive;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="neighboursCount"></param>
    public void UpdateCell(int x, int y, int neighboursCount)
    {
      throw new NotImplementedException();

      if (neighboursCount < 2)
      {

      }
      else if (neighboursCount == 2 || neighboursCount == 3)
      {

      }
      else
      {
        // dead
      }
    }
  }
}
