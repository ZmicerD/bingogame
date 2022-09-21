using System.Text;
using Bingo;

const ConsoleKey continueCommand = ConsoleKey.Spacebar;

var game = new Game();

game.OnWinnerAppeared += NotificateWinner;

var card = game.GetNewPlayerCard();

RenderGameCard(card);

var continuePlaying = true;

do
{
    Console.WriteLine($"Please press {continueCommand.ToString().ToUpper()} to draft a number and continue the game");
    Console.WriteLine("Press any other key to leave the game");
    var inputKey = Console.ReadKey().Key;
    if (inputKey != continueCommand)
    {
        break;
    }
    Console.WriteLine();

    var number = game.DraftNumber();

    Console.WriteLine($"Drafted number: {number}");
    Console.WriteLine();

    RenderGameCard(card);
} while (continuePlaying);

void RenderGameCard(GameCard card)
{
    Console.WriteLine("Player card:");

    var cells = card.GetCells();

    var rows = cells.GetLength(0);
    var columns = cells.GetLength(1);

    var stringBuilder = new StringBuilder();
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < columns; j++)
        {
            var cell = cells[i, j];
            stringBuilder.Append(cell.IsMarked ? $"({cell.Number})\t" : $"{cell.Number}\t");
        }
        stringBuilder.Append(Environment.NewLine);
    }
    Console.WriteLine(stringBuilder.ToString());
    Console.WriteLine();
}

void NotificateWinner(int winningNumber, GameCard winningCard)
{
    Console.WriteLine("CONGRATULATIONS!!!!!");
    Console.WriteLine();

    Console.WriteLine($"WINNING NUMBER IS: {winningNumber}");
    Console.WriteLine();

    Console.WriteLine("WINNING CARD IS:");
    RenderGameCard(winningCard);

    Environment.Exit(0); 
}