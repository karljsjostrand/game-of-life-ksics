namespace GameOfLife_KSICS.Utils.FieldStates
{
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils.CellFormations;
  using System;
  using System.Collections.Generic;
  using System.Text;
  using Utils.Extensions;

  class FieldStates
  {
    public static (IField Field, string Description) GliderCrashesIntoBeacon()
    {
      var field = new Field(48, 12);

      field.AddCellFormation(new Glider(), (0, 0));
      field.AddCellFormation(new Beacon(), (32, 4));

      return (field, "Glider crashes into beacon.");
    }

    public static (IField Field, string Description) RandomizedChanceField()
    {
      var field = new ChanceField(48, 48);

      var rnd = new Random();

      foreach (var cell in field.Cells)
      {
        cell.Alive = rnd.NextDouble() >= .5;
      }

      return (field, "Randomzied field with a chance for new life with older neighbours.");
    }

    public static (IField Field, string Description) AgeLimitedField()
    {
      var field = new AgeLimitedField(48, 48);

      field.AddCellFormation(new Glider(), (24, 1));
      field.AddCellFormation(new Twins(), (24, 24));
      field.AddCellFormation(new Beacon(), (1, 1));


      return (field, $"Field with cells that die after an age of {field.AgeLimit}.");
    }
  }
}
