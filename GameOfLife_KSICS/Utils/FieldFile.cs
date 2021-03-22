namespace GameOfLife_KSICS.Utils
{
  using GameOfLife_KSICS.Interfaces;
  using GameOfLife_KSICS.Models;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;

  class FieldFile
  {
    private static string path { get; } = 
      Path.Combine(
        Path.Combine(
          Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments), 
          "GOL_KSICS"),
        "FieldFile.txt");

    public FieldFile(IField field)
    {
      throw new NotImplementedException();
    }

    public FieldFile(string path)
    {
      throw new NotImplementedException();
    }

    public bool Save(string path)
    {
      throw new NotImplementedException();
    }
  }
}
