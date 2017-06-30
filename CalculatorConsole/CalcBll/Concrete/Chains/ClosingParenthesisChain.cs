using CalcBll.Abstract;
using System;

namespace CalcBll.Concrete.Chains
{
  public  class ClosingParenthesisChain : IExpressionChain
    {
        IExpressionChain _next;
        public ClosingParenthesisChain(IExpressionChain next)
        {
            _next = next;
        }
        public void Add(ref int priority, string exp, IExpressionBuilder builder)
        {
            if (exp.Equals(")", StringComparison.Ordinal))
            {
                priority += 10000;
            }
            else
            {
                if (_next != null)
                    _next.Add(ref priority, exp, builder);
            }
        }
    }
}
