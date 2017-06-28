namespace Calculator
{
    interface IExpressionBuilder
    {
        void Append(string expression);
        IExpression Build();
    }
}
