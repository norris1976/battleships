using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Model
{
    public class BattleGrid
    {
        // Default size
        private int _size = 10;

        // Property to get cells of grid.
        public Cell[,] Cells { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Size of grid to create</param>
        public BattleGrid(int size)
        {
            _size = size;

            Cells = new Cell[size, size];

            for(int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Cells[col, row] = new Cell();
                }
            }
        }

        /// <summary>
        /// Works out where ships can potentially be placed, depending on available space.
        /// </summary>
        /// <param name="cells">The cells to choose from</param>
        /// <param name="col">The column to look at</param>
        /// <param name="row">The row to look at</param>
        /// <param name="moveAcross">True of looking at horizontal space, otherwise false.</param>
        /// <returns></returns>
        private int GetAvailableStartingCells(Cell[,] cells, int col, int row, bool moveAcross)
        {
            // Check the bounds
            if(col > cells.GetUpperBound(0) || row > cells.GetUpperBound(1))
                return 0;

            // Get the cell - if already assigned to ship, then don't continue.
            var cell = cells[col, row];
            if (cell.Ship != null)
                return 0;

            // Recursively work out the next space, whether up or down.
            if(moveAcross)
                return 1 + GetAvailableStartingCells(cells, ++col, row, moveAcross);
            else
                return 1 + GetAvailableStartingCells(cells, col, ++row, moveAcross);
        }

        /// <summary>
        /// Checks for free space and assigns ships to that space.
        /// </summary>
        /// <param name="ships">The ships to position.</param>
        /// <returns>False, if there is a clash in position</returns>
        public bool PlaceShips(Ship[] ships)
        {
            if (ships == null)
                return false;

            // Loop each ship
            var rnd = new Random();
            foreach (var ship in ships)
            {
                if (ship == null)
                    continue;

                // These lists hold the potential places a ship could be placed.
                List<int[]> VerticalStarts = new List<int[]>();
                List<int[]> HorizontalalStarts = new List<int[]>();

                // Loop through each cell and assess it's suitability for the current ship.
                for (int row = 0; row < _size; row++)
                {
                    for (int col = 0; col < _size; col++)
                    {
                        // Check if there are enough spaces vertically
                        int length = GetAvailableStartingCells(this.Cells, col, row, false);
                        if (length >= ship.Length)
                            VerticalStarts.Add(new int[] { col, row });

                        // Check if there are enough spaces horizontally
                        length = GetAvailableStartingCells(this.Cells, col, row, true);
                        if (length >= ship.Length)
                            HorizontalalStarts.Add(new int[] { col, row });
                    }
                }

                // All potential positions have been worked out.
                // Now we need to randomly choose will we be placing ships horizontally or vertically.
                var hOrV = rnd.NextDouble();
                var vertical = hOrV < 0.5;
                List<int[]> chosenGroup = vertical ? VerticalStarts : HorizontalalStarts;

                // We have now picked the chosen position.
                // Let's work out the acyual cells to choose.
                var index = rnd.Next(chosenGroup.Count - 1);
                var startCellCoords = chosenGroup[index];

                List<Cell> cellsForShip = new List<Cell>();
                for (int i = 0; i < ship.Length; i++)
                {
                    int col = startCellCoords[0];
                    int row = startCellCoords[1];
                    if (vertical)
                        row += i;
                    else
                        col += i;

                    Cell cell = Cells[col, row];
                    Debug.WriteLine($"{col}, {row}");
                    ship.Coords.Add($"{col}, {row}");
                    if (cell.Ship != null)
                    {
                        cellsForShip.ForEach(c => c.Ship = null);
                        return false;
                    }
                    cell.Ship = ship;
                    cellsForShip.Add(cell);
                }
                ship.Cells = cellsForShip.ToArray();
            }
            return true;
        }
    }
}
