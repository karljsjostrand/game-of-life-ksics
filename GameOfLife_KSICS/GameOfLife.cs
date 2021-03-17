namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  public class GameOfLife
  {
    private Field Field { get; set; }

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
      throw new NotImplementedException();
    }

    /// <summary>
    /// Save state of field to a file at the given path.
    /// </summary>
    /// <param name="path">Desired location of file.</param>
    /// <returns>true if state is successfully saved, false otherwise.</returns>
    public bool SaveFieldToFile(string path)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Progress to next generation. 
    /// </summary>
    public void NextGeneration()
    {
      throw new NotImplementedException();
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
