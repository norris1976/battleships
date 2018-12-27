using BattleShips.Interfaces;
using BattleShips.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Utils
{
    public class BattleFactory : IBattleFactory
    {
        public BattleGrid CreateGrid(int size)
        {
            return new BattleGrid(size);
        }

        public Ship CreateShip(int length)
        {
            return new Ship(length);
        }
    }
}
