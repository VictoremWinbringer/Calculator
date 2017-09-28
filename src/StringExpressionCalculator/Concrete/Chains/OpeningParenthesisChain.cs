using System;
using StringExpressionCalculator.Abstract;

namespace StringExpressionCalculator.Concrete.Chains
{
    public class OpeningParenthesisChain : IExpressionChain
    {
        IExpressionChain _next;
        public OpeningParenthesisChain(IExpressionChain next)
        {
            _next = next;
        }
        public void Add(ref int priority, string exp, IExpressionBuilder builder)
        {
            if (exp.Equals("(", StringComparison.Ordinal))
            {
                priority -= 10000;
            }
            else
            {
                if (_next != null)
                    _next.Add(ref priority, exp, builder);
            }
        }
    }
}
