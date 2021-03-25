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
      // Create a Game of Life where about a tenth of the cells are initially alive.
      var gol = new GameOfLife(500, 200, .1);

      // View and control the game
      var controller = new GameOfLifeController(gol);
    }
  }
}
