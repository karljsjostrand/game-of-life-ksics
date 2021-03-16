namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;

  class GameOfLife
  {
    /// <summary>
    /// Create an instance of GameOfLife with a field of randomized width and height and randomized cells. 
    /// </summary>
    public GameOfLife()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Create an instance of GameOfLife with a field of given width and height and randomized cells.
    /// </summary>
    public GameOfLife(int width, int height)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Create an instance of GameOfLife with starting field provided as parameter.
    /// </summary>
    /// <param name="field"></param>
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
    /// Progresses to next generation. 
    /// </summary>
    public void NextGeneration()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Run the game until all cells are dead (or only states remain?).
    /// </summary>
    public void Run()
    {
      throw new NotImplementedException();
    }
  }
}
