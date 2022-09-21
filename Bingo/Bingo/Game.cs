namespace Bingo
{
    public class Game
    {
        public const int GameCardRows = 5;
        public const int GameCardColumns = 5;
        public const int MinValue = 1;
        public const int MaxValue = 52;

        private List<int> _gameNumbersSequence;
        private int _currentIndex;

        public Game()
        {
            _gameNumbersSequence = new List<int>();
            _currentIndex = 0;

            InitializeGameNumbersSequence();
        }

        public event Action<int>? OnNextNumberDrafted;
        public event Action<int, GameCard>? OnWinnerAppeared;

        public GameCard GetNewPlayerCard()
        {
            var card = new GameCard(this);

            card.OnWinnningCondiditonAppeared += ProcessPlayerCard;

            return card;
        }

        public int DraftNumber()
        {
            var number = _gameNumbersSequence.ElementAt(_currentIndex);

            OnNextNumberDrafted?.Invoke(number);

            _currentIndex++;

            return number;
        }

        private void InitializeGameNumbersSequence()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            var availableNumbers = new List<int>(Enumerable.Range(MinValue, MaxValue));
            var availableNumbersCount = availableNumbers.Count;

            for (int i = 0; i < availableNumbersCount; i++)
            {
                var numberIndex = random.Next(0, availableNumbers.Count - 1);

                _gameNumbersSequence.Add(availableNumbers[numberIndex]);

                availableNumbers.RemoveAt(numberIndex);
            }
        }

        private void ProcessPlayerCard(GameCard card)
        {
            var winningConditionHandler = new WinningConditionHandler();
            winningConditionHandler.SetNext(new DiagonalWinningConditionHandler());

            var isWinnableCard = winningConditionHandler.CheckForWinningCondition(card);

            if (isWinnableCard) { 
                OnWinnerAppeared?.Invoke(_gameNumbersSequence.ElementAt(_currentIndex), card); 
            }
        }
    }
}