namespace Bingo
{
    public class WinningConditionHandler : AbstractWinningConditionHandler
    {
        public override bool CheckForWinningCondition(GameCard card)
        {
            var rows = Game.GameCardRows;
            var columns = Game.GameCardColumns;

            var cells = card.GetCells();

            for (int i = 0; i < rows; i++)
            {
                var row = true;
                var col = true;
                for (int j = 0; j < columns; j++)
                {
                    var rowCell = cells[i, j];
                    var colCell = cells[j, i];
                    row = row && rowCell.IsMarked;
                    col = col && colCell.IsMarked;
                }
                if (row || col)
                {
                    return true;
                }
            }

            return base.CheckForWinningCondition(card);
        }
    }
}
