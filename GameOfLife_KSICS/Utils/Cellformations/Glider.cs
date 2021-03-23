namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Abstracts;
  using System.Collections.Generic;

  class Glider : CellFormation
  {
    public override (int Width, int Height) Size { get; } = (3, 3);

    public override void AddCells()
    {
      // coordinates for a glider formation
      CellPositions.AddRange(
        new List<(int x, int y)>()
        {
          (0, 1),
          (1, 2),
          (2, 0),
          (2, 1),
          (2, 2),
        });
    }
  }
}
