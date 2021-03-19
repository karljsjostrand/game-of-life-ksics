namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  interface ICellFormation
  {
    public (int Width, int Height) Size { get; }

    public void AddToField(Field field, (int x, int y) pos);
  }
}
