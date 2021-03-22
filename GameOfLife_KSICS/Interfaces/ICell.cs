namespace GameOfLife_KSICS.Interfaces
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  public interface ICell
  {
    public bool Alive { get; set; }
    public int Age { get; set; }
  }
}
