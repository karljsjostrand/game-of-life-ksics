using NUnit.Framework;
using GameOfLife_KSICS;
using GameOfLife_KSICS.Models;

namespace GameOfLife_KSICS.Tests
{
  [TestFixture()]
  public class GameOfLifeTests
  {
    private GameOfLife gameOfLife;
    static readonly int fieldWidth = 20;
    static readonly int fieldHeight = 20;
    (int x, int y) centeredCellPosition;
    (int x, int y) topLeftCornerCellPosition;
    (int x, int y) topRightCornerCellPosition;
    (int x, int y) btmLeftCornerCellPosition;
    (int x, int y) btmRightCornerCellPosition;

    [SetUp()]
    public void Setup()
    {
      var field = new Field(fieldWidth, fieldHeight);

      gameOfLife = new GameOfLife(field);
    }

    [Test()]
    public void NextGenerationTest_ShouldHave2Alive_WhenNextGeneration()
    {
      // Bring a row of 3 cells alive
      gameOfLife.Field.Cells[2, 0].Alive = true;
      gameOfLife.Field.Cells[3, 0].Alive = true;
      gameOfLife.Field.Cells[4, 0].Alive = true;

      // Step to next generation
      gameOfLife.NextGeneration();

      // Assert that the center cell in the row and the center cell 
      // in the row below is alive on next generation
      Assert.IsTrue(gameOfLife.Field.Cells[3, 0].Alive, "3, 0");
      Assert.IsTrue(gameOfLife.Field.Cells[3, 1].Alive, "3, 1");
    }
  }
}