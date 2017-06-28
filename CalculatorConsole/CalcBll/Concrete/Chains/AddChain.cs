using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Add(ref IExpression root, string exp)
        {
            if (exp.Equals("+", StringComparison.Ordinal))
            {
                var expression = new AddExpression();
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
