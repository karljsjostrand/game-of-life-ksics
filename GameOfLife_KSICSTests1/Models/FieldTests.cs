using NUnit.Framework;
using GameOfLife_KSICS.Models;
using System;

namespace GameOfLife_KSICS.Models.Tests
{
  [TestFixture()]
  public class FieldTests
  {
    Field _field;
    (int, int) _centeredCellPosition;
    (int, int) _edgeCellPosition;
    (int, int) _topLeftCornerCellPosition;
    (int, int) _topRightCornerCellPosition;
    (int, int) _btmLeftCornerCellPosition;
    (int, int) _btmRightCornerCellPosition;

    [SetUp()]
    public void Setup()
    {
      var fieldWidth = 80;
      var fieldHeight = 40;

      _field = new Field(fieldWidth, fieldHeight);

      _centeredCellPosition = (fieldWidth / 2, fieldHeight / 2);
      //_edgeCellPosition = (fieldWidth - 1, fieldHeight - 1); // TODO: remove
      _topLeftCornerCellPosition = (0, 0);
      _topRightCornerCellPosition = (fieldWidth - 1, 0);
      _btmLeftCornerCellPosition = (0, fieldHeight - 1);
      _btmRightCornerCellPosition = (fieldWidth - 1, fieldHeight - 1);
    }

    /// <summary>
    /// Should return 0 when no neighbour alive. 
    /// </summary>
    [Test()]
    public void GetNeighourCountTest_ShouldReturnZero_WhenNoNeighourAlive()
    {
      var x = _centeredCellPosition.Item1;
      var y = _centeredCellPosition.Item2;
      var expected = 0;
      var actual = _field.GetNeighourCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    [TestCase(40, 19)] // above neighbour, 12 o clock
    [TestCase(41, 19)] // 13:30
    [TestCase(41, 20)] // right, 15
    [TestCase(41, 21)] // 16:30
    [TestCase(40, 21)] // below, 18
    [TestCase(39, 21)] // 19:30
    [TestCase(39, 20)] // left, 21
    [TestCase(39, 19)] // 22:30
    public void GetNeighourCountTest_ShouldReturnOne_WhenOneNeighbourAlive(int neighbourX, int neighbourY)
    {
      var x = _centeredCellPosition.Item1;
      var y = _centeredCellPosition.Item2;

      _field.Cells[neighbourX, neighbourY].Alive = true;

      var expected = 1;
      var actual = _field.GetNeighourCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighourCountTest_ShouldReturnThree_WhenThreeNeighboursAlive()
    {
      var x = _centeredCellPosition.Item1;
      var y = _centeredCellPosition.Item2;

      _field.Cells[41, 19].Alive = true; // 13:30
      _field.Cells[41, 20].Alive = true; // 15
      _field.Cells[41, 21].Alive = true; // 16:30

      var expected = 3;
      var actual = _field.GetNeighourCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighourCountTest_ShouldIgnoreNeighbour_WhenNeighbourIsOutOfBounds()
    {
      var position = _topRightCornerCellPosition;

      var x = position.Item1;
      var y = position.Item2;

      _field.GetNeighourCount(x, y);
    }
  }
}