using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcBll.Abstract;
using CalcBll.Concrete.Expressions;

namespace CalcBll.Concrete.Chains
{
  public  class DivChain:IExpressionChain
    {
        private readonly IExpressionChain _next;

        public DivChain(IExpressionChain next)
        {
            _next = next;
        }
        public void Add(ref IExpression root, string exp)
        {
            if (exp.Equals("/", StringComparison.Ordinal))
            {
                var expression = new DivisionExpression();
               
                if (root is NumberExpression
                    || root is DivisionExpression
                    || root is MultiplicationExpression)
                {
                    expression.Left = root;
                    root = expression;
                    return;
                }

                if (root.Right == null)
                    throw new ArgumentException("Не верное выражение !", new NullReferenceException(nameof(root.Right)));

                expression.Left = root.Right;

                root.Right = expression;
            }
            else
            {
                _next.Add(ref root, exp);
            }
        }
    }
}
