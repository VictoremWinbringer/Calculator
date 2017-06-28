using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using CalcBll.Abstract;
using CalcBll.Concrete.Expressions;
using Calculator;

namespace CalcBll.Concrete
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
            foreach (var e in _expressions)
            {
                _chain.Add(ref _root, e);
            }

            return _root;
        }

        public void Append(string expression)
        {
            expression = expression?.Trim();

            if (string.IsNullOrWhiteSpace(expression))
                throw new NullReferenceException(nameof(expression));


            if (!Regex.IsMatch(expression, @"^\d+\.?\d*$") && !Regex.IsMatch(expression, "^[-,+,*,/]$"))
                throw new ArgumentException($"Не допустимый символ {expression}");

            _expressions.Add(expression);
        }
    }
}
