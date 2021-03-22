namespace GameOfLife_KSICS.Interfaces
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  interface IView
  {
    public (int Width, int Height) WindowSize { get; set; }

    public void Draw(IField field);
  }
}
