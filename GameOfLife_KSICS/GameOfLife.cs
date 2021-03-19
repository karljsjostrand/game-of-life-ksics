namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class GameOfLife
  {
    public Field Field { get; private set; }

    /// <summary>
    /// Create an instance of GameOfLife with a starting field of randomized width, height, and cells. 
    /// </summary>
    public GameOfLife()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Create an instance of GameOfLife with a field of given width and height and randomized cells.
    /// </summary>
    /// <param name="width">Width of starting field in number of cells.</param>
    /// <param name="height">Height of starting field in number of cells.</param>
    public GameOfLife(int width, int height)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Create an instance of GameOfLife with given starting field.
    /// </summary>
    /// <param name="field">Starting field.</param>
    public GameOfLife(Field field)
    {
      Field = field;
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

      var fileContents = "";

      // Create a string representation of the fields alive states.
      for (int y = 0; y < Field.Height; y++)
      {
        for (int x = 0; x < Field.Width; x++)
        {
          fileContents += Convert.ToInt32(Field.Cells[x, y].Alive);
        }
        fileContents += "\n";
      }

      // TODO
    }

    /// <summary>
    /// Progress to next generation. 
    /// 
    /// 0111
    /// 0000
    /// 
    /// becomes
    /// 
    /// 0010
    /// 0010
    /// </summary>
    public void NextGeneration()
    {
      Field.NextCells = Field.InitializeCells(Field.NextCells, Field.Width, Field.Height);

      for (int y = 0; y < Field.Height; y++)
      {
        for (int x = 0; x < Field.Width; x++)
        {
          var neighbourCount = Field.GetNeighbourCount(x, y);
          Field.NextCells[x, y] = Field.UpdateCell(x, y, neighbourCount);
        }
      }

      Field.Cells = Field.NextCells;
    }

    /// <summary>
    /// Progress until all cells are dead (or only 2 states remain?).
    /// </summary>
    public void Run()
    {
      throw new NotImplementedException();
    }
  }
}
