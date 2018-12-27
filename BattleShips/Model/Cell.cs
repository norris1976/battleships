using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Model
{
    public class Cell
    {
        /// <summary>
        /// The ship (if any) assigned to this cell.
        /// </summary>
        public Ship Ship { get; set; }

        // the cell status.
        public CellStatus Status { get; set; } = CellStatus.Empty;

        /// <summary>
        /// Takes a shot on this cell.
        /// </summary>
        /// <returns>True if it's hit</returns>
        public bool TakeShot()
        {
            this.Status = (this.Ship == null) ? CellStatus.Miss : CellStatus.Hit;

            return this.Status == CellStatus.Hit;
        }
    }
}
