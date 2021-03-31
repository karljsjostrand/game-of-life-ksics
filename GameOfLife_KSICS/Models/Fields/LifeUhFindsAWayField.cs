namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Interfaces;
  using System;

  public class LifeUhFindsAWayField : Field, IField
  {
    /// <summary>
    /// The total age required by neighbouring cells for an additional possibility of new life.
    /// </summary>
    public int HighTotalAge { get; set; } = 64;

    /// <summary>
    /// Chance for a new life in a cell when total age of neighbours is high enough.
    /// Default is .01, yielding a 1% chance.
    /// </summary>
    public double ChanceForNewLife { get; set; } = .01;

    /// <summary>
    /// Create a field with the given width and height. It's rules 
    /// differs from the standard field in such way that a cell has a
    /// chance to become alive with only 2 neighbours if the total 
    /// age of neighbouring cells is high enough.
    /// </summary>
    /// <param name="width">Number of cells in width.</param>
    /// <param name="height">Number of cells in height.</param>
    public LifeUhFindsAWayField(int width, int height) : base(width, height) 
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

      // Count surrounding cells ages.
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
    /// This position's count of living neighbours.
    /// </param>
    /// <returns>Next generations cell at this position.</returns>
    public new Cell NextCell(int x, int y)
    {
      var neighboursCount = NeighboursCount(x, y);

      // For Under- or overpopulation, don't need set age nor is it alive.
      var nextCell = new Cell();
      var rnd = new Random();

      // Stay alive.
      if ((neighboursCount == 2 || neighboursCount == 3) && Cells[x, y].IsAlive)
      {
        nextCell.IsAlive = true;
        nextCell.Age = Cells[x, y].Age + 1;
      }
      // Bring alive when 3 neighbours.
      else if (neighboursCount == 3 && !Cells[x, y].IsAlive)
      {
        nextCell.IsAlive = true;
      }
      // Chance to bring alive when 2 neighbours and total age is high.
      else if (neighboursCount == 2 && !Cells[x, y].IsAlive && GetTotalAgeOfNeighbours(x, y) > HighTotalAge)
      {
        nextCell.IsAlive = rnd.NextDouble() <= ChanceForNewLife;
      }

      return nextCell;
    }
  }
}
