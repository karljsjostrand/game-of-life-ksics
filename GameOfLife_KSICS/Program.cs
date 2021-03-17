namespace GameOfLife_KSICS
{
  using System;
  using System.Numerics;
  using GameOfLife_KSICS.Models;
  using Raylib_cs;

  class Program
  {
    static void Main()
    {
      Views.Raylib.FieldView.DrawIn2D();
      //Views.Raylib.FieldView.DrawIn3D();
    }
  }
}
