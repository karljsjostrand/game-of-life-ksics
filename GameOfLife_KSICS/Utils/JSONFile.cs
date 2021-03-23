namespace GameOfLife_KSICS.Utils
{
  using Newtonsoft.Json;
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.Text;
  using Interfaces;

  public class JSONFile<T>
  {
    public T Data { get; set; } = default;
    public string FilePath { get; set; }
      = Path.Combine(
          Path.Combine(
            Environment.GetFolderPath(
              Environment.SpecialFolder.MyDocuments),
            "GOL_KSICS\\"));

    public string FileName { get; set; } 
      = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.json";
    
    /// <summary>
    /// Save state of an object to at file at the given path.
    /// </summary>
    public void Save()
    {
      File.WriteAllText(
      FilePath + FileName,
      JsonConvert.SerializeObject(Data, Formatting.Indented,
      new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      })
      );
    }

    public void Load()
    {
      string data = string.Empty;
      if (File.Exists(FilePath + FileName))
      {
        try
        {
          data = File.ReadAllText(FilePath + FileName);
        }
        catch (Exception ex)
        {
          Debug.WriteLine(ex.Message);
          data = "{}";
        }
      }
      try
      {
        Data = JsonConvert.DeserializeObject<T>(data);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
      }
    }
  }
}
