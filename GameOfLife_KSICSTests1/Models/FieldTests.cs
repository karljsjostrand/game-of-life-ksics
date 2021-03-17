using NUnit.Framework;
using GameOfLife_KSICS.Models;
using System;

namespace GameOfLife_KSICS.Models.Tests
{
  [TestFixture()]
  public class FieldTests
  {
    Field _field;
    (int, int) CenteredCellPosition;
    (int, int) EdgeCellPosition;

    [SetUp()]
    public void Setup()
    {
      var fieldWidth = 80;
      var fieldHeight = 40;

      _field = new Field(fieldWidth, fieldHeight);

      CenteredCellPosition = (fieldWidth / 2, fieldHeight / 2);
      EdgeCellPosition = (fieldWidth, fieldHeight);
    }

    /// <summary>
    /// Should return 0 when no neighbour alive. 
    /// </summary>
    [Test()]
    public void GetNeighourCountTest_ShouldReturnZero_WhenNoNeighourAlive()
    {
      var x = 40;
      var y = 20;
      var expected = 0;
      var actual = _field.GetNeighourCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    [TestCase(40, 19)] // above, 12 o clock
    [TestCase(41, 19)] // 13:30
    [TestCase(41, 20)] // 15
    [TestCase(41, 21)] // 16:30
    [TestCase(40, 21)] // 18
    [TestCase(39, 21)] // 19:30
    [TestCase(39, 20)] // 21
    [TestCase(39, 19)] // 22:30
    public void GetNeighourCountTest_ShouldReturnOne_WhenOneNeighbourAlive(int neighbourX, int neighbourY)
    {
      var x = 40;
      var y = 20;

      _field.Cells[neighbourX, neighbourY].Alive = true;

      var expected = 1;
      var actual = _field.GetNeighourCount(x, y);

      Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void GetNeighourCountTest_ShouldReturnThree_WhenThreeNeighboursAlive()
    {
      var x = 40;
      var y = 20;

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
      var x = EdgeCellPosition.Item1;
      var y = EdgeCellPosition.Item2;

      try
      {
        _field.GetNeighourCount(x, y);
      }
      catch (Exception)
      {
        Assert.Fail();
      }
    }
  }
}