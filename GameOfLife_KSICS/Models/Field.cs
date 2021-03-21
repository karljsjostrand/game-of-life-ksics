﻿namespace GameOfLife_KSICS.Models
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

    public Field(int width, int height)
    {
      Width = width;
      Height = height;
      Cells = InitializeCells(Cells, width, height);
    }

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

      // Clockwise, starting at 12
      if (IsAlive(x,     y - 1)) count++; // 12
      if (IsAlive(x + 1, y - 1)) count++; // 13:30
      if (IsAlive(x + 1, y    )) count++; // 15
      if (IsAlive(x + 1, y + 1)) count++; // 16:30
      if (IsAlive(x,     y + 1)) count++; // 18
      if (IsAlive(x - 1, y + 1)) count++; // 19:30
      if (IsAlive(x - 1, y    )) count++; // 21
      if (IsAlive(x - 1, y - 1)) count++; // 22:30

      return count;
    }

    /// <summary>
    /// Gets alive state for a cell at x and y. 
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <returns>
    /// Alive state of cell.
    /// </returns>
    private bool IsAlive(int x, int y)
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
      var nextCell = new Cell();
      if (Cells[x, y] is Cell) nextCell.Age = (Cells[x, y] as Cell).Age;

      // Underpopulation?
      if (neighboursCount < 2)
      {
        nextCell.Alive = false;
        if (nextCell is Cell) (nextCell as Cell).Age = 0;
      }
      // Stay alive
      else if ((neighboursCount == 2 || neighboursCount == 3) && Cells[x, y].Alive)
      {
        nextCell.Alive = true;
        if (nextCell is Cell) (nextCell as Cell).Age++;
      }
      // Bring alive
      else if (neighboursCount == 3 && !Cells[x, y].Alive)
      {
        nextCell.Alive = true;
        if (nextCell is Cell) (nextCell as Cell).Age = 0;
      }
      // Overpopulation
      else
      {
        nextCell.Alive = false;
        if (nextCell is Cell) (nextCell as Cell).Age = 0;
      }

      return nextCell;
    }

    public void AddCellFormation(CellFormation cellformation, (int x, int y) pos)
    {
      foreach (var cell in cellformation.Cells)
      {
        Cells[pos.x + cell.x, pos.y + cell.y].Alive = true;
      }
    }
  }
}
