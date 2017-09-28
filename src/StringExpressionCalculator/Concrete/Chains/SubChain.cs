using System;
using StringExpressionCalculator.Abstract;
using StringExpressionCalculator.Concrete.Expressions;

namespace StringExpressionCalculator.Concrete.Chains
{
    public class SubChain : IExpressionChain
    {
        private readonly IExpressionChain _next;

        public SubChain(IExpressionChain next)
        {
            _next = next;
        }

        public void Add(ref int priority, string exp, IExpressionBuilder builder)
        {
            if (exp.Equals("-", StringComparison.Ordinal))
            {
                priority++;

                var expression = new SubtractExpression();

                expression.Priority = priority + 3000;

                builder.Append(expression);
            }
            else
            {
                if (_next != null)
                    _next.Add(ref priority, exp, builder);
            }
        }
    }
}
