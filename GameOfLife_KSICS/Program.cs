﻿namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Controllers.Raylib;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils.CellFormations;
  using GameOfLife_KSICS.Utils.Extensions;

  class Program
  {
    static void Main()
    {


      //GameOfLifeFromDefinedField();
      GameOfLifeFromRandomizedField();
    }

    static void GameOfLifeFromDefinedField()
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

      var gameOfLife = new GameOfLife(field);

      new GameOfLifeController(gameOfLife);
    }

    static void GameOfLifeFromRandomizedField()
    {
      var gameOfLife = new GameOfLife();

      new GameOfLifeController(gameOfLife);
    }
  }
}
