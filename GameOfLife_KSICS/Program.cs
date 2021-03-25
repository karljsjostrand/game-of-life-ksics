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
      // Create a 110 by 80 Game of Life where about a third of the cells are initially alive.
      var gol = new GameOfLife(110, 80, .3);

      // Control and view the game
      var controller = new GameOfLifeController(gol);
    }
  }
}
