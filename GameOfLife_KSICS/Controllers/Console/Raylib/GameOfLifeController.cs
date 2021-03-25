namespace GameOfLife_KSICS.Controllers.Raylib
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils;
  using GameOfLife_KSICS.Utils.FieldStates;
  using Raylib_cs;

  class GameOfLifeController
  {
    private static readonly int defaultTargetFps = 10;
    private static readonly string title = "Game of Life";
    private static readonly string saveFieldStateName = "SavedFieldState";

    private static readonly List<(string input, string description)> userInterface = new List<(string, string)>()
    {
      { ("     F1/R", "new random") },
      { ("    F2-F4", "load pre-defined") },
      { ("     F5/S", "save state") },
      { ("     F6/P", "print state") },
      { ("     F9/L", "load saved state") },
      { ("    Space", "pause/resume") },
      { ("Tab/Right", "step to next gen") },
      { ("      1-2", "hold to slow down") },
      { ("      3-4", "hold to speed up") },
    };

    private readonly Dictionary<int, (IField field, string description)> predefinedFieldStates = new Dictionary<int, (IField, string)>()
    {
      { 1, FieldStates.GliderCrashesIntoBeacon() },
      { 2, FieldStates.RandomizedChanceField() },
      { 3, FieldStates.AgeLimitedField() },
    };

    private bool update = true;
    private int targetFps = defaultTargetFps;
    private int CellsToWindowSizeRatio = 12; // TODO: Window size width incorrect at 7 and lower with a 15x35 field.

    private GameOfLife GameOfLife { get; set; }
    private IView View { get; set; }

    private int WindowWidth => GameOfLife.Field.Width * CellsToWindowSizeRatio;
    private int WindowHeight => GameOfLife.Field.Height * CellsToWindowSizeRatio;
    
    /// <summary>
    /// Create a user interface and a view for a Game of Life.
    /// </summary>
    /// <param name="gameOfLife">Game of Life API.</param>
    public GameOfLifeController(GameOfLife gameOfLife)
    {
      Setup(gameOfLife);
      Start();
    }

    /// <summary>
    /// Create a user interface and a view for a Game of Life.
    /// </summary>
    /// <param name="gameOfLife">Game of Life API.</param>
    /// <param name="pixelsPerCell">Width and height of a drawn cell in number of pixels.</param>
    public GameOfLifeController(GameOfLife gameOfLife, int pixelsPerCell)
    {
      CellsToWindowSizeRatio = pixelsPerCell;
      Setup(gameOfLife);
      Start();
    }

    private void Setup(GameOfLife gameOfLife)
    {
      // Set API/Models
      GameOfLife = gameOfLife;

      // Set view
      View = new Views.Raylib.In2D.FieldView(WindowWidth, WindowHeight);
    }

    /// <summary>
    /// Write user interface instructions to the console.
    /// </summary>
    private static void WriteInterfaceInstructions()
    {
      Console.WriteLine(" - User Interface Key Bindings -\n");
      foreach (var (input, description) in userInterface)
      {
        Console.WriteLine($" {input}: {description}");
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

      // Initialize Raylib
      Raylib.SetTraceLogLevel(TraceLogType.LOG_WARNING);
      Raylib.SetTargetFPS(targetFps);
      Raylib.InitWindow(WindowWidth, WindowHeight, title);

      while (!Raylib.WindowShouldClose())
      {
        // View
        View.Draw(GameOfLife.Field);

        // User input
        HandleUserInput();

        // Update
        if (update) GameOfLife.NextGeneration();
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
      HandleUserLoadsPreDefined();

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
        Setup(
          new GameOfLife(
            jsonFile.Data
            ));

        // Resize window.
        Raylib.SetWindowSize(WindowWidth, WindowHeight);

        Console.WriteLine($"Loaded saved state from {jsonFile.DirPath + jsonFile.FileName + jsonFile.FileExtension}.");
      }
      else
      {
        Console.WriteLine($"Could not load state from {jsonFile.DirPath + jsonFile.FileName + jsonFile.FileExtension}.");
      }
    }

    /// <summary>
    /// Create a new game of life from a predefined state, set
    /// new view, and resize the window.
    /// </summary>
    /// <param name="id">Id for the predefined.</param>
    private void LoadPredefined(int id)
    {
      var predefined = predefinedFieldStates.GetValueOrDefault(id);

      if (predefined != default)
      {
        Setup(
          new GameOfLife(
            predefined.field
            ));

        // Resize window
        Raylib.SetWindowSize(WindowWidth, WindowHeight);

        Console.WriteLine($"Loaded predefined state #{id}. Description: {predefined.description}");
      }
      else
      {
        Console.WriteLine($"Could not find a predefined state #{id}.");
      }
    }

    #region User input handling
    private void HandleUserSteppingToNextGen()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
      {
        update = false;

        GameOfLife.NextGeneration();

        Console.WriteLine($"Stepped to generation {GameOfLife.GenerationsCount}.");
      }
    }

    private void HandleUserSavesState()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_F5) || Raylib.IsKeyPressed(KeyboardKey.KEY_S))
      {
        // Create a field file and save it
        var jsonFile = new JSONFile<Field> { FileName = saveFieldStateName, Data = (GameOfLife.Field as Field) };

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
        Console.Write(GameOfLife.Field.ToString());
        Debug.Write(GameOfLife.Field.ToString());
      }
    }

    private void HandleUserPausingOrResuming()
    {
      if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
      {
        // Toggle update flag.
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
        GameOfLife = new GameOfLife();

        // Set new view
        View.WindowSize = (WindowWidth, WindowHeight);

        Raylib.SetWindowSize(WindowWidth, WindowHeight);

        Console.WriteLine("New random.");
      }
    }

    private void HandleUserLoadsPreDefined()
    {
      switch ((KeyboardKey) Raylib.GetKeyPressed())
      {
        case KeyboardKey.KEY_F2:
          LoadPredefined(1);
          break;
        case KeyboardKey.KEY_F3:
          LoadPredefined(2);
          break;
        case KeyboardKey.KEY_F4:
          LoadPredefined(3);
          break;
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
