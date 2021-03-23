using NUnit.Framework;
using GameOfLife_KSICS.Utils;
using GameOfLife_KSICS.Models;

namespace GameOfLife_KSICS.Utils.Tests
{
  [TestFixture()]
  public class FieldFileTests
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

      var fieldFile = new FieldFile(field);

      Assert.IsTrue(fieldFile.Save());
    }

    [Test()]
    public void FieldFileTest()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      var path = @"C:\Users\KarlJS\Documents\GOL_KSICS\2021-3-23 10.19.3.txt";

      var fieldFile = new FieldFile(path);

      var cell = fieldFile.Field.Cells[x, y].Alive;

      Assert.IsNotNull(fieldFile.Field);
    }
  }
}