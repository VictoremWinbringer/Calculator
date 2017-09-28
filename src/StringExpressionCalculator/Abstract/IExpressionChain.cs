namespace StringExpressionCalculator.Abstract
{
    public interface IExpressionChain
    {
        void Add(ref int priority, string exp, IExpressionBuilder builder);
    }
}
