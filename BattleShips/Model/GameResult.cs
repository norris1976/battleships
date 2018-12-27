using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Model
{
    public class GameResult
    {
        public bool GameOver { get; set; } = false;

        public string Message { get; set; }

        public int NumberOfShots { get; set; }
    }
}
