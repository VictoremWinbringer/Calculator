using System;
using StringExpressionCalculator.Abstract;
using StringExpressionCalculator.Concrete.Expressions;

namespace StringExpressionCalculator.Concrete.Chains
{
  public  class DivChain:IExpressionChain
    {
        private readonly IExpressionChain _next;

        public DivChain(IExpressionChain next)
        {
            _next = next;
        }
        public void Add(ref int priority, string exp, IExpressionBuilder builder)
        {
            if (exp.Equals("/", StringComparison.Ordinal))
            {
                priority++;

                var expression = new DivisionExpression();

                expression.Priority = priority + 2000;

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
