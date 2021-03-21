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
    private static int targetFps = 10;
    private static int cellSizeInPixels = 16;

    private int windowWidth => GameOfLife.Field.Width * cellSizeInPixels;
    private int windowHeight => GameOfLife.Field.Height * cellSizeInPixels;

    public GameOfLifeController(GameOfLife gameOfLife)
    {
      GameOfLife = gameOfLife;
    }

    public void Start()
    {
      Raylib.SetTargetFPS(targetFps);

      Raylib.InitWindow(windowWidth, windowHeight, "Game of Life");

      var update = true;

      while (!Raylib.WindowShouldClose())
      {
        #region Update
        if (update) GameOfLife.NextGeneration();
        #endregion

        #region User input
        // Pause updating.
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
          update = !update;
          if (update) Console.WriteLine("Updating.");
          else        Console.WriteLine("Updating paused.");
        }

        // Set a new random game of life
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F1))
        {
          GameOfLife = new GameOfLife();

          Raylib.SetWindowSize(windowWidth, windowHeight);
        }

        // TODO: Save state of field to a file.
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F5))
        {
          GameOfLife.SaveFieldToFile("path?");
        }

        // Hold down 1-4 key to set updating frequency. 
        // Releasing the key returns it to default frequency. 

        // 1/10 default speed.
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ONE))
        {
          Raylib.SetTargetFPS(1);
        }
        // 1/2 default speed.
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_TWO))
        {
          Raylib.SetTargetFPS(5);
        }
        // Three times default speed.
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_THREE))
        {
          Raylib.SetTargetFPS(30);
        }
        // Six times default speed.
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_FOUR))
        {
          Raylib.SetTargetFPS(60);
        }
        // Default speed.
        else
        {
          Raylib.SetTargetFPS(targetFps);
        }
        #endregion

        #region View
        Views.Raylib.In2D.FieldView.DrawIn2D(GameOfLife.Field, windowWidth, windowHeight);
        #endregion
      }
      Raylib.CloseWindow();
    }
  }
}
