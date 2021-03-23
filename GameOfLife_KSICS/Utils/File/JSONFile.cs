namespace GameOfLife_KSICS.Utils
{
  using Newtonsoft.Json;
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.Text;
  using Interfaces;

  /// <summary>
  /// Saves and loads the state of an object.
  /// </summary>
  /// <typeparam name="T">Type of class to save or load.</typeparam>
  public class JSONFile<T>
  {
    public T Data { get; set; } = default;

    /// <summary>
    /// Path to folder where saved states are stored.
    /// </summary>
    public string FilePath { get; set; }
      = Path.Combine(
          Path.Combine(
            Environment.GetFolderPath(
              Environment.SpecialFolder.MyDocuments),
            "GOL_KSICS\\"));

    /// <summary>
    /// Name of file and filename extension.
    /// </summary>
    public string FileName { get; set; } 
      = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}";

    /// <summary>
    /// The file extension part of the full path.
    /// </summary>
    public string FileExtension { get; } = ".json";

    /// <summary>
    /// Save state of an object to a file at the specified path. 
    /// </summary>
    public void Save()
    {
      File.WriteAllText(
      FilePath + FileName + FileExtension,
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

    /// <summary>
    /// Load state of an object from specified path.
    /// </summary>
    public void Load()
    {
      string data = string.Empty;
      if (File.Exists(FilePath + FileName + FileExtension))
      {
        try
        {
          data = File.ReadAllText(FilePath + FileName + FileExtension);
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
