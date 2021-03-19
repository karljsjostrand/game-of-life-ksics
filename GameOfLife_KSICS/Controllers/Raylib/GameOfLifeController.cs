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
    private static int targetFps = 60;

    public GameOfLifeController(GameOfLife gameOfLife)
    {
      GameOfLife = gameOfLife;

      #region move this to its own class or to field?
      //// add line
      //GameOfLife.Field.Cells[5, 3].Alive = true;
      //GameOfLife.Field.Cells[6, 3].Alive = true;
      //GameOfLife.Field.Cells[7, 3].Alive = true;

      // add tub
      //GameOfLife.Field.Cells[74, 8].Alive = true;
      //GameOfLife.Field.Cells[75, 7].Alive = true;
      //GameOfLife.Field.Cells[76, 8].Alive = true;
      //GameOfLife.Field.Cells[75, 9].Alive = true;

      //// add box
      //GameOfLife.Field.Cells[45, 12].Alive = true;
      //GameOfLife.Field.Cells[46, 12].Alive = true;
      //GameOfLife.Field.Cells[47, 12].Alive = true;
      //GameOfLife.Field.Cells[47, 13].Alive = true;
      //GameOfLife.Field.Cells[47, 14].Alive = true;
      //GameOfLife.Field.Cells[46, 14].Alive = true;
      //GameOfLife.Field.Cells[45, 14].Alive = true;
      //GameOfLife.Field.Cells[45, 13].Alive = true;

      // add gliders
      //GameOfLife.Field.Cells[20, 18].Alive = true;
      //GameOfLife.Field.Cells[21, 19].Alive = true;
      //GameOfLife.Field.Cells[22, 17].Alive = true;
      //GameOfLife.Field.Cells[22, 18].Alive = true;
      //GameOfLife.Field.Cells[22, 19].Alive = true;
      var glider = new Glider();
      glider.AddToField(GameOfLife.Field, (0, 0));
      //glider.AddToField(GameOfLife.Field, (20, 32));
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

        GameOfLife.NextGeneration();

        if (Raylib.IsKeyReleased(KeyboardKey.KEY_S))
        {
          GameOfLife.SaveFieldToFile("???");
        }


        #region view
        Views.Raylib.In2D.FieldView.DrawIn2D(GameOfLife.Field, windowWidth, windowHeight);
        #endregion

        #endregion
      }
      Raylib.CloseWindow();
    }
  }
}
