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
  }
}
