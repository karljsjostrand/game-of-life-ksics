namespace GameOfLife_KSICS
{
  using System;
  using System.Collections.Generic;
  using System.Numerics;
  using GameOfLife_KSICS.Controllers.Raylib;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils.CellFormations;
  using Raylib_cs;
  using Utils;

  class Program
  {
    static void Main()
    {
      GameOfLifeFromRandomizedField();
      //GameOfLifeFromDefinedField();
    }

    static void GameOfLifeFromDefinedField()
    {
      var field = new Field(80, 40);

      #region add some initial living cells to field
      // add blinker
      field.AddCellFormation(new Blinker(), (0, 10));
      field.AddCellFormation(new Blinker(), (0, 20));
      field.AddCellFormation(new Blinker(), (0, 30));

      //// add gliders
      field.AddCellFormation(new Glider(), (0, 0));
      field.AddCellFormation(new Glider(), (10, 0));
      field.AddCellFormation(new Glider(), (20, 0));
      field.AddCellFormation(new Glider(), (30, 0));
      field.AddCellFormation(new Glider(), (40, 0));

      // add beehive
      field.AddCellFormation(new Beehive(), (50, 0));
      #endregion

      var gameOfLife = new GameOfLife(field);

      var golc = new GameOfLifeController(gameOfLife);
      golc.Start();
    }

    static void GameOfLifeFromRandomizedField()
    {
      var gameOfLife = new GameOfLife();

      var golc = new GameOfLifeController(gameOfLife);
      golc.Start();
    }
  }
}
