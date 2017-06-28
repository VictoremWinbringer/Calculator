using System;
using System.Collections.Generic;

namespace Calculator
{
    sealed class Parser : IParser
    {
        IExpressionBuilder _builder;
        public Parser(IExpressionBuilder builder)
        {
            _builder = builder;
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
                throw new NullReferenceException($"{nameof(expression)} is empty");

            var chars = new List<char>(100);

            foreach (char c in expression)
            {
                if (TryAddChar(c, chars))
                    continue;

                if (c.Equals(' '))
                    continue;

                _builder.Append(new string(chars.ToArray()));

                chars.Clear();

                _builder.Append(c.ToString());

            }

            if (chars.Count > 0)
                _builder.Append(new string(chars.ToArray()));

            return _builder.Build();
        }
    }
}
