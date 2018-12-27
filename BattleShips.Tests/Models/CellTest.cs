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
    [Trait("Category", "# Cell")]
    public class CellTest
    {
        #region TakeShot

        [Trait("Category", "TakeShot")]
        [Fact(DisplayName = "Cell -> no ship")]
        public void GivenTakeshotCalledWhenCellHasNoShipThenReturnsFalse()
        {
            // Arrange
            var sut = new Cell();

            // Act
            var result = sut.TakeShot();

            // Assert
            Assert.False(result);
            Assert.Equal(CellStatus.Miss, sut.Status);
        }

        [Trait("Category", "TakeShot")]
        [Fact(DisplayName = "Cell -> ship")]
        public void GivenTakeshotCalledWhenCellHasShipThenReturnsTrue()
        {
            // Arrange
            var sut = new Cell();
            sut.Ship = new Ship(4);

            // Act
            var result = sut.TakeShot();

            // Assert
            Assert.True(result);
            Assert.Equal(CellStatus.Hit, sut.Status);
        }

        #endregion TakeShot
    }
}
