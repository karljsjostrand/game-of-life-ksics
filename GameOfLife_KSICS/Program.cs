namespace GameOfLife_KSICS
{
  using GameOfLife_KSICS.Controllers.Raylib;
  using GameOfLife_KSICS.Models;
  using GameOfLife_KSICS.Utils.CellFormations;
  using GameOfLife_KSICS.Utils.Extensions;

  class Program
  {
    static void Main()
    {
      // Create a Game of Life where about a fifth of the cells are initially alive.
      var gameOfLife = new GameOfLife(200, 200, .2);

      // View and control the game
      new GameOfLifeController(gameOfLife);
    }
  }
}
