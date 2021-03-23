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

    [SetUp()]
    public void Setup()
    {
      field = new Field(fieldWidth, fieldHeight);
      centeredCellPosition = (fieldWidth / 2, fieldHeight / 2);
    }

    [Test()]
    public void SaveTest()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x, y].Alive = true;
      var fileName = "test.json";

      var jSONFile = new JSONFile<Field> { FileName = fileName, Data = field };
      var fullPath = jSONFile.FilePath + fileName;

      File.Delete(fullPath);

      FileAssert.DoesNotExist(fullPath);

      jSONFile.Save();

      FileAssert.Exists(fullPath);

      File.Delete(fullPath);
    }

    [Test()]
    public void LoadTest()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x, y].Alive = true;
      var fileName = "test.json";

      var jSONSaveFileExpected = new JSONFile<Field> { FileName = fileName, Data = field };
      var fullPath = jSONSaveFileExpected.FilePath + fileName;

      jSONSaveFileExpected.Save();

      var jSONLoadFileActual = new JSONFile<Field> { FileName = fileName };
      jSONLoadFileActual.Load();

      var expected = jSONSaveFileExpected.Data.ToString();
      var actual = jSONLoadFileActual.Data.ToString();

      Assert.AreEqual(expected, actual);

      File.Delete(fullPath);
    }
  }
}