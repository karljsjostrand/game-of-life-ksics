namespace GameOfLife_KSICS.Models
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class Field
  {
    public Cell[,] Cells { get; set; }
    public Cell[,] NextCells { get; set; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Field(int width, int height)
    {
      Width = width;
      Height = height;
      Cells = InitializeCells(Cells, width, height);
    }

    public Cell[,] InitializeCells(Cell[,] cells, int width, int height)
    {
      cells = new Cell[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          cells[x, y] = new Cell(x, y);
        }
      }

      return cells;
    }


    /// <summary>
    /// Get a cells number of living neighbours. 
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <returns>
    /// Amount of neighbours alive next to this position, 
    /// vertically, horizontally, or diagonally, 0 to 8. 
    /// </returns>
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
    /// Update the cell at this position using a set of rules 
    /// based on it's count of neighbours alive.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="neighboursCount"></param>
    public Cell UpdateCell(int x, int y, int neighboursCount)
    {
      var nextCell = new Cell(x, y);

      if (neighboursCount < 2)
      {
        nextCell.Alive = false;
      }
      else if (neighboursCount == 2 && Cells[x, y].Alive)
      {
        nextCell.Alive = true;
      }
      else if (neighboursCount == 3)
      {
        nextCell.Alive = true;
      }
      // Overpopulation
      else
      {
        nextCell.Alive = false;
      }

      return nextCell;
    }
  }
}
