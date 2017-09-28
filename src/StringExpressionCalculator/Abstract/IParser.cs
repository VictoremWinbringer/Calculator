namespace StringExpressionCalculator.Abstract
{
 public interface IParser
    {
        IExpression Parse(string expression);
    }
}
