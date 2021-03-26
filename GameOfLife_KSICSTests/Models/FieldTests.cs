using NUnit.Framework;
using GameOfLife_KSICS.Models;
using System;
using System.Collections.Generic;

namespace GameOfLife_KSICS.Models.Tests
{
  [TestFixture()]
  public class FieldTests
  {
    // Let's not change these field dimensions, as some test case arguments depend on these values. 
    static readonly int fieldWidth = 80;
    static readonly int fieldHeight = 40;

    Field field;
    (int x, int y) centeredCellPosition;
    (int x, int y) topLeftCornerCellPosition;
    (int x, int y) topRightCornerCellPosition;
    (int x, int y) btmLeftCornerCellPosition;
    (int x, int y) btmRightCornerCellPosition;

    /// <summary>
    /// Setup a new field and set named positions for the field.
    /// </summary>
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
    /// Should count 0 neighbours for every position on the field when no cells alive. 
    /// </summary>
    [Test()]
    public void NeighoursCountTest_ShouldReturnZero_WhenNoNeighoursAlive()
    {
      for (int y = 0; y < field.Height; y++)
      {
        for (int x = 0; x < field.Width; x++)
        {
          var expected = 0;
          var actual = field.NeighboursCount(x, y);

          Assert.AreEqual(expected, actual);
        }
      }
    }

    /// <summary>
    /// Should count 1 alive neighbour. 
    /// </summary>
    /// <param name="neighbourX">Horizontal position of the neighbour.</param>
    /// <param name="neighbourY">Vertical position of the neighbour.</param>
    [Test()]
    [TestCase(40, 19)] // the above neighbour, 12 o clock
    [TestCase(41, 19)] // 13:30
    [TestCase(41, 20)] // right, 15
    [TestCase(41, 21)] // 16:30
    [TestCase(40, 21)] // below, 18
    [TestCase(39, 21)] // 19:30
    [TestCase(39, 20)] // left, 21
    [TestCase(39, 19)] // 22:30
    public void NeighoursCountTest_ShouldReturnOne_WhenOneNeighbourAlive(int neighbourX, int neighbourY)
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[neighbourX, neighbourY].IsAlive = true;

      var expected = 1;
      var actual = field.NeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Should count 2 alive neighbours. 
    /// </summary>
    [Test()]
    public void NeighoursCountTest_ShouldReturnTwo_WhenTwoNeighboursAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x + 1, y - 1].IsAlive = true; // 13:30
      field.Cells[x + 1, y].IsAlive = true; // 15

      var expected = 2;
      var actual = field.NeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Should count 3 alive neighbours. 
    /// </summary>
    [Test()]
    public void NeighoursCountTest_ShouldReturnThree_WhenThreeNeighboursAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x + 1, y - 1].IsAlive = true; // 13:30
      field.Cells[x + 1, y    ].IsAlive = true; // 15
      field.Cells[x + 1, y + 1].IsAlive = true; // 16:30

      var expected = 3;
      var actual = field.NeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Should count 4 alive neighbours. 
    /// </summary>
    [Test()]
    public void NeighoursCountTest_ShouldReturnFour_WhenFourNeighboursAlive()
    {
      var x = centeredCellPosition.x;
      var y = centeredCellPosition.y;

      field.Cells[x + 1, y - 1].IsAlive = true; // 13:30
      field.Cells[x + 1, y].IsAlive = true; // 15
      field.Cells[x + 1, y + 1].IsAlive = true; // 16:30
      field.Cells[x, y + 1].IsAlive = true; // 18

      var expected = 4;
      var actual = field.NeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Should not throw exception when counting neighbours from an edge position.
    /// </summary>
    [Test()]
    public void NeighoursCountTest_ShouldNotThrowException_WhenGettingNeighboursCountFromEdgePosition()
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
        foreach (var (x, y) in edgePositions)
        {
          Assert.AreEqual(0, field.NeighboursCount(x, y), "Did not return 0, is the field not empty?");
        }
      }
      catch (Exception e)
      {
        Assert.Fail(e.Message);
      }
    }

    /// <summary>
    /// Should count neighbour on other side from an edge position on the x-axis.
    /// </summary>
    /// <param name="x">Horizontal position on the field.</param>
    [Test()]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(79)]
    public void NeighourCountTest_ShouldReturnOne_WhenOneNeighbourAliveOnOtherSideInXAxis(int x)
    {
      var y = 0;

      // Set neighbour alive on other side in the x-axis
      (int x, int y) neighbourPos = (x, field.Height - 1);
      field.Cells[neighbourPos.x, neighbourPos.y].IsAlive = true;

      var expected = 1;
      var actual = field.NeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Should count neighbour on other side from an edge position on the y-axis.
    /// </summary>
    /// <param name="y">Vertical position on the field.</param>
    [Test()]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(39)]
    public void NeighourCountTest_ShouldReturnOne_WhenOneNeighbourAliveOnOtherSideInYAxis(int y)
    {
      var x = 0;
      // Set neighbour alive on other side in the y-axis
      (int x, int y) neighbourPos = (field.Width - 1, y);
      field.Cells[neighbourPos.x, neighbourPos.y].IsAlive = true;

      var expected = 1;
      var actual = field.NeighboursCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Should get a living cell for a position with a dead cell that has 3 living neighbouring cells.
    /// </summary>
    [Test()]
    public void NextCellTest_ShouldBringAlive_WhenThreeNeighboursAndDeadInCurrentGen()
    {
      var pos = centeredCellPosition;
      var x = pos.x;
      var y = pos.y;

      // 3 neighbours pos
      var neighboursPos = new List<(int x, int y)>() 
      {
        { (pos.x - 1, pos.y - 1) },
        { (pos.x - 1, pos.y) },
        { (pos.x, pos.y - 1) },
      };

      // Set neighbours alive
      foreach (var (neighbourX, neighbourY) in neighboursPos)
      {
        field.Cells[neighbourX, neighbourY].IsAlive = true;
      }

      // Set position is currently dead
      field.Cells[x, y].IsAlive = false;

      // Assert next cell is alive
      Assert.IsTrue(field.NextCell(x, y).IsAlive, "Cell is not alive.");
    }

    /// <summary>
    /// Should get a living cell for a position with a living cell that has 2 living neighbouring cells.
    /// </summary>
    [Test()]
    public void NextCellTest_ShouldKeepAlive_WhenTwoNeighboursAndAliveInCurrentGen()
    {
      var pos = centeredCellPosition;
      var x = pos.x;
      var y = pos.y;

      // 2 neighbours pos
      var neighboursPos = new List<(int x, int y)>()
      {
        { (pos.x - 1, pos.y - 1) },
        { (pos.x - 1, pos.y) },
      };

      // Set neighbours alive
      foreach (var (neighbourX, neighbourY) in neighboursPos)
      {
        field.Cells[neighbourX, neighbourY].IsAlive = true;
      }

      // Set position is currently alive
      field.Cells[x, y].IsAlive = true;

      // Assert next cell is alive
      Assert.IsTrue(field.NextCell(x, y).IsAlive, "Cell is not alive.");
    }

    /// <summary>
    /// Should get a dead cell for a position with a dead cell that has 2 living neighbouring cells.
    /// </summary>
    [Test()]
    public void NextCellTest_ShouldNotBringAlive_WhenTwoNeighboursAndDeadInCurrentGen()
    {
      var pos = centeredCellPosition;
      var x = pos.x;
      var y = pos.y;

      // 2 neighbours pos
      var neighboursPos = new List<(int x, int y)>()
      {
        { (pos.x - 1, pos.y - 1) },
        { (pos.x - 1, pos.y) },
      };

      // Set neighbours alive
      foreach (var (neighbourX, neighbourY) in neighboursPos)
      {
        field.Cells[neighbourX, neighbourY].IsAlive = true;
      }

      // Set position is currently dead
      field.Cells[x, y].IsAlive = false;

      // Assert next cell is dead
      Assert.IsFalse(field.NextCell(x, y).IsAlive, "Cell is alive.");
    }

    /// <summary>
    /// Should get a dead cell for a position that has less than 2 living neighbouring cells.
    /// </summary>
    /// <param name="neighboursCount">Living neighbouring cells count.</param>
    [Test()]
    public void NextCellTest_ShouldSetToDead_WhenLessThanTwoNeighbours()
    {
      var pos = centeredCellPosition;
      var x = pos.x;
      var y = pos.y;

      // 1 neighbour pos
      var neighboursPos = new List<(int x, int y)>()
      {
        { (pos.x - 1, pos.y - 1) },
      };

      // Set neighbours alive
      foreach (var (neighbourX, neighbourY) in neighboursPos)
      {
        field.Cells[neighbourX, neighbourY].IsAlive = true;
      }

      // Assert next cell is dead
      Assert.IsFalse(field.NextCell(x, y).IsAlive, "Cell is alive.");
    }

    /// <summary>
    /// Should get a dead cell for a position that has more than 3 living neighbouring cells.
    /// </summary>
    /// <param name="neighboursCount">Living neighbouring cells count.</param>
    [Test()]
    public void NextCellTest_ShouldSetToDead_WhenMoreThanThreeNeighbours()
    {
      var pos = centeredCellPosition;
      var x = pos.x;
      var y = pos.y;

      // 4 neighbours pos
      var neighboursPos = new List<(int x, int y)>()
      {
        { (pos.x - 1, pos.y - 1) },
        { (pos.x - 1, pos.y) },
        { (pos.x, pos.y - 1) },
        { (pos.x + 1, pos.y + 1) },
      };

      // Set neighbours alive
      foreach (var (neighbourX, neighbourY) in neighboursPos)
      {
        field.Cells[neighbourX, neighbourY].IsAlive = true;
      }

      // Assert next cell is dead
      Assert.IsFalse(field.NextCell(x, y).IsAlive, "Cell is alive.");
    }
  }
}