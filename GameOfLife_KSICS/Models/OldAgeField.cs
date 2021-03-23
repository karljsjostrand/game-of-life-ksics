namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class OldAgeField : Field, IField
  {
    public OldAgeField(int width, int height) : base(width, height)
    {
    }

    /// <summary>
    /// Get the next cell at this position based on current cells alive state
    /// and count of living neighbours, and age.
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    /// <param name="y">Vertical position on the field.</param>
    /// <param name="neighboursCount">
    /// Count of living neighbours to this cell.
    /// </param>
    /// <returns>The next cell at this position in the field.</returns>
    public new Cell NextCell(int x, int y, int neighboursCount)
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

      // Old age
      if (Cells[x, y].Age > 50)
      {
        nextCell.Alive = false;
      }

      return nextCell;
    }
  }
}
