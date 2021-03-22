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
    private static int defaultTargetFps = 10;
    private int targetFps = defaultTargetFps;
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
        #region User input
        // Pauses updating.
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
          update = !update;
          if (update) Console.WriteLine("Updating.");
          else        Console.WriteLine("Updating paused.");
        }

        // Sets a new random game of life
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F1) || Raylib.IsKeyPressed(KeyboardKey.KEY_R))
        {
          GameOfLife = new GameOfLife();

          Raylib.SetWindowSize(windowWidth, windowHeight);
        }

        // Sets a new pre-defined game of life
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F2) || Raylib.IsKeyPressed(KeyboardKey.KEY_N))
        {
          var field = new Field(80, 40);

          #region add some initial cellformations to field
          // add blinker
          field.AddCellFormation(new Blinker(), (-1, 0));

          //// add glider
          field.AddCellFormation(new Glider(), (10, 1));

          // add beehive
          field.AddCellFormation(new Beehive(), (20, 1));

          // snake
          field.AddCellFormation(new Snake(), (30, 1));

          // beacon
          field.AddCellFormation(new Beacon(), (40, 1));
          #endregion

          GameOfLife = new GameOfLife(field);

          Raylib.SetWindowSize(windowWidth, windowHeight);
        }

        // TODO: Saves state of field to a file.
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F5) || Raylib.IsKeyPressed(KeyboardKey.KEY_S))
        {
          GameOfLife.SaveFieldToFile("path?");
        }

        // TODO: Prints current state of field to console.
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F6) || Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        {
          Console.WriteLine(GameOfLife.Field.ToString());
        }

        // Holding down 1-4 key sets updating frequency.
        // 1-2 sets lower.
        // 3-4 sets higher.
        // Releasing the key returns it to default frequency. 

        // 1/10 default speed.
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ONE))
        {
          var newFps = 1;
          if (targetFps != newFps)
          {
            targetFps = newFps;
            Raylib.SetTargetFPS(targetFps);
          }
        }
        // 1/2 default speed.
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_TWO))
        {
          var newFps = 5;
          if (targetFps != newFps)
          {
            targetFps = newFps;
            Raylib.SetTargetFPS(targetFps);
          }
        }
        // Three times default speed.
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_THREE))
        {
          var newFps = 30;
          if (targetFps != newFps)
          {
            targetFps = newFps;
            Raylib.SetTargetFPS(targetFps);
          }
        }
        // Six times default speed.
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_FOUR))
        {
          var newFps = 60;
          if (targetFps != newFps)
          {
            targetFps = newFps;
            Raylib.SetTargetFPS(targetFps);
          }
        }
        // Default speed.
        else
        {
          if (targetFps != defaultTargetFps)
          {
            targetFps = defaultTargetFps;
            Raylib.SetTargetFPS(targetFps);
          }
        }
        #endregion

        #region Update
        if (update) GameOfLife.NextGeneration();
        #endregion

        #region View
        Views.Raylib.In2D.FieldView.DrawIn2D(GameOfLife.Field, windowWidth, windowHeight);
        #endregion
      }
      Raylib.CloseWindow();
    }
  }
}
