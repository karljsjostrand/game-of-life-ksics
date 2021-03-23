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
      //Field field = new Field(20, 20);
      //field.Cells[10, 10].Alive = true;

      //var fieldFile = new FieldFile(field);

      //fieldFile.Save();


      //GameOfLifeFromRandomizedField();
      //GameOfLifeFromDefinedField();

      //GameOfLifeFromChanceField();
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

    static void GameOfLifeFromChanceField()
    {
      var field = new ChanceField(100, 50);

      var rnd = new Random();

      foreach (var cell in field.Cells)
      {
        cell.Alive = rnd.NextDouble() >= .5;
      }

      var gameOfLife = new GameOfLife(field);

      new GameOfLifeController(gameOfLife);
    }
  }
}
