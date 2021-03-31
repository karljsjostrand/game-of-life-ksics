namespace GameOfLife_KSICS.Views.Raylib.In2D
{
  using GameOfLife_KSICS.Interfaces;
  using Raylib_cs;

  class FieldView : IView
  {
    public Color CellColor { get; set; } = Color.RAYWHITE;

    private static Color backgroundColor = Color.BLANK;

    private const int borderThickness = 2;

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

      var cellWidth = (WindowSize.Width / field.Width) - borderThickness;
      var cellheight = (WindowSize.Height / field.Height) - borderThickness;

      for (int y = 0; y < field.Height; y++)
      {
        for (int x = 0; x < field.Width; x++)
        {
          // Don't draw dead cell.
          if (!field.Cells[x, y].IsAlive) continue;

          var posX = x * (cellWidth + borderThickness);
          var posY = y * (cellheight + borderThickness);

          // Color alpha by age. Dimmed to bright to dimmed.
          var alpha = field.Cells[x, y].Age switch
          {
            var i when (0 <= i && i <= 9) => 30 + (i * 25),
            var i when (9 < i && i < 19) => CellColor.a - ((i - 9) * 25),
            _ => CellColor.a - 225
          };

          var color = new Color(CellColor.r, CellColor.g, CellColor.b, alpha);

          // Draw the cell representation.
          Raylib.DrawRectangle(posX, posY, cellWidth, cellheight, color);
        }
      }

      Raylib.EndDrawing();
    }
  }
}
