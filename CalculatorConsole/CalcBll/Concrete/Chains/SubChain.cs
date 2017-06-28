using System;
using CalcBll.Abstract;
using CalcBll.Concrete.Expressions;

namespace CalcBll.Concrete.Chains
{
    public class SubChain : IExpressionChain
    {
        private readonly IExpressionChain _next;

        public SubChain(IExpressionChain next)
        {
            _next = next;
        }
        public void Add(ref IExpression root, string exp)
        {
            if (exp.Equals("-", StringComparison.Ordinal))
            {
                var expression = new SubtractExpression();

                expression.Left = root;

                root = expression;

            }
            else
            {
                if (_next != null)
                    _next.Add(ref root, exp);
            }
        }
    }
}
