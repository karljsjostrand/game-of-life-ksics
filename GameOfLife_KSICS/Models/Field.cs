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
        //var neighbourCount = GetNeighourCount(cell);

        //switch (neighbourCount)
        //{
        //  case 3:
        //    cell.Alive = true;
        //    break;
        //  default:
        //    break;
        //}
      }
    }

    /// <summary>
    /// Get a cells number of living neighbours. (0 to 8)
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public int GetNeighourCount(int x, int y)
    {
      var count = 0;





      if (x == 0 || y == 0 || x == Width || y == Height)
      {
        if (y == 0) // top
        {
          if (x == 0) // left edge
          {

          }
          else if (x == Width) // right edge
          {

          }
          else // not on either vertical edge
          {

          }
        }
        if (y == Height) // btm
        {
          if (x == 0) // left edge
          {

          }
          else if (x == Width) // right edge
          {

          }
          else // not on either vertical edge
          {

          }
        }
        if (x == 0) // left edge
        {

        }
        if (x == Width) // right edge
        {

        } 
      }
      else // not on any edge
      {
        // Clockwise, starting at 12
        if (Cells[x, y - 1].Alive) count++; // 12
        if (Cells[x + 1, y - 1].Alive) count++; // 13:30
        if (Cells[x - 1, y - 1].Alive) count++; // 22:30

        if (Cells[x + 1, y].Alive) count++; // 15
        if (Cells[x + 1, y + 1].Alive) count++; // 16:30
        if (Cells[x, y + 1].Alive) count++; // 18
        if (Cells[x - 1, y + 1].Alive) count++; // 19:30
        if (Cells[x - 1, y].Alive) count++; // 21
      }

      return count;
    }

    public void UpdateCell(Cell cell, int neighboursCount)
    {

    }
  }
}
