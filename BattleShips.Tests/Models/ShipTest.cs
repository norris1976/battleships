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
    [Trait("Category", "# Ship")]
    public class ShipTest
    {
        #region IsDestroyed

        [Trait("Category", "IsDestroyed")]
        [Fact(DisplayName = "Ship -> all cells empty")]
        public void GivenIsDestroyedCalledWhenCellsEmptyThenIsDestroyedReturnsFalse()
        {
            // Arrange
            var sut = new Ship(4);
            sut.Cells = new Cell[]
            {
                new Cell{ Status = CellStatus.Empty },
                new Cell{ Status = CellStatus.Empty },
                new Cell{ Status = CellStatus.Empty }
            };

            // Act
            var result = sut.IsDestroyed;

            // Assert
            Assert.False(result);
        }

        [Trait("Category", "IsDestroyed")]
        [Fact(DisplayName = "Ship -> one cell empty")]
        public void GivenIsDestroyedCalledWhenOneCellEmptyThenIsDestroyedReturnsFalse()
        {
            // Arrange
            var sut = new Ship(4);
            sut.Cells = new Cell[]
            {
                new Cell{ Status = CellStatus.Hit },
                new Cell{ Status = CellStatus.Hit },
                new Cell{ Status = CellStatus.Empty }
            };

            // Act
            var result = sut.IsDestroyed;

            // Assert
            Assert.False(result);
        }

        [Trait("Category", "IsDestroyed")]
        [Fact(DisplayName = "Ship -> all cells hit")]
        public void GivenIsDestroyedCalledWhenAllCellsHitThenIsDestroyedReturnsTrue()
        {
            // Arrange
            var sut = new Ship(4);
            sut.Cells = new Cell[]
            {
                new Cell{ Status = CellStatus.Hit },
                new Cell{ Status = CellStatus.Hit },
                new Cell{ Status = CellStatus.Hit }
            };

            // Act
            var result = sut.IsDestroyed;

            // Assert
            Assert.True(result);
        }

        #endregion IsDestroyed
    }
}
