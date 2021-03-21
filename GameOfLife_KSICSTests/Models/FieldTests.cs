using NUnit.Framework;
using GameOfLife_KSICS.Models;
using System;
using System.Collections.Generic;

namespace GameOfLife_KSICS.Models.Tests
{
  [TestFixture()]
  public class FieldTests
  {
    int fieldWidth = 80;
    int fieldHeight = 40;
    Field field;
    (int x, int y) centeredCellPosition;
    (int x, int y) topLeftCornerCellPosition;
    (int x, int y) topRightCornerCellPosition;
    (int x, int y) btmLeftCornerCellPosition;
    (int x, int y) btmRightCornerCellPosition;

    [SetUp()]
    public void Setup()
    {
      field = new Field(fieldWidth, fieldHeight);

      centeredCellPosition = (fieldWidth / 2, fieldHeight / 2);
      topLeftCornerCellPosition = (0, 0);
      topRightCornerCellPosition = (fieldWidth - 1, 0);
      btmLeftCornerCellPosition = (0, fieldHeight - 1);
      btmRightCornerCellPosition = (fieldWidth - 1, fieldHeight - 1);
    }

    /// <summary>
    /// Should return 0 when no neighbour alive. 
    /// </summary>
    [Test()]
    public void GetNeighourCountTest_ShouldReturnZero_WhenNoNeighourAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;
      var expected = 0;
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    [TestCase(40, 19)] // the above neighbour, 12 o clock
    [TestCase(41, 19)] // 13:30
    [TestCase(41, 20)] // right, 15
    [TestCase(41, 21)] // 16:30
    [TestCase(40, 21)] // below, 18
    [TestCase(39, 21)] // 19:30
    [TestCase(39, 20)] // left, 21
    [TestCase(39, 19)] // 22:30
    public void GetNeighourCountTest_ShouldReturnOne_WhenOneNeighbourAlive(int neighbourX, int neighbourY)
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[neighbourX, neighbourY].Alive = true;

      var expected = 1;
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighourCountTest_ShouldReturnThree_WhenThreeNeighboursAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[41, 19].Alive = true; // 13:30
      field.Cells[41, 20].Alive = true; // 15
      field.Cells[41, 21].Alive = true; // 16:30

      var expected = 3;
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighourCountTest_ShouldIgnoreNeighbour_WhenNeighbourIsOutOfBounds()
    {
      var edgePositions = new List<(int x, int y)>()
      {
        topLeftCornerCellPosition,
        topRightCornerCellPosition,
        btmLeftCornerCellPosition,
        btmRightCornerCellPosition,
      };

      foreach (var position in edgePositions)
      {
        var x = position.x;
        var y = position.y;
        field.GetNeighboursCount(x, y);
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
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void UpdateCellTest_ShouldSetToAlive_WhenThreeNeighbours()
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.NextCell(x, y, 3);

      Assert.IsTrue(field.Cells[x, y].Alive);
    }

    [Test()]
    public void UpdateCellTest_ShouldSetToAlive_WhenTwoNeighbours()
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.NextCell(x, y, 2);

      Assert.IsTrue(field.Cells[x, y].Alive);
    }

    [Test()]
    [TestCase(0)]
    [TestCase(1)]
    public void UpdateCellTest_ShouldSetToDead_WhenLessThanTwoNeighbours(int neighboursCount)
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.NextCell(x, y, neighboursCount);

      Assert.IsFalse(field.Cells[x, y].Alive);
    }

    [Test()]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(int.MaxValue)]
    public void UpdateCellTest_ShouldSetToDead_WhenMoreThanThreeNeighbours(int neighboursCount)
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.NextCell(x, y, neighboursCount);

      Assert.IsFalse(field.Cells[x, y].Alive);
    }
  }
}