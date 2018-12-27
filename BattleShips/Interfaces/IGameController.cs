using BattleShips.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Interfaces
{
    public interface IGameController
    {
        void CreateGame();
        GameResult EnterCoords(string coord);
        Cell[,] Cells { get; }
        int Shots { get; }
        int ShipsStillToSink { get; }
    }
}
