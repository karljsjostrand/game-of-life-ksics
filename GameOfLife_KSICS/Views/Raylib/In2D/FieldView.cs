namespace GameOfLife_KSICS.Views.Raylib.In2D
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using GameOfLife_KSICS.Models;
  using Raylib_cs;

  class FieldView
  {
    private static Color colorWhenAlive = Color.DARKPURPLE;
    private static Color colorWhenDead = Color.DARKGRAY;
    private static Color borderColor = Color.BLACK;

    public static void DrawIn2D(Field field, int windowWidth, int windowHeight)
    {
      var info = true; // TODO: remove
      Raylib.BeginDrawing();
      Raylib.ClearBackground(borderColor);

      var cellWidth = windowWidth / field.Width - 1;
      var cellheight = windowHeight / field.Height - 1;
      foreach (var cell in field.Cells)
      {
        var posX = cell.X * (windowWidth / field.Width);
        var posY = cell.Y * (windowHeight / field.Height);

        // color according to alive state of cell
        var color = cell.Alive ? colorWhenAlive : colorWhenDead;

        Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);

        if (info) // TODO: remove
        {
          Raylib.DrawText($"{cell.X},", posX + 1, posY + 1, 1, Color.BLACK); // TODO: remove
          Raylib.DrawText($"{cell.Y}", posX + 1, posY + 10, 1, Color.BLACK); // TODO: remove
          Raylib.DrawText($"{Raylib.GetMousePosition()}", Raylib.GetMouseX(), Raylib.GetMouseY(), 10, Color.WHITE); // TODO: remove
        }
      }
      Raylib.DrawFPS(5, 5); // TODO: remove
      Raylib.EndDrawing();
    }
  }
}
