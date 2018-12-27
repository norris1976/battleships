using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Interfaces
{
    public interface IBattleCanvas
    {
        void Draw(IGameController game);
    }
}
