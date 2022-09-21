namespace Bingo
{
    public interface IWinningConditionHandler
    {
        IWinningConditionHandler SetNext(IWinningConditionHandler handler);

        bool CheckForWinningCondition(GameCard card);
    }

    public abstract class AbstractWinningConditionHandler : IWinningConditionHandler
    {
        private IWinningConditionHandler _nextHandler;

        public IWinningConditionHandler SetNext(IWinningConditionHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual bool CheckForWinningCondition(GameCard card)
        {
            if (this._nextHandler == null) { return false; }

            return this._nextHandler.CheckForWinningCondition(card);
        }
    }
}
