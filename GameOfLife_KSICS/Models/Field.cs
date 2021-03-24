namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Abstracts;
  using GameOfLife_KSICS.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class Field : IField
  {
    public Cell[,] Cells { get; set; }
    public Cell[,] NextCells { get; set; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    /// <summary>
    /// Create a field of the given width and height and initialize cells.
    /// </summary>
    /// <param name="width">Number of cells in width.</param>
    /// <param name="height">Number of cells in height.</param>
    public Field(int width, int height)
    {
      Width = width;
      Height = height;
      Cells = InitializedCells();
    }

    public Cell[,] InitializedCells()
    {
      var cells = new Cell[Width, Height];

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          cells[x, y] = new Cell();
        }
      }

      return cells;
    }

    public int NeighboursCount(int x, int y)
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
      return Cells[(x + Width) % Width, (y + Height) % Height].Alive;
    }

    public Cell NextCell(int x, int y, int neighboursCount)
    {
      // TODO: get the neighbourscount from here, dont need to send it as parameter...?

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
          // Alive + Age
          fieldStr += (Convert.ToInt32(Cells[x, y].Alive) + Cells[x, y].Age) + " ";
        }
        fieldStr += "\n";
      }

      return fieldStr;
    }
  }
}
