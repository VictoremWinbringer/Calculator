using CalcBll.Abstract;

namespace Calculator
{
  public  interface IExpressionBuilder
    {
        void Append(string expression);
        IExpression Build();
    }
}
