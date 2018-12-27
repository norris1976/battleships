using BattleShips.Interfaces;
using BattleShips.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Controller
{
    public class GameController : IGameController
    {
        // Factory for returning dependent objects
        private readonly IBattleFactory _factory;

        // The grid that looks after the cells
        protected BattleGrid _grid;

        // The list of ships in the battle
        protected List<Ship> _ships;

        // Current shot count
        protected int _shotCount = 0;

        // The default grid size
        protected int _gridSize = 10;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">The factory for creating objects</param>
        public GameController(IBattleFactory factory)
        {
            _factory = factory;
            _ships = new List<Ship>();
        }

        /// <summary>
        /// Access to the grid's cells
        /// </summary>
        public Cell[,] Cells
        {
            get
            {
                if (_grid == null)
                    return null;

                return _grid.Cells;
            }
        }

        /// <summary>
        /// The shot couunt for the current game
        /// </summary>
        public int Shots => _shotCount;

        /// <summary>
        /// The number of ships yet to be destroyed.
        /// </summary>
        public int ShipsStillToSink => _ships.Count(s => !s.IsDestroyed);

        /// <summary>
        /// Add a ship to the game. Should be called before CreateGame.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public bool AddShip(int size)
        {
            // Create the ship
            var ship = _factory.CreateShip(size);
            _ships.Add(ship);

            // Place the ship on the grid
            return _grid.PlaceShips(new Ship[] { ship });
        }

        /// <summary>
        /// Sets up the game resources.
        /// </summary>
        public void CreateGame()
        {
            _shotCount = 0;
            _ships.Clear();

            _grid = _factory.CreateGrid(_gridSize);
        }

        /// <summary>
        /// Receives a coordinate and takes a shot on the battle grid. 
        /// </summary>
        /// <param name="coord">The coordinate in the form "A5"</param>
        /// <returns>Result showing the status of the shot and game.</returns>
        public GameResult EnterCoords(string coord)
        {
            // Check the input
            if (string.IsNullOrEmpty(coord) || coord.Length < 2)
                return new GameResult { Message = $"Invalid coord." };

            // Check the numeric part of the coordinate.
            string col = coord[0].ToString();
            string rowString = coord.Substring(1);
            int row = 0;
            var validInt = int.TryParse(rowString, out row);
            if(!validInt)
                return new GameResult { Message = $"Invalid coord." };

            // More validation on the actual column and row
            int column = (int)col[0] % 32;
            if (column > _gridSize)
                return new GameResult { Message = $"Invalid column coord: {col}" };
            if (row > _gridSize)
                return new GameResult { Message = $"Invalid row coord: {row}" };

            // Get the cell from the grid, checking if it's a hit.
            var selectedCell = Cells[column-1, row-1];
            var isHit = selectedCell.TakeShot();
            bool gameOver = _ships.Where(s => s.IsDestroyed).Count() == _ships.Count();
            _shotCount++;

            // Return message to the indicate success or not.
            string message = "Oooooh! So close - but that's a miss!";
            if (isHit)
                message = "Yeah! You got a hit!";
            if (selectedCell.Ship != null && selectedCell.Ship.IsDestroyed)
                message = "Yes! That's a ship destroyed!";
            if (gameOver)
                message = $"Well done! That's all ships destroyed in {_shotCount} shots.";

            return new GameResult {
                GameOver = gameOver,
                NumberOfShots = _shotCount,
                Message = message
            };
        }
    }
}
