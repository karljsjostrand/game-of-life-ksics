namespace GameOfLife_KSICS.Utils.Extensions
{
  using GameOfLife_KSICS.Abstracts;
  using GameOfLife_KSICS.Interfaces;

  static class FieldExtensions
  {
    /// <summary>
    /// Add a cell formation to the field.
    /// </summary>
    /// <param name="field">Field to add the cell formation in.</param>
    /// <param name="cf">Cell formation to add.</param>
    /// <param name="pos">Cell formation is positioned with it's upper left corner here.</param>
    public static void AddCellFormation(this IField field, CellFormation cf, (int x, int y) pos)
    {
      foreach (var (cellX, cellY) in cf.CellPositions)
      {
        field.Cells[
          (pos.x + cellX + field.Width) % field.Width, 
          (pos.y + cellY + field.Height) % field.Height
          ].IsAlive = true;
      }
    }
  }
}
