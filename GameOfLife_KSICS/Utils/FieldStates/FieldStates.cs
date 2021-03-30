namespace GameOfLife_KSICS.Utils.FieldStates
{
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils.CellFormations;
  using System;
  using Utils.Extensions;

  static class FieldStates
  {
    /// <summary>
    /// Creates a field with a beacon and a glider formation on collision course.
    /// </summary>
    /// <returns>Field and description.</returns>
    public static (IField Field, string Description) GliderCrashesIntoBeacon()
    {
      var field = new Field(48, 12);

      field.AddCellFormation(new Glider(), (0, 0));
      field.AddCellFormation(new Beacon(), (32, 4));

      return (field, "Glider crashes into beacon.");
    }

    /// <summary>
    /// Creates a randomized field with a chance for new life in cells with old enough neighbours.
    /// </summary>
    /// <returns>Field and description.</returns>
    public static (IField Field, string Description) RandomizedChanceField()
    {
      var field = new LifeUhFindsAWayField(48, 48);

      var rnd = new Random();

      foreach (var cell in field.Cells)
      {
        cell.IsAlive = rnd.NextDouble() >= .5;
      }

      return (field, $"Randomized field with a chance for new life at a high enough total age ({field.HighTotalAge}) of neighbouring cells.");
    }

    /// <summary>
    /// Creates a field with a glider, a twins, and a beacon formation in a field where age limits a cells life span.
    /// </summary>
    /// <returns>Field and description.</returns>
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
