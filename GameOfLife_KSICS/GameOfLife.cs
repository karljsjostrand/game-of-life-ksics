namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class GameOfLife
  {
    public IField Field { get; private set; }
    public int GenerationsCount { get; private set; } = 0;

    /// <summary>
    /// Create an instance with a field of randomized width, height, and initial alive states. 
    /// </summary>
    public GameOfLife()
    {
      var rnd = new Random();

      var minSize = 20;
      var maxSize = 100;

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
    /// Create an instance with a field of given width and height and randomized cells.
    /// </summary>
    /// <param name="width">Width of it's field in number of cells.</param>
    /// <param name="height">Height of it's field in number of cells.</param>
    public GameOfLife(int width, int height)
    {
      var rnd = new Random();

      var field = new Field(width, height);

      foreach (var cell in field.Cells)
      {
        cell.Alive = rnd.NextDouble() >= .5;
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
    /// Create an instance with a given field from a file.
    /// </summary>
    /// <param name="path">Path to and including file name.</param>
    public GameOfLife(string path)
    {
      throw new NotImplementedException();

      // TODO: load field from file - unpack string into a Field object
    }

    /// <summary>
    /// Save state of field to a file at the given path.
    /// </summary>
    /// <param name="path">Desired location of file.</param>
    /// <returns>true if state is successfully saved, false otherwise.</returns>
    public bool SaveFieldToFile(string path)
    {
      throw new NotImplementedException();

      var fileName = DateTime.Now + ".txt";

      var fileContents = Field.ToString();

      // TODO: save string to .txt file?
    }

    /// <summary>
    /// Step field state to next generation. 
    /// </summary>
    public void NextGeneration()
    {
      Field.NextCells = Field.InitializeCells(Field.NextCells, Field.Width, Field.Height);

      for (int y = 0; y < Field.Height; y++)
      {
        for (int x = 0; x < Field.Width; x++)
        {
          var neighbourCount = Field.GetNeighboursCount(x, y);
          Field.NextCells[x, y] = Field.NextCell(x, y, neighbourCount);
        }
      }

      Field.Cells = Field.NextCells;
      GenerationsCount++;
    }
  }
}
