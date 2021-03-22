using NUnit.Framework;
using GameOfLife_KSICS.Models;
using System;
using System.Collections.Generic;

namespace GameOfLife_KSICS.Models.Tests
{
  [TestFixture()]
  public class FieldTests
  {
    static readonly int fieldWidth = 80;
    static readonly int fieldHeight = 40;
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
    /// Should return 0 when no neighbours alive. 
    /// </summary>
    [Test()]
    public void GetNeighoursCountTest_ShouldReturnZero_WhenNoNeighoursAlive()
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
    public void GetNeighoursCountTest_ShouldReturnOne_WhenOneNeighbourAlive(int neighbourX, int neighbourY)
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[neighbourX, neighbourY].Alive = true;

      var expected = 1;
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighoursCountTest_ShouldReturnTwo_WhenTwoNeighboursAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x + 1, y - 1].Alive = true; // 13:30
      field.Cells[x + 1, y].Alive = true; // 15

      var expected = 2;
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighoursCountTest_ShouldReturnThree_WhenThreeNeighboursAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x + 1, y - 1].Alive = true; // 13:30
      field.Cells[x + 1, y    ].Alive = true; // 15
      field.Cells[x + 1, y + 1].Alive = true; // 16:30

      var expected = 3;
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighoursCountTest_ShouldReturnFour_WhenFourNeighboursAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x + 1, y - 1].Alive = true; // 13:30
      field.Cells[x + 1, y].Alive = true; // 15
      field.Cells[x + 1, y + 1].Alive = true; // 16:30
      field.Cells[x, y + 1].Alive = true; // 18

      var expected = 4;
      var actual = field.GetNeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighoursCountTest_ShouldNotThrowException_WhenNeighbourIsOutOfBounds()
    {
      var edgePositions = new List<(int x, int y)>()
      {
        topLeftCornerCellPosition,
        topRightCornerCellPosition,
        btmLeftCornerCellPosition,
        btmRightCornerCellPosition,
      };

      try
      {
        foreach (var position in edgePositions)
        {
          var x = position.x;
          var y = position.y;
          field.GetNeighboursCount(x, y);
        }
      }
      catch (Exception e)
      {
        Assert.Fail($"Neighbour out of bounds? {e} was thrown.");
      }
    }

    // TODO: make it check other side of array neighbours
    //[Test()]
    //[TestCase(-1, -1)]
    //[TestCase(0, 41)]
    //[TestCase(fieldWidth, 41)]
    //[TestCase(fieldWidth, 0)]
    //public void GetNeighourCountTest_ShouldReturnZero_WhenNeighboursAreOutOfBounds(int x, int y)
    //{
    //  var expected = 0;
    //  var actual = field.GetNeighboursCount(x, y);

    //  Assert.AreEqual(expected, actual);
    //}

    [Test()]
    public void NextCellTest_ShouldBringAlive_WhenThreeNeighboursAndDeadInCurrentGen()
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.Cells[x, y].Alive = false;

      Assert.IsTrue(field.NextCell(x, y, 3).Alive, "Should be alive.");
    }

    [Test()]
    public void NextCellTest_ShouldKeepAlive_WhenTwoNeighboursAndAliveInCurrentGen()
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.Cells[x, y].Alive = true;

      Assert.IsTrue(field.NextCell(x, y, 2).Alive, "Should be alive.");
    }

    [Test()]
    public void NextCellTest_ShouldNotBringAlive_WhenTwoNeighboursAndDeadInCurrentGen()
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.Cells[x, y].Alive = false;

      Assert.IsFalse(field.NextCell(x, y, 2).Alive, "Should not be alive.");
    }

    [Test()]
    [TestCase(int.MinValue)]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void NextCellTest_ShouldSetToDead_WhenLessThanTwoNeighbours(int neighboursCount)
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      Assert.IsFalse(field.NextCell(x, y, neighboursCount).Alive);
    }

    [Test()]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(int.MaxValue)]
    public void NextCellTest_ShouldSetToDead_WhenMoreThanThreeNeighbours(int neighboursCount)
    {
      var position = centeredCellPosition;
      var x = position.x;
      var y = position.y;

      field.NextCell(x, y, neighboursCount);

      Assert.IsFalse(field.NextCell(x, y, neighboursCount).Alive);
    }
  }
}