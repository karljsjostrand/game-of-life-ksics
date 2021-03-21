namespace GameOfLife_KSICS.Controllers.Raylib
{
  using System;
  using System.Collections.Generic;
  using System.Numerics;
  using System.Text;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils.CellFormations;
  using GameOfLife_KSICS.Views.Raylib;
  using Raylib_cs;

  class GameOfLifeController
  {
    private GameOfLife GameOfLife { get; set; }

    private static int windowWidth = 1200;
    private static int windowHeight = 600;
    private static int targetFps = 10;
    private static int cellSizeInPixels = 16;

    public GameOfLifeController(GameOfLife gameOfLife)
    {
      GameOfLife = gameOfLife;

      windowWidth = gameOfLife.Field.Width * cellSizeInPixels;
      windowHeight = gameOfLife.Field.Height * cellSizeInPixels;
    }

    public void Start()
    {
      Raylib.SetTargetFPS(targetFps);

      Raylib.InitWindow(windowWidth, windowHeight, "Game of Life");

      while (!Raylib.WindowShouldClose())
      {
        #region user input
        // TODO: click cells to turn them alive or dead?
        var mousePosition = Raylib.GetMousePosition();

        if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE))
        {
          GameOfLife.NextGeneration();
        }

        GameOfLife.NextGeneration(); // TODO: GameOfLife.Run()?

        if (Raylib.IsKeyReleased(KeyboardKey.KEY_S)) // TODO
        {
          GameOfLife.SaveFieldToFile("path?");
        }

        #endregion
        #region view
        Views.Raylib.In2D.FieldView.DrawIn2D(GameOfLife.Field, windowWidth, windowHeight);
        #endregion
      }
      Raylib.CloseWindow();
    }
  }
}
