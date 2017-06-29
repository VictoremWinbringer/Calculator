using System;
using CalcBll.Abstract;
using CalcBll.Concrete.Expressions;

namespace CalcBll.Concrete.Chains
{
    public class AddChain : IExpressionChain
    {
        private readonly IExpressionChain _next;

        public AddChain(IExpressionChain next)
        {
            _next = next;
        }
        public void Add(ref int priority,string exp,IExpressionBuilder builder)
        {
            if (exp.Equals("+", StringComparison.Ordinal))
            {
                priority++;

                var expression = new AddExpression();

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
