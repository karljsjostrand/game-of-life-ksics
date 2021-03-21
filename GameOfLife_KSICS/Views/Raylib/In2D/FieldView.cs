namespace GameOfLife_KSICS.Views.Raylib.In2D
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using Raylib_cs;

  class FieldView
  {
    private static Color borderColor = Color.BLACK;
    private static Color colorWhenAlive = Color.DARKGRAY;
    private static Color colorWhenDead = Color.BLACK;
    private static Color colorWhenYoung = Color.GRAY;
    private static Color colorWhenAdult = Color.LIGHTGRAY;
    private static Color colorWhenOld = Color.WHITE;
    private static Color colorWhenOlder = Color.GOLD;

    public static void DrawIn2D(IField field, int windowWidth, int windowHeight)
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(borderColor);

      var cellWidth = windowWidth / field.Width - 1;
      var cellheight = windowHeight / field.Height - 1;

      for (int y = 0; y < field.Height; y++)
      {
        for (int x = 0; x < field.Width; x++)
        {
          var posX = x * (windowWidth / field.Width);
          var posY = y * (windowHeight / field.Height);

          // Color according to alive state of cell.
          var color = field.Cells[x, y].Alive ? colorWhenAlive : colorWhenDead;

          // Color by age.
          if (field.Cells[x, y].Alive && field.Cells[x, y] is Cell)
          {
            if ((field.Cells[x, y] as Cell).Age == 1) color = colorWhenYoung;
            if ((field.Cells[x, y] as Cell).Age == 2) color = colorWhenAdult;
            if ((field.Cells[x, y] as Cell).Age == 3) color = colorWhenOld;
            if ((field.Cells[x, y] as Cell).Age >= 4) color = colorWhenOlder;
          }

          Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);
        }
      }
      Raylib.EndDrawing();
    }
  }
}
