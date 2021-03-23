﻿namespace GameOfLife_KSICS.Interfaces
{
  using System;
  using System.Collections.Generic;
  using System.Text;

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
  }
}
