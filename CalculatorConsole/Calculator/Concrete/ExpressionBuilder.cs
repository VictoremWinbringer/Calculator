using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Calculator
{
    sealed class ExpressionBuilder : IExpressionBuilder
    {
        private volatile IExpression _root = null;

        public IExpression Build()
        {
            return _root;
        }

        private void AddRoot(IExpression expression)
        {
            expression.Left = _root;
            _root = expression;
        }

        private void MulDiv(IExpression expression)
        {
            if (_root is NumberExpression
                || _root is DivisionExpression
                || _root is MultiplicationExpression)
            {
                AddRoot(expression);
                return;
            }

            if (_root.Right == null)
                throw new ArgumentException("Не верное выражение !", new NullReferenceException(nameof(_root.Right)));

            expression.Left = _root.Right;

            _root.Right = expression;
        }

        private void InsertAddExpression()
        {
            AddRoot(new AddExpression());
        }

        private void InsertMultiplicationExpression()
        {
            MulDiv(new MultiplicationExpression());
        }

        private void InsertSubtractExpression()
        {
            AddRoot(new SubtractExpression());
        }

        private void InsertDivExpression()
        {
            MulDiv(new DivisionExpression());
        }

        private void InsertNumberExpression(double value)
        {
            var exp = new NumberExpression(value);

            if (_root == null)
            {
                _root = exp;
            }
            else
            {
                var root = _root;

                while (root.Right != null)
                    root = root.Right;

                if (root is NumberExpression)
                    throw new ArgumentException("Не валидное выражение!");

                if (root is DivisionExpression
                    && value < 0.000001
                    && value > -0.000001)
                    throw new DivideByZeroException("Деление на ноль!");

                root.Right = exp;
            }
        }

        public void Append(string expression)
        {
            if (Regex.IsMatch(expression, @"^\d+\.?\d*$"))
            {
                double result;
                if (!double.TryParse(expression
                                    , System.Globalization.NumberStyles.AllowDecimalPoint
                                    , CultureInfo.CreateSpecificCulture("en-US")
                                    , out result))

                    throw new ArgumentException($"Не удалось распарсить строку {expression}");

                InsertNumberExpression(result);

                return;
            }

            if (expression.Equals("-", StringComparison.Ordinal))
            {
                InsertSubtractExpression();
                return;
            }

            if (expression.Equals("+", StringComparison.Ordinal))
            {
                InsertAddExpression();
                return;
            }

            if (expression.Equals("*", StringComparison.Ordinal))
            {
                InsertMultiplicationExpression();
                return;
            }

            if (expression.Equals("/", StringComparison.Ordinal))
            {
                InsertDivExpression();
                return;
            }

            throw new ArgumentException($"Не допустимый символ {expression}");
        }
    }
}
