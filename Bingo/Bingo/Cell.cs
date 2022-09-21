namespace Bingo
{
    public class Cell
    {
        public Cell(int number)
        {
            Number = number;
        }

        public int Number { get; }
        public bool IsMarked { get; private set; }

        public void MarkCell()
        {
            IsMarked = true;
        }
    }
}
