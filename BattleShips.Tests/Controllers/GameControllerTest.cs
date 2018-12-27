using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BattleShips.Model;
using BattleShips.Interfaces;
using BattleShips.Controller;

namespace BattleShips.Tests.Controllers
{
    [Trait("Category", "# GameController")]
    public class GameControllerTest
    {
        #region Cells

        [Trait("Category", "Cells")]
        [Fact(DisplayName = "No game created -> no cells")]
        public void GivenCellsCalledWhenNoGameCreatedThenReturnsNull()
        {
            // Arrange
            Mock<IBattleFactory> MockFactory = new Mock<IBattleFactory>();
            var sut = new GameController(MockFactory.Object);

            // Act
            var result = sut.Cells;

            // Assert
            Assert.Null(result);
        }

        [Trait("Category", "Cells")]
        [Fact(DisplayName = "Game created -> cells")]
        public void GivenCellsCalledWhenGameCreatedThenReturnsCells()
        {
            // Arrange
            Mock<IBattleFactory> MockFactory = new Mock<IBattleFactory>();
            MockFactory
                .Setup(_ => _.CreateGrid(10))
                .Returns(new BattleGrid(10));

            var sut = new GameController(MockFactory.Object);
            sut.CreateGame();

            // Act
            var result = sut.Cells;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, sut.Cells.LongLength);
        }

        #endregion Cells

        #region EnterCoords

        [Trait("Category", "EnterCoords")]
        [Fact(DisplayName = "Invalid letter as column -> game result message correct")]
        public void GivenEnterCoordsCalledWhenInvalidColumnThenReturns()
        {
            // Arrange
            Mock<IBattleFactory> MockFactory = new Mock<IBattleFactory>();
            MockFactory
                .Setup(_ => _.CreateGrid(10))
                .Returns(new BattleGrid(10));

            var sut = new GameController(MockFactory.Object);
            sut.CreateGame();

            // Act
            var result = sut.EnterCoords("asd2");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.NumberOfShots);
            Assert.False(result.GameOver);
            Assert.Equal("Invalid coord.", result.Message);
        }

        [Trait("Category", "EnterCoords")]
        [Fact(DisplayName = "Column letter too high -> game result message correct")]
        public void GivenEnterCoordsCalledWhenHighColumnThenReturns()
        {
            // Arrange
            Mock<IBattleFactory> MockFactory = new Mock<IBattleFactory>();
            MockFactory
                .Setup(_ => _.CreateGrid(10))
                .Returns(new BattleGrid(10));

            var sut = new GameController(MockFactory.Object);
            sut.CreateGame();

            // Act
            var result = sut.EnterCoords("K0");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.NumberOfShots);
            Assert.False(result.GameOver);
            Assert.Contains("Invalid column coord", result.Message);
        }

        [Trait("Category", "EnterCoords")]
        [Fact(DisplayName = "Row too high -> game result message correct")]
        public void GivenEnterCoordsCalledWhenHighRowThenReturns()
        {
            // Arrange
            Mock<IBattleFactory> MockFactory = new Mock<IBattleFactory>();
            MockFactory
                .Setup(_ => _.CreateGrid(10))
                .Returns(new BattleGrid(10));

            var sut = new GameController(MockFactory.Object);
            sut.CreateGame();

            // Act
            var result = sut.EnterCoords("J11");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.NumberOfShots);
            Assert.False(result.GameOver);
            Assert.Contains("Invalid row coord", result.Message);
        }

        [Trait("Category", "EnterCoords")]
        [Fact(DisplayName = "All cells hit -> game over")]
        public void GivenEnterCoordsCalledWhenAllCellsHitThenReturnsGameOVer()
        {
            // Arrange
            Mock<IBattleFactory> MockFactory = new Mock<IBattleFactory>();
            MockFactory
                .Setup(_ => _.CreateGrid(It.IsAny<int>()))
                .Returns(new BattleGrid(1));
            var ship = 
            MockFactory
                .Setup(_ => _.CreateShip(1))
                .Returns(new Ship(1));

            var sut = new GameController(MockFactory.Object);
            sut.CreateGame();
            sut.AddShip(1);

            // Act
            var result = sut.EnterCoords("A1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.NumberOfShots);
            Assert.True(result.GameOver);
            Assert.Contains("Well done! That's all ships destroyed", result.Message);
        }

        [Trait("Category", "EnterCoords")]
        [Fact(DisplayName = "Not all cells hit -> game not over")]
        public void GivenEnterCoordsCalledWhenNotAllCellsHitThenReturnsHit()
        {
            // Arrange
            Mock<IBattleFactory> MockFactory = new Mock<IBattleFactory>();
            MockFactory
                .Setup(_ => _.CreateGrid(It.IsAny<int>()))
                .Returns(new BattleGrid(2));
            var ship =
            MockFactory
                .Setup(_ => _.CreateShip(It.IsAny<int>()))
                .Returns(new Ship(2));

            var sut = new GameController(MockFactory.Object);
            sut.CreateGame();
            sut.AddShip(1);
            sut.AddShip(1);
            sut.EnterCoords("A1");
            sut.EnterCoords("A2");

            // Act
            var result = sut.EnterCoords("B1");

            // Assert
            Assert.NotNull(result);
            Assert.False(result.GameOver);
            Assert.Equal (3, result.NumberOfShots);
        }

        #endregion EnterCoords
    }
}
