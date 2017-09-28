using System.Collections.Generic;
using StringExpressionCalculator.Abstract;

namespace StringExpressionCalculator.Concrete
{
    public sealed class ExpressionBuilder : IExpressionBuilder
    {
        private readonly IExpressionChain _chain;
        private IExpression _root;
        private readonly List<string> _expressions;

        public ExpressionBuilder(IExpressionChain chain)
        {
            _chain = chain;
            _expressions = new List<string>();
            _root = null;
        }

        public IExpression Build()
        {
            var priority = 0;

            foreach (var e in _expressions)
            {
                _chain.Add(ref priority, e, this);
            }

            return _root;
        }

        public void Append(string expression)
        {
            expression = expression?.Trim();

            _expressions.Add(expression);
        }

        public void Append(IExpression expression)
        {
            if (_root == null || _root.Priority <= expression.Priority)
            {
                expression.Left = _root;
                _root = expression;
            }
            else
            {
                Sort(_root, expression);
            }
        }

        public void Clear()
        {
            _root = null;

            _expressions.Clear();
        }

        private void Sort(IExpression root, IExpression value)
        {
            if (root.Right == null)
            {
                root.Right = value;
            }
            else if (root.Right.Priority <= value.Priority)
            {
                value.Left = root.Right;
                root.Right = value;
            }
            else
            {
                Sort(root.Right, value);
            }
        }
    }
}
