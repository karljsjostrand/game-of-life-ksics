using NUnit.Framework;
using GameOfLife_KSICS.Utils;
using GameOfLife_KSICS.Models;
using System.IO;

namespace GameOfLife_KSICS.Utils.Tests
{
  [TestFixture()]
  public class JSONFileTests
  {
    static readonly int fieldWidth = 20;
    static readonly int fieldHeight = 20;
    Field field;
    (int x, int y) centeredCellPosition;

    /// <summary>
    /// Setup a new field and set named positions for the field.
    /// </summary>
    [SetUp()]
    public void Setup()
    {
      field = new Field(fieldWidth, fieldHeight);
      centeredCellPosition = (fieldWidth / 2, fieldHeight / 2);
    }

    /// <summary>
    /// Should create a saved field state file.
    /// Save test file is deleted at the end of the test.
    /// </summary>
    [Test()]
    public void SaveTest()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x, y].Alive = true;
      var fileName = "testFieldState";

      var jSONFile = new JSONFile<Field> { FileName = fileName, Data = field };
      var fullPath = jSONFile.DirPath + fileName + jSONFile.FileExtension;

      FileAssert.DoesNotExist(fullPath);

      Assert.IsTrue(jSONFile.Save(), "Did not save.");

      FileAssert.Exists(fullPath);

      File.Delete(fullPath);
    }

    /// <summary>
    /// Should create a saved a field state file and then load the file. The 
    /// field state loaded should then be asserted equal to the one saved.
    /// Save test file is deleted at the end of the test.
    /// </summary>
    [Test()]
    public void LoadTest()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x, y].Alive = true;
      var fileName = "testFieldState";

      var jSONSaveFileExpected = new JSONFile<Field> { FileName = fileName, Data = field };
      var fullPath = jSONSaveFileExpected.DirPath + fileName + jSONSaveFileExpected.FileExtension;

      Assert.IsTrue(jSONSaveFileExpected.Save(), "Did not save.");

      var jSONLoadFileActual = new JSONFile<Field> { FileName = fileName };
      Assert.IsTrue(jSONLoadFileActual.Load(), "Did not load.");

      var expected = jSONSaveFileExpected.Data.ToString();
      var actual = jSONLoadFileActual.Data.ToString();

      Assert.AreEqual(expected, actual);

      File.Delete(fullPath);
    }
  }
}