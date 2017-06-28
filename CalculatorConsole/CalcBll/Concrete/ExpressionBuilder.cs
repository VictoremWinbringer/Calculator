using System;
using System.Collections.Generic;
using CalcBll.Abstract;
using Calculator;

namespace CalcBll.Concrete
{
    public sealed class ExpressionBuilder : IExpressionBuilder
    {
        private readonly IExpressionChain _chain;
        private IExpression _root;
        private readonly List<string> _expressions;
        private readonly IExpressionValidator _validator;

        public ExpressionBuilder(IExpressionChain chain, IExpressionValidator validator)
        {
            _chain = chain;
            _expressions = new List<string>();
            _root = null;
            _validator = validator;
        }

        public IExpression Build()
        {
            var valid = _validator.IsValid(_expressions);

            if (!valid.Item1)
                throw new ArgumentException($"Not valid simbol {_expressions[valid.Item2]} on index {valid.Item2}");

            foreach (var e in _expressions)
            {
                _chain.Add(ref _root, e);
            }

            return _root;
        }

        public void Append(string expression)
        {
            expression = expression?.Trim();

            _expressions.Add(expression);
        }
    }
}
