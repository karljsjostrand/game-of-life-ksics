namespace GameOfLife_KSICS.Utils.CellFormations
{
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.Text;
  using Utils;

  class Glider : ICellFormation
  {
    public (int Width, int Height) Size { get; } = (3, 3);

    /// <summary>
    /// Add a Glider cell formation.
    /// </summary>
    /// <param name="field">Field to add the Glider in.</param>
    /// <param name="pos">Top left position of cell formation.</param>
    public void AddToField(Field field, (int x, int y) pos)
    {
      // leftmost
      field.Cells[pos.x, pos.y.Down()].Alive = true;
      // second from the left
      field.Cells[pos.x.Right(), pos.y.Down(2)].Alive = true; 
      // top in third column from left
      field.Cells[pos.x.Right(2), pos.y].Alive = true;
      // center in third column from left
      field.Cells[pos.x.Right(2), pos.y.Down()].Alive = true;
      // bottom in third column from left
      field.Cells[pos.x.Right(2), pos.y.Down(2)].Alive = true;
    }
  }
}
