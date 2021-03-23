namespace GameOfLife_KSICS.Controllers.Raylib
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
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
    private static readonly string saveFieldStateName = "SavedFieldState.json";

    private static Dictionary<string, string> InterfaceInstructions = new Dictionary<string, string>()
    {
      { "F1", "new random" },
      { "F2", "load pre-defined #1" },
      { "F3", "load pre-defined #2" },
      { "F5", "save state" },
      { "F6", "print state" },
      { "F9", "load saved state" },
      { "Space", "pause" },
      { "Right", "step to next gen" },
      { "1-2", "hold to slow down" },
      { "3-4", "hold to speed up" },
    };

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

      WriteInterfaceInstructions();

      Start();
    }

    private static void WriteInterfaceInstructions()
    {
      Console.WriteLine(" - Interface instructions -");
      foreach (var instruction in InterfaceInstructions)
      {
        Console.WriteLine($" {instruction.Key}:\t{instruction.Value}");
      }
      Console.WriteLine();
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

      // Pauses updating and steps to next generation
      HandleUserSteppingToNextGen();

      // Sets a new random game of life
      HandleUserSetsNewRandomGame();

      // Sets a new pre-defined game of life
      HandleUserSetsNewPreDefined1();
      HandleUserSetsNewPreDefined2();

      // Saves state of field to a file.
      HandleUserSavesFieldState();

      // Loads a state of a field into game.
      HandleUserLoadsFieldState();

      // Prints current state of field.
      HandleUserPrintsFieldState();

      // Sets update frequency.
      HandleUserSetsUpdateFrequency();
    }

    private void HandleUserSteppingToNextGen()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
      {
        update = false;

        gameOfLife.NextGeneration();

        Console.WriteLine($"Stepped to next generation {gameOfLife.GenerationsCount}.");
      }
    }

    private void HandleUserSavesFieldState()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F5) || Raylib.IsKeyPressed(KeyboardKey.KEY_S))
      {
        // Create a field file and save it
        var jsonFile = new JSONFile<Field> { FileName = saveFieldStateName, Data = (gameOfLife.Field as Field) };
        jsonFile.Save();
        Console.WriteLine("Saved state.");
      }
    }

    private void HandleUserLoadsFieldState()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F9) || Raylib.IsKeyPressed(KeyboardKey.KEY_L))
      {
        var jsonFile = new JSONFile<Field> { FileName = saveFieldStateName };
        jsonFile.Load();

        var field = jsonFile.Data;
        gameOfLife = new GameOfLife(field);

        // Set new view
        view.WindowSize = (WindowWidth, WindowHeight);

        Raylib.SetWindowSize(WindowWidth, WindowHeight);
        Console.WriteLine("Loaded saved state.");
      }
    }

    private void HandleUserPrintsFieldState()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F6) || Raylib.IsKeyPressed(KeyboardKey.KEY_P))
      {
        Console.WriteLine(gameOfLife.Field.ToString());
        Debug.WriteLine(gameOfLife.Field.ToString());
      }
    }

    private void HandleUserPausingOrResuming()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
      {
        update = !update;
        if (update) Console.WriteLine("Resumed.");
        else        Console.WriteLine("Paused.");
      }
    }

    private void HandleUserSetsNewRandomGame()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F1) || Raylib.IsKeyPressed(KeyboardKey.KEY_R))
      {
        gameOfLife = new GameOfLife();

        // Set new view
        view.WindowSize = (WindowWidth, WindowHeight);

        Raylib.SetWindowSize(WindowWidth, WindowHeight);
        Console.WriteLine("New randomized.");
      }
    }

    private void HandleUserSetsNewPreDefined1() // TODO load from file
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F2))
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

        // Set new view
        view.WindowSize = (WindowWidth, WindowHeight);

        Raylib.SetWindowSize(WindowWidth, WindowHeight);
        Console.WriteLine("Loaded pre-defined #1.");
      }
    }

    private void HandleUserSetsNewPreDefined2() // TODO load from file
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F3))
      {

        Console.WriteLine("Loaded pre-defined #2.");
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
