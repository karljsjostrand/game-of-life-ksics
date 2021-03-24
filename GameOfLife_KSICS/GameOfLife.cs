namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils;
  using System;

  public class GameOfLife
  {
    /// <summary>
    /// Holds most the games states, most importantly, the cells, 
    /// and the methods that dictates how they are changed on new 
    /// generations. 
    /// </summary>
    public IField Field { get; private set; }

    /// <summary>
    /// Current generation. 
    /// Starts at 0 and is increased by 1 on each next generation. 
    /// </summary>
    public int GenerationsCount { get; private set; } = 0;

    /// <summary>
    /// Create an instance with a field of randomized width, height, and 
    /// cells, with a 50/50 chance for each cell in the field to be 
    /// initialized alive. 
    /// </summary>
    /// <param name="minSize"></param>
    /// <param name="maxSize"></param>
    public GameOfLife(int minSize = 3, int maxSize = 100)
    {
      var rnd = new Random();

      var width = rnd.Next(minSize, maxSize);
      var height = rnd.Next(minSize, maxSize / 2);

      var field = new Field(width, height);

      foreach (var cell in field.Cells)
      {
        cell.Alive = rnd.NextDouble() >= .5;
      }

      Field = field;
    }

    /// <summary>
    /// Create an instance with a field of given width and height and 
    /// randomized cells, with the given chance for each cell in the 
    /// field to be initialized alive.
    /// </summary>
    /// <param name="width">Width of it's field in number of cells.</param>
    /// <param name="height">Height of it's field in number of cells.</param>
    /// <param name="chance">
    /// Chance for each in the field to be alive initially.
    /// </param>
    public GameOfLife(int width, int height, double chance)
    {
      var rnd = new Random();

      var field = new Field(width, height);

      foreach (var cell in field.Cells)
      {
        cell.Alive = rnd.NextDouble() >= chance;
      }

      Field = field;
    }

    /// <summary>
    /// Create an instance with a given field.
    /// </summary>
    /// <param name="field">Starting field.</param>
    public GameOfLife(IField field)
    {
      Field = field;
    }

    /// <summary>
    /// Create an instance with a field from a file at default saved states folder.
    /// </summary>
    /// <param name="fileName">File name, file extension excluded.</param>
    public GameOfLife(string fileName)
    {
      var jsonFile = new JSONFile<Field> { FileName = fileName };
      jsonFile.Load();

      Field = jsonFile.Data;
    }

    /// <summary>
    /// Step the state of the game to next generation. 
    /// </summary>
    public void NextGeneration()
    {
      Field.NextCells = Field.InitializedCells();

      for (int y = 0; y < Field.Height; y++)
      {
        for (int x = 0; x < Field.Width; x++)
        {
          var neighbourCount = Field.NeighboursCount(x, y);
          Field.NextCells[x, y] = Field.NextCell(x, y, neighbourCount);
        }
      }

      Field.Cells = Field.NextCells;
      GenerationsCount++;
    }
  }
}
