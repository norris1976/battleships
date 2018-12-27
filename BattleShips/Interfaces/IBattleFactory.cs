using BattleShips.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Interfaces
{
    public interface IBattleFactory
    {
        Ship CreateShip(int length);
        BattleGrid CreateGrid(int size);
    }
}
