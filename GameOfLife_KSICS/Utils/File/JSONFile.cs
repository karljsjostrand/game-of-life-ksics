namespace GameOfLife_KSICS.Utils
{
  using Newtonsoft.Json;
  using System;
  using System.Diagnostics;
  using System.IO;

  /// <summary>
  /// Saves and loads the state of an object.
  /// </summary>
  /// <typeparam name="T">Type of class to save or load.</typeparam>
  public class JSONFile<T>
  {
    public T Data { get; set; } = default;

    /// <summary>
    /// Path to directory where saved states are stored.
    /// </summary>
    public string DirPath { get; set; }
      = Path.Combine(
          Path.Combine(
            Environment.GetFolderPath(
              Environment.SpecialFolder.MyDocuments),
            "GOL_KSICS\\"));

    /// <summary>
    /// Name of file, file extension excluded.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// The file extension part of the full path.
    /// </summary>
    public string FileExtension { get; } = ".json";

    /// <summary>
    /// DirPath + FileName + FileExtension
    /// </summary>
    public string FullPath => DirPath + FileName + FileExtension;

    /// <summary>
    /// Save state of an object to a file at the specified path. 
    /// </summary>
    /// <returns>true if saved successfully; otherwise  false</returns>
    public bool Save()
    {
      try
      {
        Directory.CreateDirectory(DirPath);

        File.WriteAllText(
          FullPath,
          JsonConvert.SerializeObject(
            Data, 
            Formatting.Indented,
            new JsonSerializerSettings
            {
              Formatting = Formatting.Indented,
              NullValueHandling = NullValueHandling.Ignore,
              DefaultValueHandling = DefaultValueHandling.Ignore,
              ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            })
          );
      }
      catch (Exception e)
      {
        Debug.WriteLine(e.Message);
        return false;
      }

      return true;
    }

    /// <summary>
    /// Load state of an object from specified path.
    /// </summary>
    /// <returns>true if loaded successfully; otherwise  false</returns>
    public bool Load()
    {
      string data = string.Empty;
      if (File.Exists(FullPath))
      {
        try
        {
          data = File.ReadAllText(FullPath);
        }
        catch (Exception e)
        {
          Debug.WriteLine(e.Message);
          return false;
        }
      }
      else
      {
        // File not found.
        return false;
      }

      try
      {
        Data = JsonConvert.DeserializeObject<T>
        (
          data
        );
      }
      catch (Exception e)
      {
        // Could not deserialize.
        Debug.WriteLine(e.Message);
        return false;
      }

      // File loaded successfully.
      return true;
    }
  }
}
