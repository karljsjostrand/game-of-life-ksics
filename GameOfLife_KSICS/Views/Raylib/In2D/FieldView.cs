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
    private static Color backgroundColor = Color.BLANK;
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
      Raylib.ClearBackground(backgroundColor);

      var borderThickness = 1;
      var cellWidth = WindowSize.Width / field.Width - borderThickness;
      var cellheight = WindowSize.Height / field.Height - borderThickness;

      for (int y = 0; y < field.Height; y++)
      {
        for (int x = 0; x < field.Width; x++)
        {
          // Don't draw dead cell.
          if (!field.Cells[x, y].IsAlive) continue;

          var posX = x * (cellWidth + borderThickness);
          var posY = y * (cellheight + borderThickness);

          // Color by alive or age.
          var color = field.Cells[x, y].Age switch
          {
            0 => colorWhenAlive,
            1 => colorWhenYoung,
            2 => colorWhenLessYoung,
            3 => colorWhenOld,
            _ => colorWhenOlder,
          };

          // Draw the cell representation.
          Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);
        }
      }

      Raylib.EndDrawing();
    }
  }
}
