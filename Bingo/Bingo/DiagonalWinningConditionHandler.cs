namespace Bingo
{
    public class DiagonalWinningConditionHandler : AbstractWinningConditionHandler
    {
        public override bool CheckForWinningCondition(GameCard card)
        {
            var rows = Game.GameCardRows;
            var columns = Game.GameCardColumns;

            if (rows != columns) { return base.CheckForWinningCondition(card); }

            var cells = card.GetCells();

            var isMainDiagonalMarked = true;
            var isRightDiagonalMarked = true;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var cell = cells[i, j];
                    if (i == j)
                    {
                        isMainDiagonalMarked = isMainDiagonalMarked && cell.IsMarked;
                    }
                    if ((i + j) == (rows - 1))
                    {
                        isRightDiagonalMarked = isRightDiagonalMarked && cell.IsMarked;
                    }
                }
            }

            if (isMainDiagonalMarked || isRightDiagonalMarked)
            {
                return true;
            }

            return base.CheckForWinningCondition(card);
        }
    }
}
