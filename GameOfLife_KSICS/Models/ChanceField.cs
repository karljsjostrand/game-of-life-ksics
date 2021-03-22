namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class ChanceField : Field, IField
  {
    public ChanceField(int width, int height) : base(width, height) 
    {
    }

    /// <summary>
    /// Get total sum age of cells next to this position.
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <returns>Total sum of ages.</returns>
    private int GetTotalAgeOfNeighbours(int x, int y)
    {
      var totalAge = 0;

      // Check surrounding cells alive states.
      for (int i = -1; i < 2; i++)
      {
        for (int j = -1; j < 2; j++)
        {
          if (i != 0 && j != 0)
          {
            totalAge += Cells[(x + i + Width) % Width, (y + j + Height) % Height].Age;
          }
        }
      }

      return totalAge;
    }

    /// <summary>
    /// Get the next cell at this position based on current cells alive state
    /// count of living neighbours, and neighbours total age.
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <param name="neighboursCount">
    /// Count of living neighbours to this cell.
    /// </param>
    /// <returns>The next cell at this position in the field.</returns>
    public new ICell NextCell(int x, int y, int neighboursCount)
    {
      // For Under- or overpopulation, don't need set age nor is it alive
      var nextCell = new Cell();
      var rnd = new Random();
      var totalHighAge = 64;
      var chance = .01;

      // Stay alive
      if ((neighboursCount == 2 || neighboursCount == 3) && Cells[x, y].Alive)
      {
        nextCell.Alive = true;
        nextCell.Age = Cells[x, y].Age + 1;
      }
      // Bring alive when 3 neighbours
      else if (neighboursCount == 3 && !Cells[x, y].Alive)
      {
        nextCell.Alive = true;
      }
      // 1% Chance to bring alive when 2 neighbours and total age is high
      else if (neighboursCount == 2 && !Cells[x, y].Alive && GetTotalAgeOfNeighbours(x, y) > totalHighAge)
      {
        nextCell.Alive = rnd.NextDouble() <= chance;
      }

      return nextCell;
    }
  }
}
