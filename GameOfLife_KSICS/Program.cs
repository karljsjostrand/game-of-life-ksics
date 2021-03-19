namespace GameOfLife_KSICS
{
  using System;
  using System.Collections.Generic;
  using System.Numerics;
  using GameOfLife_KSICS.Controllers.Raylib;
  using GameOfLife_KSICS.Models;
  using Raylib_cs;
  using Utils;

  class Program
  {
    static void Main()
    {
      //SaveFieldToFileExperiment();

      GameOfLifeFromField();
      //BiasedRandom.WriteBiasedRandomNumbers(10);
    }

    private static void SaveFieldToFileExperiment()
    {
      var g = new GameOfLife(new Field(80, 40));

      g.Field.Cells[1, 1].Alive = true;

      var fileContents = "";

      for (int y = 0; y < g.Field.Height; y++)
      {
        for (int x = 0; x < g.Field.Width; x++)
        {
          fileContents += Convert.ToInt32(g.Field.Cells[x, y].Alive);
          //g.Field.Cells[x, y].X + "," + g.Field.Cells[x, y].Y + " "
        }
        fileContents += "\n";
      }
      Console.WriteLine(fileContents);
    }

    static void GameOfLifeFromField()
    {
      var field = new Field(80, 40);

      var gameOfLife = new GameOfLife(field);

      var golc = new GameOfLifeController(gameOfLife);
      golc.Start();
    }
  }
}
