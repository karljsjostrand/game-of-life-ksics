namespace GameOfLife_KSICS.Controllers.Raylib
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils;
  using Raylib_cs;

  class GameOfLifeController
  {
    private static readonly int defaultTargetFps = 10;
    private static readonly int cellSizeInPixels = 16;
    private static readonly string title = "Game of Life";
    private static readonly string saveFieldStateName = "SavedFieldState";

    private static readonly Dictionary<string, string> UserInterface = new Dictionary<string, string>()
    {
      { "     F1/R", "new random" },
      { "       F2", "load pre-defined #1" },
      { "       F3", "load pre-defined #2" },
      { "     F5/S", "save state" },
      { "     F6/P", "print state" },
      { "     F9/L", "load saved state" },
      { "    Space", "pause/resume" },
      { "Tab/Right", "step to next gen" },
      { "      1-2", "hold to slow down" },
      { "      3-4", "hold to speed up" },
    };

    private bool update = true;
    private int targetFps = defaultTargetFps;

    private GameOfLife gameOfLife;
    private IView view;

    private int WindowWidth => gameOfLife.Field.Width * cellSizeInPixels;
    private int WindowHeight => gameOfLife.Field.Height * cellSizeInPixels;
    
    /// <summary>
    /// Create a user interface and a view for a Game of Life.
    /// </summary>
    /// <param name="gameOfLife">Game of Life API.</param>
    public GameOfLifeController(GameOfLife gameOfLife)
    {
      // Set API/Models
      this.gameOfLife = gameOfLife;

      // Set view
      view = new Views.Raylib.In2D.FieldView(WindowWidth, WindowHeight);

      Start();
    }

    /// <summary>
    /// Write user interface instructions to the console.
    /// </summary>
    private static void WriteInterfaceInstructions()
    {
      Console.WriteLine(" - User Interface Key Bindings -\n");
      foreach (var instruction in UserInterface)
      {
        Console.WriteLine($" {instruction.Key}: {instruction.Value}");
      }
      Console.WriteLine();
    }

    /// <summary>
    /// Start the controller - 
    /// present the view, handle user input, and update.
    /// </summary>
    private void Start()
    {
      WriteInterfaceInstructions();

      Raylib.SetTargetFPS(targetFps);
      Raylib.InitWindow(WindowWidth, WindowHeight, title);

      while (!Raylib.WindowShouldClose())
      {
        // View
        view.Draw(gameOfLife.Field);

        // User input
        HandleUserInput();

        // Update
        if (update) gameOfLife.NextGeneration();
      }
      Raylib.CloseWindow();
    }

    /// <summary>
    /// Check for any valid input made by the user.
    /// </summary>
    private void HandleUserInput()
    {
      // Pauses/resumes updating.
      HandleUserPausingOrResuming();

      // Pauses updating and steps to next generation
      HandleUserSteppingToNextGen();

      // New random
      HandleUserSetsNewRandom();

      // Pre-defined
      HandleUserLoadsPreDefined1();
      HandleUserLoadsPreDefined2();

      // Saves
      HandleUserSavesState();

      // Loads
      HandleUserLoadsState();

      // Prints
      HandleUserPrintsState();

      // Sets Update frequency.
      HandleUserSetsUpdateFrequency();
    }

    /// <summary>
    /// Create a new game of life with loaded state of a field from file, set
    /// new view, and resize the window.
    /// </summary>
    /// <param name="fileName">
    /// Name of the file to load saved field state from.
    /// </param>
    private void LoadFromFile(string fileName)
    {
      var jsonFile = new JSONFile<Field> { FileName = fileName };

      // Try to load file.
      if (jsonFile.Load())
      {
        var field = jsonFile.Data;
        gameOfLife = new GameOfLife(field);

        // Set new view
        view.WindowSize = (WindowWidth, WindowHeight);
        // Resize window
        Raylib.SetWindowSize(WindowWidth, WindowHeight);

        Console.WriteLine($"Loaded saved state from {jsonFile.DirPath + jsonFile.FileName + jsonFile.FileExtension}.");
      }
      else
      {
        Console.WriteLine($"Could not load state from {jsonFile.DirPath + jsonFile.FileName + jsonFile.FileExtension}.");
      }
    }

    #region User input handling
    private void HandleUserSteppingToNextGen()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
      {
        update = false;

        gameOfLife.NextGeneration();

        Console.WriteLine($"Stepped to generation {gameOfLife.GenerationsCount}.");
      }
    }

    private void HandleUserSavesState()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F5) || Raylib.IsKeyPressed(KeyboardKey.KEY_S))
      {
        // Create a field file and save it
        var jsonFile = new JSONFile<Field> { FileName = saveFieldStateName, Data = (gameOfLife.Field as Field) };

        if (jsonFile.Save())
        {
          Console.WriteLine($"Saved state to {jsonFile.DirPath + jsonFile.FileName + jsonFile.FileExtension}.");
        }
        else
        {
          Console.WriteLine($"Could not save state to {jsonFile.DirPath + jsonFile.FileName + jsonFile.FileExtension}.");
        }
      }
    }

    private void HandleUserLoadsState()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F9) || Raylib.IsKeyPressed(KeyboardKey.KEY_L))
      {
        LoadFromFile(saveFieldStateName);
      }
    }

    private void HandleUserPrintsState()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F6) || Raylib.IsKeyPressed(KeyboardKey.KEY_P))
      {
        Console.Write(gameOfLife.Field.ToString());
        Debug.Write(gameOfLife.Field.ToString());
      }
    }

    private void HandleUserPausingOrResuming()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
      {
        // Toggle to opposite.
        update = !update;

        if (update) 
          Console.WriteLine("Resumed.");
        else
          Console.WriteLine("Paused.");
      }
    }

    private void HandleUserSetsNewRandom()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F1) || Raylib.IsKeyPressed(KeyboardKey.KEY_R))
      {
        gameOfLife = new GameOfLife();

        // Set new view
        view.WindowSize = (WindowWidth, WindowHeight);

        Raylib.SetWindowSize(WindowWidth, WindowHeight);

        Console.WriteLine("New random.");
      }
    }

    private void HandleUserLoadsPreDefined1()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F2))
      {
        LoadFromFile("PreDefinedState1");
      }
    }

    private void HandleUserLoadsPreDefined2()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F3))
      {
        LoadFromFile("PreDefinedState2");
      }
    }

    private void HandleUserSetsUpdateFrequency()
    {
      // Holding down 1-4 key sets updating frequency.
      // 1-2 sets lower.
      // 3-4 sets higher.
      // Releasing the key returns it to default frequency. 

      // 1/10 of default speed.
      if (Raylib.IsKeyDown(KeyboardKey.KEY_ONE))
      {
        var newFps = defaultTargetFps / 10;
        if (targetFps != newFps)
        {
          targetFps = newFps;
          Raylib.SetTargetFPS(targetFps);
        }
      }
      // 1/2 of default speed.
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
    #endregion
  }
}
