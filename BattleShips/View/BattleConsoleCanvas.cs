using BattleShips.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.View
{
    public class BattleConsoleCanvas : IBattleCanvas
    {
        public void Draw(IGameController game)
        {
            if (game == null)
                return;

            int rows = game.Cells.GetUpperBound(1);
            int cols = game.Cells.GetUpperBound(0);

            for (int row = rows; row >= 0; row--)
            {

                Console.WriteLine();
                for (int col = 0; col <= cols; col++)
                {
                    var cell = game.Cells[col, row];
                    if (cell.Status == Model.CellStatus.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" + ");
                    }
                    else if (cell.Status == Model.CellStatus.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" O ");
                    }
                    else if (cell.Status == Model.CellStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" X ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{row + 1} ");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($" A  B  C  D  E  F  G  H  I  J");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Ships left to sink: {game.ShipsStillToSink}. Shots taken so far: {game.Shots}.");
            Console.WriteLine();
        }
    }
}
