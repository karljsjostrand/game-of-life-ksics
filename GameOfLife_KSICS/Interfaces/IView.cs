namespace GameOfLife_KSICS.Interfaces
{
  interface IView
  {
    /// <summary>
    /// Size of window to the draw in.
    /// </summary>
    public (int Width, int Height) WindowSize { get; set; }

    /// <summary>
    /// Draw a fields state.
    /// Should be called from within a Raylib window.
    /// </summary>
    /// <param name="field">Field to draw.</param>
    public void Draw(IField field);

    /// <summary>
    /// Gets and sets the color of the rectangle representing the cell.
    /// </summary>
    public Raylib_cs.Color CellColor { get; set; }
  }
}
