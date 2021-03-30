namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Interfaces;

  class AgeLimitedField : Field, IField
  {
    /// <summary>
    /// A cells age limit.
    /// </summary>
    public int AgeLimit { get; set; } = 100;

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
    /// <returns>Next generations cell at this position.</returns>
    public new Cell NextCell(int x, int y)
    {
      var neighboursCount = NeighboursCount(x, y);

      // For Under- or overpopulation, don't need set age nor is it alive.
      var nextCell = new Cell();

      // Stay alive.
      if ((neighboursCount == 2 || neighboursCount == 3) && Cells[x, y].IsAlive)
      {
        nextCell.IsAlive = true;
        nextCell.Age = Cells[x, y].Age + 1;
      }
      // Bring alive.
      else if (neighboursCount == 3 && !Cells[x, y].IsAlive)
      {
        nextCell.IsAlive = true;
      }

      // Age limit.
      if (Cells[x, y].Age > AgeLimit)
      {
        nextCell.IsAlive = false;
      }

      return nextCell;
    }
  }
}
