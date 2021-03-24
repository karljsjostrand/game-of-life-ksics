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
    (int x, int y) centeredCellPos;

    /// <summary>
    /// Setup a new field.
    /// </summary>
    [SetUp()]
    public void Setup()
    {
      var field = new Field(fieldWidth, fieldHeight);

      gameOfLife = new GameOfLife(field);

      centeredCellPos = (fieldWidth / 2, fieldHeight / 2);
    }

    /// <summary>
    /// Should assert that the next generation of cells have 3 cells alive from
    /// a field with 3 living cells in a centered row. One being the cell in 
    /// the absolute center, and the others being the cells directly above and
    /// below the center cell.
    /// </summary>
    [Test()]
    public void NextGenerationTest_ShouldHave2Alive_WhenNextGeneration()
    {
      var center = centeredCellPos;

      // Bring a row of 3 cells alive in the center of the field
      gameOfLife.Field.Cells[center.x - 1, center.y].Alive = true;
      gameOfLife.Field.Cells[center.x,     center.y].Alive = true;
      gameOfLife.Field.Cells[center.x + 1, center.y].Alive = true;

      // Step to next generation
      gameOfLife.NextGeneration();

      // Assert that the center cell in the row and the center cell 
      // in the row below is alive on next generation
      Assert.IsTrue(gameOfLife.Field.Cells[center.x, center.y - 1].Alive, "Not alive above centered");
      Assert.IsTrue(gameOfLife.Field.Cells[center.x, center.y].Alive, "Not alive centered");
      Assert.IsTrue(gameOfLife.Field.Cells[center.x, center.y + 1].Alive, "Not alive below centered");
    }
  }
}