namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Abstracts;
  using GameOfLife_KSICS.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class Field : IField
  {
    public ICell[,] Cells { get; set; }
    public ICell[,] NextCells { get; set; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    /// <summary>
    /// Create a field with the given width and height.
    /// </summary>
    /// <param name="width">Number of cells in width.</param>
    /// <param name="height">Number of cells in height.</param>
    public Field(int width, int height)
    {
      Width = width;
      Height = height;
      Cells = InitializeCells(Cells, width, height);
    }

    /// <summary>
    /// Initialize the cells of a two dimensional cell array.
    /// </summary>
    /// <param name="cells">Cell array to be initialized.</param>
    /// <param name="width">Number of cells in width.</param>
    /// <param name="height">Number of cells in height.</param>
    /// <returns>The initialized cell array.</returns>
    public ICell[,] InitializeCells(ICell[,] cells, int width, int height)
    {
      cells = new Cell[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          cells[x, y] = new Cell();
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
    public int GetNeighboursCount(int x, int y)
    {
      var count = 0;

      // Check the 8 surrounding cells alive states.
      // Clockwise, starting at 12
      if (IsAlive(x, y - 1)) count++; // 12
      if (IsAlive(x + 1, y - 1)) count++; // 13:30
      if (IsAlive(x + 1, y)) count++; // 15
      if (IsAlive(x + 1, y + 1)) count++; // 16:30
      if (IsAlive(x, y + 1)) count++; // 18
      if (IsAlive(x - 1, y + 1)) count++; // 19:30
      if (IsAlive(x - 1, y)) count++; // 21
      if (IsAlive(x - 1, y - 1)) count++; // 22:30

      // TODO: refactor if cases into for loop?
      // Check surrounding cells alive states.
      //for (int i = -1; i < 2; i++)
      //{
      //  for (int j = -1; j < 2; j++)
      //  {
      //    if (IsAlive(x + i, y + j) && (i != 0 && j != 0)) count++;
      //  }
      //}

      return count;
    }

    /// <summary>
    /// Get alive state for a cell at x and y. 
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <returns>
    /// Alive state of cell.
    /// </returns>
    protected bool IsAlive(int x, int y)
    {
      //// If position is out of bounds.
      //if (x < 0 || x >= Width) return false;
      //if (y < 0 || y >= Height) return false;

      return Cells[(x + Width) % Width, (y + Height) % Height].Alive;
    }

    /// <summary>
    /// Get the next cell at this position based on current cells alive state
    /// and count of living neighbours.
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <param name="neighboursCount">
    /// Count of living neighbours to this cell.
    /// </param>
    /// <returns>The next cell at this position in the field.</returns>
    public ICell NextCell(int x, int y, int neighboursCount)
    {
      // For Under- or overpopulation, don't need set age nor is it alive
      var nextCell = new Cell();
      
      // Stay alive
      if ((neighboursCount == 2 || neighboursCount == 3) && Cells[x, y].Alive)
      {
        nextCell.Alive = true;
        nextCell.Age = Cells[x, y].Age + 1;
      }
      // Bring alive
      else if (neighboursCount == 3 && !Cells[x, y].Alive)
      {
        nextCell.Alive = true;
      }

      return nextCell;
    }

    /// <summary>
    /// Add a formation of cells to the field.
    /// </summary>
    /// <param name="cellformation">Positions of cells that shape the cellformation.</param>
    /// <param name="pos">Where to position the cell formations upper left corner.</param>
    public void AddCellFormation(CellFormation cellformation, (int x, int y) pos)
    {
      foreach (var cell in cellformation.CellPositions)
      {
        Cells[(pos.x + cell.x + Width) % Width, (pos.y + cell.y + Height) % Height].Alive = true;
      }
    }

    /// <summary>
    /// // Create a string representation of the fields alive states and ages.
    /// </summary>
    /// <returns>The fields alive states and ages in rows and columns as they are positioned in the field.</returns>
    public override string ToString()
    {
      var fieldStr = "";

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          // alive + age
          fieldStr += (Convert.ToInt32(Cells[x, y].Alive) + Cells[x, y].Age) + " ";
        }
        fieldStr += "\n";
      }

      return fieldStr;
    }
  }
}
