using NUnit.Framework;
using GameOfLife_KSICS.Models;
using System;
using System.Collections.Generic;

namespace GameOfLife_KSICS.Models.Tests
{
  [TestFixture()]
  public class FieldTests
  {
    int _fieldWidth => 80;
    static int _fieldHeight = 40;
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
      _field = new Field(_fieldWidth, _fieldHeight);

      _centeredCellPosition = (_fieldWidth / 2, _fieldHeight / 2);
      //_edgeCellPosition = (fieldWidth - 1, fieldHeight - 1); // TODO: remove
      _topLeftCornerCellPosition = (0, 0);
      _topRightCornerCellPosition = (_fieldWidth - 1, 0);
      _btmLeftCornerCellPosition = (0, _fieldHeight - 1);
      _btmRightCornerCellPosition = (_fieldWidth - 1, _fieldHeight - 1);
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
      var edgePositions = new List<(int, int)>()
      {
        _topLeftCornerCellPosition,
        _topRightCornerCellPosition,
        _btmLeftCornerCellPosition,
        _btmRightCornerCellPosition,
      };

      foreach (var position in edgePositions)
      {
        var x = position.Item1;
        var y = position.Item2;
        _field.GetNeighourCount(x, y);
      }
    }

    [Test()]
    [TestCase(-1, -1)]
    [TestCase(0, 41)]
    [TestCase(81, 41)]
    [TestCase(81, 0)]
    public void GetNeighourCountTest_ShouldReturnZero_WhenOutOfBoundsPositions(int x, int y)
    {
      var expected = 0;
      var actual = _field.GetNeighourCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void UpdateCellTest_ShouldSetToAlive_WhenThreeNeighbours()
    {
      var position = _centeredCellPosition;

      //var neighbour1 = (position.Item1 + 1, position.Item2); // to the right
      //var neighbour2 = (position.Item1, position.Item2 + 1); // below
      //var neighbour3 = (position.Item1, position.Item2 - 1); // above

      _field.UpdateCell(_centeredCellPosition.Item1, _centeredCellPosition.Item2, 3);
    }
  }
}