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
      foreach (var cell in cf.CellPositions)
      {
        field.Cells[
          (pos.x + cell.x + field.Width) % field.Width, 
          (pos.y + cell.y + field.Height) % field.Height
          ].Alive = true;
      }
    }
  }
}
