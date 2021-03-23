namespace GameOfLife_KSICS.Utils
{
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;

  public class FieldFile
  {
    private static string path { get; } = 
      Path.Combine(
        Path.Combine(
          Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments), 
          "GOL_KSICS\\"));

    public IField Field { get; private set; }

    public FieldFile(IField field)
    {
      Field = field;
    }

    public FieldFile(string path)
    {
      var fieldStr = File.ReadAllText(path);

      // Get rows and height
      var lines = fieldStr.Split("\n");
      var height = lines.Length;

      string[] cells = new string[0];
      var width = 0;

      // Get cells and width
      for (int y = 0; y < height; y++)
      {
        cells = lines[y].Split(" ");
        width = cells.Length - 1;
      }

      Field field = new Field(width, height);

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          int cellValue = Convert.ToInt32(cells[x]);

          switch (cellValue)
          {
            case 0:
              field.Cells[x, y].Alive = false;
              break;
            case 1:
              field.Cells[x, y].Alive = true;
              break;
            default:
              field.Cells[x, y].Alive = true;
              field.Cells[x, y].Age = cellValue - 1;
              break;
          }
        }
      }

      Field = field;
    }

    public bool Save()
    {
      var path = FieldFile.path;
      var now = DateTime.Now;

      path += $"{now.Year}-{now.Month}-{now.Day} {now.Hour}.{now.Minute}.{now.Second}.txt";

      File.WriteAllText(path, Field.ToString());

      return true;
    }
  }
}
