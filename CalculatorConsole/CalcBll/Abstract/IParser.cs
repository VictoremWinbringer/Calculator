namespace CalcBll.Abstract
{
 public interface IParser
    {
        IExpression Parse(string expression);
    }
}
