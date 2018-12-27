using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BattleShips.Model;

namespace BattleShips.Tests.Models
{
    [Trait("Category", "# Battle Grid")]
    public class BattleGridTest
    {
        #region Create Grid

        [Trait("Category", "Create")]
        [Fact(DisplayName = "BattleGrid -> cell count with zero size")]
        public void GivenNewGridCalledWhenSizeIsZeroThenCellsCountCorrect()
        {
            // Arrange
            var sut = new BattleGrid(0);

            // Act
            var result = sut.Cells;

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "BattleGrid -> cell count with size of 1")]
        public void GivenNewGridCalledWhenSizeIsOneThenCellsCountCorrect()
        {
            // Arrange
            var sut = new BattleGrid(1);

            // Act
            var result = sut.Cells;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Length);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "BattleGrid -> cell count with valid size")]
        public void GivenNewGridCalledWhenSizeIsValidThenCellsCountCorrect()
        {
            // Arrange
            var sut = new BattleGrid(10);

            // Act
            var result = sut.Cells;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Length);
        }

        #endregion Create Grid

        #region Place Ships

        [Trait("Category", "Place Ships")]
        [Fact(DisplayName = "BattleGrid PlaceShips -> No ships")]
        public void GivenPlaceShipsCalledWhenNoShipsSuppliedThenCells()
        {
            // Arrange
            var sut = new BattleGrid(10);

            // Act
            sut.PlaceShips(new Ship[] { null });

            // Assert
            for(int col = 0; col < 10; col++)
            {
                for (int row = 0; row < 10; row++)
                {
                    var cell = sut.Cells[col, row];
                    Assert.Null(cell.Ship);
                }
            }
        }

        [Trait("Category", "Place Ships")]
        [Fact(DisplayName = "BattleGrid PlaceShips -> No collisions")]
        public void GivenPlaceShipsCalledWhenAcceptableshipsSuppliedThenNoCollisions()
        {
            // Arrange
            var ship42 = new Ship(4);
            var ship41 = new Ship(4);
            var ship5 = new Ship(5);
            var sut = new BattleGrid(10);

            // Act
            sut.PlaceShips(new Ship[] { ship5, ship41, ship42 });

            // Assert
            Assert.Equal(4, ship41.Cells.Length);
            Assert.Equal(4, ship41.Coords.Count);
            Assert.Same(ship41.Cells[0].Ship, ship41);
            Assert.Same(ship41.Cells[1].Ship, ship41);
            Assert.Same(ship41.Cells[2].Ship, ship41);
            Assert.Same(ship41.Cells[3].Ship, ship41);
        }

        #endregion Place Ships
    }
}
