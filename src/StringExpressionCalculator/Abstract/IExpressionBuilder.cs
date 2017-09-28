namespace StringExpressionCalculator.Abstract
{
    public interface IExpressionBuilder
    {
        void Append(string expression);
        IExpression Build();
        void Append(IExpression expression);
        void Clear();
    }
}
