﻿using System;
using System.Collections.Generic;
using StringExpressionCalculator.Abstract;

namespace StringExpressionCalculator.Concrete
{
    public sealed class Parser : IParser
    {
        private IExpressionBuilder _builder;
        private readonly IExpressionValidator _validator;

        public Parser(IExpressionBuilder builder, IExpressionValidator validator)
        {
            _builder = builder;
            _validator = validator;
        }
        private bool TryAddChar(char c, List<char> chars)
        {
            if (!char.IsDigit(c) && !c.Equals('.'))
                return false;

            chars.Add(c);

            return true;
        }

        public IExpression Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException($"{nameof(expression)} is empty");

            var expressions = new List<string>();

            var chars = new List<char>(100);

            foreach (char c in expression)
            {
                if (TryAddChar(c, chars))
                    continue;

                if (c.Equals(' '))
                    continue;

                if (chars.Count > 0)
                    expressions.Add(new string(chars.ToArray()));

                chars.Clear();

                expressions.Add(c.ToString());

            }

            if (chars.Count > 0)
                expressions.Add(new string(chars.ToArray()));

            var valid = _validator.IsValid(expressions);

            if (!valid.Item1)
                throw new ArgumentException($"Not valid symbol {expressions[valid.Item2]} on index {valid.Item2}");

            foreach (var e in expressions)
                _builder.Append(e);

            var exp = _builder.Build();

            _builder.Clear();

            return exp;
        }
    }
}
