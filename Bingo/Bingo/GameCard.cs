namespace Bingo
{
    public class GameCard
    {
        private readonly Game _game;
        private readonly Cell[,] _cells;

        public GameCard(Game game)
        {
            _game = game;
            _cells = new Cell[Game.GameCardRows, Game.GameCardColumns];

            InitializeCells();

            _game.OnNextNumberDrafted += MarkNumber;
        }

        public event Action<GameCard>? OnWinnningCondiditonAppeared;

        public Cell[,] GetCells()
        {
            return _cells;
        }

        public void MarkNumber(int number)
        {
            for (int i = 0; i < Game.GameCardRows; i++)
            {
                for (int j = 0; j < Game.GameCardColumns; j++)
                {
                    var cell = _cells[i, j];

                    if (cell.Number != number) { continue; }

                    cell.MarkCell();

                    var winnable = CheckWinningCondition();
                    if (winnable)
                    {
                        OnWinnningCondiditonAppeared?.Invoke(this);
                    }

                    return;
                }
            }
        }

        private bool CheckWinningCondition()
        {
            var winningConditionHandler = new WinningConditionHandler();
            winningConditionHandler.SetNext(new DiagonalWinningConditionHandler());

            return winningConditionHandler.CheckForWinningCondition(this);
        }

        private void InitializeCells()
        {
            var minVal = Game.MinValue;
            var maxVal = Game.MaxValue;

            var random = new Random(Guid.NewGuid().GetHashCode());

            var availableNumbers = new List<int>(Enumerable.Range(minVal, maxVal));

            for (int i = 0; i < Game.GameCardRows; i++)
            {
                for (int j = 0; j < Game.GameCardColumns; j++)
                {
                    var numberIndex = random.Next(0, availableNumbers.Count - 1);
                    var number = availableNumbers[numberIndex];

                    _cells[i, j] = new Cell(number);

                    availableNumbers.RemoveAt(numberIndex);
                }
            }
        }
    }
}
