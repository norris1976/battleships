using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Model
{
    public class Ship
    {
        public Cell[] Cells { get; set; }

        public List<string> Coords { get; set; } = new List<string>();

        public Ship(int length)
        {
            Length = length;
        }

        public int Length { get; set; }

        public bool IsDestroyed
        {
            get
            {
                return Cells.Where(s => s.Status == CellStatus.Hit).Count() == Cells.Length;
            }
        }
    }
}
