using System.Linq;
using NUnit.Framework;

namespace GameOfLife.Tests
{
  [TestFixture]
  public class TestGameOfLife
  {
    [TestFixture]
    public class Reset
    {
      [Test]
      public void GivenWidth1_Height1_ShouldCreateGameWith1Cell()
      {
        var sut = CreateSut();

        sut.Reset(1, 1);

        Assert.AreEqual(1, sut.Cells.Length);
      }

      [Test]
      public void GivenWidth2_Height3_ShouldCreateGameWith6Cells()
      {
        var sut = CreateSut();

        sut.Reset(2, 3);

        Assert.AreEqual(6, sut.Cells.Length);
      }

      [Test]
      public void ShouldCreateACellAtEachPosition()
      {
        var sut = CreateSut();

        sut.Reset(2, 2);

        foreach (var cell in sut.Cells)
        {
          Assert.IsInstanceOf<ICell>(cell);
        }
      }
    }


    [TestFixture]
    public class Tick
    {
      [Test]
      public void CellWithNoLiveNeighbors_Dies()
      {
        // Arrange
        var sut = CreateSut();
        sut.Reset(2, 2);
        sut.Cells[0, 0].CurrentState = true;

        // Act
        sut.Tick();

        // Assert
        Assert.False(sut.Cells[0, 0].CurrentState);
      }

      [Test]
      public void CellWith2LiveNeighbors_StayCurrentState()
      {
        // Arrange
        var sut = CreateSut();
        sut.Reset(2, 2);
        sut.Cells[0, 0].CurrentState = true;
        sut.Cells[1, 0].CurrentState = true;
        sut.Cells[0, 1].CurrentState = true;

        // Act
        sut.Tick();

        // Assert
        Assert.True(sut.Cells[0, 0].CurrentState);
      }

      [Test]
      public void CellWith4LiveNeighbors_Dies()
      {
        // Arrange
        var sut = CreateSut();
        sut.Reset(3, 3);
        sut.Cells[1, 1].CurrentState = true;

        sut.Cells[0, 0].CurrentState = true;
        sut.Cells[0, 1].CurrentState = true;
        sut.Cells[0, 2].CurrentState = true;
        sut.Cells[1, 0].CurrentState = true;

        // Act
        sut.Tick();

        // Assert
        Assert.False(sut.Cells[1, 1].CurrentState);
      }

      [Test]
      public void CellWith3LiveNeighbors_BecomesAlive()
      {
        // Arrange
        var sut = CreateSut();
        sut.Reset(2, 2);
        sut.Cells[0, 0].CurrentState = false;
        sut.Cells[1, 0].CurrentState = true;
        sut.Cells[0, 1].CurrentState = true;
        sut.Cells[1, 1].CurrentState = true;

        // Act
        sut.Tick();

        // Assert
        Assert.True(sut.Cells[0, 0].CurrentState);
      }


      [TestFixture]
      public class VerifyStaticExamples
      {
        [Test]
        public void SquareBlock()
        {
          // Arrange
          var sut = CreateSut();
          sut.Reset(4, 4);
          sut.Cells[1, 1].CurrentState = true;
          sut.Cells[1, 2].CurrentState = true;
          sut.Cells[2, 1].CurrentState = true;
          sut.Cells[2, 2].CurrentState = true;

          // Act
          sut.Tick();

          // Assert
          var expectedAlive = new[]
          {
            (1, 1), (1, 2), (2, 1), (2, 2)
          };
          foreach (var cell in sut.Cells)
          {
            var shouldBeAlive = expectedAlive.Any(coords => coords.Item1 == cell.X && coords.Item2 == cell.Y);

            Assert.AreEqual(shouldBeAlive, cell.CurrentState);
          }
        }
      }

      [TestFixture]
      public class VerifyBlinkerExamples
      {
        [Test]
        public void Line()
        {
          // Arrange
          var sut = CreateSut();
          sut.Reset(5, 5);
          sut.Cells[1, 2].CurrentState = true;
          sut.Cells[2, 2].CurrentState = true;
          sut.Cells[3, 2].CurrentState = true;

          // Act
          sut.Tick();

          // Assert
          var expectedAlive = new[]
          {
            (2, 1), (2, 2), (2, 3)
          };
          foreach (var cell in sut.Cells)
          {
            var shouldBeAlive = expectedAlive.Any(coords => coords.Item1 == cell.X && coords.Item2 == cell.Y);

            Assert.AreEqual(shouldBeAlive, cell.CurrentState);
          }
        }
      }
    }


    private static GameOfLife CreateSut()
    {
      return new GameOfLife();
    }
  }
}
