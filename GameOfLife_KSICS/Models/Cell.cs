namespace GameOfLife_KSICS.Models
{
  using GameOfLife_KSICS.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class Cell : ICell
  {
    public bool Alive { get; set; } = false;
    public int Age { get; set; } = 0;

    /// <summary>
    /// Creates a cell with it's alive state set to false and age at 0.
    /// </summary>
    public Cell() { }
  }
}
