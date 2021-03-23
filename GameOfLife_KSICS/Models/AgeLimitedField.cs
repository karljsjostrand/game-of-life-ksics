namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class AgeLimitedField : Field, IField
  {
    /// <summary>
    /// A cells age limit.
    /// </summary>
    public static int AgeLimit { get; } = 100;

    /// <summary>
    /// Create an OldAgeField with the given width and height. It's rules 
    /// differs from the standard field in such way that a cell dies
    /// when its age is past the specified age limit. 
    /// </summary>
    /// <param name="width">Number of cells in width.</param>
    /// <param name="height">Number of cells in height.</param>
    public AgeLimitedField(int width, int height) : base(width, height)
    {
    }

    /// <summary>
    /// Get the next cell at this position based on current cells alive state, 
    /// count of living neighbours, and the cells age.
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <param name="neighboursCount">
    /// This position's count of living neighbours.
    /// </param>
    /// <returns>Next generations cell at this position.</returns>
    public new Cell NextCell(int x, int y, int neighboursCount)
    {
      // For Under- or overpopulation, don't need set age nor is it alive.
      var nextCell = new Cell();

      // Stay alive.
      if ((neighboursCount == 2 || neighboursCount == 3) && Cells[x, y].Alive)
      {
        nextCell.Alive = true;
        nextCell.Age = Cells[x, y].Age + 1;
      }
      // Bring alive.
      else if (neighboursCount == 3 && !Cells[x, y].Alive)
      {
        nextCell.Alive = true;
      }

      // Age limit.
      if (Cells[x, y].Age > AgeLimit)
      {
        nextCell.Alive = false;
      }

      return nextCell;
    }
  }
}
