namespace GameOfLife_KSICS.Controllers.Raylib
{
  using System;
  using System.Collections.Generic;
  using System.Numerics;
  using System.Text;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils;
  using GameOfLife_KSICS.Utils.CellFormations;
  using GameOfLife_KSICS.Views.Raylib;
  using Raylib_cs;

  class GameOfLifeController
  {
    private GameOfLife gameOfLife;

    private IView view;

    private static readonly int defaultTargetFps = 10;
    private static readonly int cellSizeInPixels = 16;
    private static readonly string title = "Game of Life";

    private bool update = true;
    private int targetFps = defaultTargetFps;

    private int WindowWidth => gameOfLife.Field.Width * cellSizeInPixels;
    private int WindowHeight => gameOfLife.Field.Height * cellSizeInPixels;

    /// <summary>
    /// Create a user interface and a view for a Game of Life.
    /// </summary>
    /// <param name="gameOfLife">Game of Life API.</param>
    public GameOfLifeController(GameOfLife gameOfLife)
    {
      this.gameOfLife = gameOfLife;

      // Set view
      view = new Views.Raylib.In2D.FieldView(WindowWidth, WindowHeight);

      Start();
    }

    private void Start()
    {
      Raylib.SetTargetFPS(targetFps);

      Raylib.InitWindow(WindowWidth, WindowHeight, title);

      while (!Raylib.WindowShouldClose())
      {
        #region View
        view.Draw(gameOfLife.Field);
        #endregion

        #region User input
        HandleUserInput();
        #endregion


        #region Update
        if (update) gameOfLife.NextGeneration();
        #endregion

        
      }
      Raylib.CloseWindow();
    }

    private void HandleUserInput()
    {
      // Pauses updating.
      HandleUserPausingOrResuming();

      // Sets a new random game of life
      HandleUserSetsNewRandomGame();

      // Sets a new pre-defined game of life
      HandleUserSetsNewPreDefinedGame();

      // TODO: Saves state of field to a file.
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F5) || Raylib.IsKeyPressed(KeyboardKey.KEY_S))
      {
        throw new NotImplementedException();

        // Create a field file and save it
        var fieldFile = new FieldFile(gameOfLife.Field);
        fieldFile.Save("path?");

        gameOfLife.SaveFieldToFile("path?");
      }

      // Prints current state of field to console.
      HandleUserPrintsFieldToConsole();

      HandleUserSetsUpdateFrequency();
    }

    private void HandleUserPrintsFieldToConsole()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F6) || Raylib.IsKeyPressed(KeyboardKey.KEY_P))
      {
        Console.WriteLine(gameOfLife.Field.ToString());
      }
    }

    private void HandleUserPausingOrResuming()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
      {
        update = !update;
        if (update) Console.WriteLine("Updating.");
        else Console.WriteLine("Updating paused.");
      }
    }

    private void HandleUserSetsNewRandomGame()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F1) || Raylib.IsKeyPressed(KeyboardKey.KEY_R))
      {
        gameOfLife = new GameOfLife();

        view.WindowSize = (WindowWidth, WindowHeight);

        Raylib.SetWindowSize(WindowWidth, WindowHeight);
      }
    }

    private void HandleUserSetsNewPreDefinedGame()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F2) || Raylib.IsKeyPressed(KeyboardKey.KEY_N))
      {
        var field = new Field(80, 40);

        #region add some initial cellformations to field
        // add blinker
        field.AddCellFormation(new Blinker(), (-1, 0)); // place it through the edge

        //// add glider
        field.AddCellFormation(new Glider(), (10, 1));

        // add beehive
        field.AddCellFormation(new Beehive(), (20, 1));

        // snake
        field.AddCellFormation(new Snake(), (30, 1));

        // beacon
        field.AddCellFormation(new Beacon(), (40, 1));
        #endregion

        gameOfLife = new GameOfLife(field);

        view.WindowSize = (WindowWidth, WindowHeight);

        Raylib.SetWindowSize(WindowWidth, WindowHeight);
      }
    }

    private void HandleUserSetsUpdateFrequency()
    {
      // Holding down 1-4 key sets updating frequency.
      // 1-2 sets lower.
      // 3-4 sets higher.
      // Releasing the key returns it to default frequency. 

      // 1/10 default speed.
      if (Raylib.IsKeyDown(KeyboardKey.KEY_ONE))
      {
        var newFps = defaultTargetFps / 10;
        if (targetFps != newFps)
        {
          targetFps = newFps;
          Raylib.SetTargetFPS(targetFps);
        }
      }
      // 1/2 default speed.
      else if (Raylib.IsKeyDown(KeyboardKey.KEY_TWO))
      {
        var newFps = defaultTargetFps / 2;
        if (targetFps != newFps)
        {
          targetFps = newFps;
          Raylib.SetTargetFPS(targetFps);
        }
      }
      // Three times default speed.
      else if (Raylib.IsKeyDown(KeyboardKey.KEY_THREE))
      {
        var newFps = defaultTargetFps * 3;
        if (targetFps != newFps)
        {
          targetFps = newFps;
          Raylib.SetTargetFPS(targetFps);
        }
      }
      // Six times default speed.
      else if (Raylib.IsKeyDown(KeyboardKey.KEY_FOUR))
      {
        var newFps = defaultTargetFps * 6;
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
    }
  }
}
