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
    private static Color colorWhenAlive = Color.DARKPURPLE;
    private static Color colorWhenDead = Color.DARKGRAY;
    private static Color borderColor = Color.BLACK;

    public static void DrawIn2D(IField field, int windowWidth, int windowHeight)
    {
      var info = false; // TODO: remove
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

          Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);

          //if (info) // TODO: remove
          //{
          //  Raylib.DrawText($"{cell.X},", posX + 1, posY + 1, 1, Color.BLACK); // TODO: remove
          //  Raylib.DrawText($"{cell.Y}", posX + 1, posY + 10, 1, Color.BLACK); // TODO: remove
          //  Raylib.DrawText($"{Raylib.GetMousePosition()}", Raylib.GetMouseX(), Raylib.GetMouseY(), 10, Color.WHITE); // TODO: remove
          //}
        }
      }

      //Raylib.DrawFPS(5, 5); // TODO: remove
      Raylib.EndDrawing();
    }
  }
}
