namespace GameOfLife_KSICS.Views.Raylib.In2D
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using Raylib_cs;

  class FieldView : IView
  {
    private static Color borderColor = Color.BLACK;
    private static Color colorWhenAlive = Color.DARKGRAY;
    private static Color colorWhenDead = Color.BLACK;
    private static Color colorWhenYoung = Color.GRAY;
    private static Color colorWhenLessYoung = Color.LIGHTGRAY;
    private static Color colorWhenOld = Color.WHITE;
    private static Color colorWhenOlder = Color.GOLD;

    public (int Width, int Height) WindowSize { get; set; }

    /// <summary>
    /// Create an instance of a field view that can draw in 2D.
    /// </summary>
    /// <param name="windowWidth">Width of drawable space in pixels.</param>
    /// <param name="windowHeight">Height of drawable space in pixels.</param>
    public FieldView(int windowWidth, int windowHeight)
    {
      WindowSize = (windowWidth, windowHeight);
    }

    public void Draw(IField field)
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(borderColor);

      var cellWidth = WindowSize.Width / field.Width - 1;
      var cellheight = WindowSize.Height / field.Height - 1;

      for (int y = 0; y < field.Height; y++)
      {
        for (int x = 0; x < field.Width; x++)
        {
          var posX = x * (WindowSize.Width / field.Width);
          var posY = y * (WindowSize.Height / field.Height);

          // Color according to alive state of cell.
          var color = field.Cells[x, y].Alive ? colorWhenAlive : colorWhenDead;

          if (field.Cells[x, y].Alive && field.Cells[x, y].Age > 0)
          {
            // Color by age.
            color = field.Cells[x, y].Age switch
            {
              1 => colorWhenYoung,
              2 => colorWhenLessYoung,
              3 => colorWhenOld,
              _ => colorWhenOlder,
            };
          }

          // Draw the cell representation.
          Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);
        }
      }
      Raylib.EndDrawing();
    }
  }
}
