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

    private static int windowWidth = 1600;
    private static int windowHeight = 800;
    private static int targetFps = 20;

    public GameOfLifeController(GameOfLife gameOfLife)
    {
      GameOfLife = gameOfLife;

      #region add some initial life to the field, TODO: move this to where field is created?
      //// add blinker
      (GameOfLife.Field as Field).AddCellFormation(new Blinker(), (0, 10));
      (GameOfLife.Field as Field).AddCellFormation(new Blinker(), (0, 20));
      (GameOfLife.Field as Field).AddCellFormation(new Blinker(), (0, 30));

      //// add gliders
      (GameOfLife.Field as Field).AddCellFormation(new Glider(), (0, 0));
      (GameOfLife.Field as Field).AddCellFormation(new Glider(), (10, 0));
      (GameOfLife.Field as Field).AddCellFormation(new Glider(), (20, 0));
      (GameOfLife.Field as Field).AddCellFormation(new Glider(), (30, 0));
      (GameOfLife.Field as Field).AddCellFormation(new Glider(), (40, 0));

      // add beehive
      (GameOfLife.Field as Field).AddCellFormation(new Beehive(), (50, 0));
      #endregion
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
