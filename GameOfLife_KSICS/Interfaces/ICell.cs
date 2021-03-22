namespace GameOfLife_KSICS.Interfaces
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  public interface ICell
  {
    public bool Alive { get; set; }

    /// <summary>
    /// Number of generations it's been alive.
    /// </summary>
    public int Age { get; set; }
  }
}
