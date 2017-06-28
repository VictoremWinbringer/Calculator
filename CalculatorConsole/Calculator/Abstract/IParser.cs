namespace Calculator
{
    interface IParser
    {
        IExpression Parse(string expression);
    }
}
