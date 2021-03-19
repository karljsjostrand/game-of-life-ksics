namespace GameOfLife_KSICS.Utils
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  static class IntXYExtensions
  {
    public static int Up(this int i) => --i;
    public static int Up(this int i, int steps) => i - steps;

    public static int Down(this int i) => ++i;
    public static int Down(this int i, int steps) => i + steps;

    public static int Left(this int i) => i.Up();
    public static int Left(this int i, int steps) => i.Up(steps);

    public static int Right(this int i) => i.Down();
    public static int Right(this int i, int steps) => i.Down(steps);
  }
}
