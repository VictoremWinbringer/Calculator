using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите выражение. Поддерживаються только -, +, /,* без скобок без знака. В качестве десятичного разделителя используеться точка.");

                string expression = Console.ReadLine();

                var calc = new Calc(new ExpressionBuilder(), new Logger(new Adapter()));

                Console.WriteLine(calc.Calculate(expression));
            }
        }
    }

    sealed class Calc
    {
        private readonly IExpressionBuilder _builder;
        private readonly ILogger _logger;

        public Calc(IExpressionBuilder builder, ILogger logger)
        {
            _builder = builder;
            _logger = logger;
        }

        public double Calculate(string expression)
        {
            try
            {
                var chars = new List<char>(100);

                foreach (char c in expression)
                {
                    if (char.IsDigit(c) || c.Equals('.'))
                    {
                        chars.Add(c);
                        continue;
                    }

                    switch (c)
                    {
                        case '-':
                            AddNumberExpression(chars);
                            _builder.InsertSubtractExpression();
                            break;
                        case '+':
                            AddNumberExpression(chars);
                            _builder.InsertAddExpression();
                            break;
                        case '*':
                            AddNumberExpression(chars);
                            _builder.InsertMultiplicationExpression();
                            break;
                        case '/':
                            AddNumberExpression(chars);
                            _builder.InsertDivExpression();
                            break;
                        case ' ': break;
                        default:
                            throw new ArgumentException($"Не допустимый символ {c}");
                    }
                }

                AddNumberExpression(chars);

                return _builder.Build().Interpret();
            }
            catch (Exception ex)
            {
                _logger.Log("Не удалось вычислить выражение ", ex);
            }

            return 0;
        }

        private void AddNumberExpression(List<char> list)
        {
            if (list.Count == 0)
                throw new ArgumentException("Перед операцией нет чифр!");

            var s = new string(list.ToArray());

            if (!double.TryParse(s
                , System.Globalization.NumberStyles.AllowDecimalPoint
                , CultureInfo.CreateSpecificCulture("en-US")
                , out double result))
                throw new ArgumentException($"Не удалось распарсить строку {s}");

            _builder.InsertNumberExpression(result);

            list.Clear();
        }
    }

    sealed class Parser
    {
        struct Expression
        {
            public string Value;
            public int Order;
        }

        public List<string> Parse(string text)
        {
            var list = new List<Expression>(100);

            var chars = new List<char>(100);

            int order = 0;

            foreach (char c in text)
            {
                if (char.IsDigit(c) || c.Equals('.'))
                    chars.Add(c);

                switch (c)
                {
                    case '-':
                        list.Add(new Expression
                        {
                            Value = new string(chars.ToArray()),
                            Order = order
                        });
                        list.Add(new Expression
                        {
                            Value = "-",
                            Order = order
                        });
                        break;
                    default:
                        break;
                }
            }

            return list.OrderBy(x => x.Order).Select(x => x.Value).ToList();
        }
    }

    interface IAdapter
    {
        void Write(string message);
    }

    sealed class Adapter : IAdapter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    interface ILogger
    {
        void Log(string message, Exception ex);
    }

    sealed class Logger : ILogger
    {
        private readonly IAdapter _adapter;

        public Logger(IAdapter adapter)
        {
            _adapter = adapter;
        }

        public void Log(string message, Exception ex)
        {
            _adapter.Write(
                $">message -> {message}{Environment.NewLine}>exception -> {WithInner(ex)}{Environment.NewLine}>stack ->{ex.StackTrace}"
                );
        }

        private string WithInner(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            AddMessage(ex, sb);

            return sb.ToString();
        }

        private void AddMessage(Exception ex, StringBuilder sb)
        {
            if (ex == null)
                return;

            sb.Append(ex.Message);

            if (ex.InnerException != null)
                AddMessage(ex.InnerException, sb);
        }
    }

    interface IExpressionBuilder
    {
        void InsertNumberExpression(double value);
        void InsertAddExpression();
        void InsertSubtractExpression();
        void InsertMultiplicationExpression();
        void InsertDivExpression();
        IExpression Build();
    }

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
                || _root is MultiplicationExpression
                || _root is DivisionExpression)
            {
                AddRoot(expression);
                return;
            }

            var root = _root;

            while (root.Right.Right != null)
            {
                root = root.Right;
            }

            expression.Left = root.Right;
            root.Right = expression;
        }

        public void InsertAddExpression()
        {
            AddRoot(new AddExpression());
        }

        public void InsertMultiplicationExpression()
        {
            MulDiv(new MultiplicationExpression());
        }

        public void InsertSubtractExpression()
        {
            AddRoot(new SubtractExpression());
        }
        public void InsertDivExpression()
        {
            MulDiv(new DivisionExpression());
        }

        public void InsertNumberExpression(double value)
        {
            var exp = new NumberExpression(value);

            if (_root == null)
                _root = exp;

            else
            {
                var root = _root;

                while (root.Right != null)
                {
                    root = root.Right;
                }
                root.Right = exp;
            }
        }
    }

    interface IExpression
    {
        double Interpret();

        IExpression Left { get; set; }
        IExpression Right { get; set; }
    }

    sealed class NumberExpression : IExpression
    {
        private readonly double _variable;

        public NumberExpression(double variable)
        {
            _variable = variable;
        }

        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Interpret()
        {
            return _variable;
        }
    }

    class AddExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Interpret()
        {
            return Left.Interpret() + Right.Interpret();
        }
    }

    class SubtractExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Interpret()
        {
            return Left.Interpret() - Right.Interpret();
        }
    }

    class MultiplicationExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Interpret()
        {
            return Left.Interpret() * Right.Interpret();
        }
    }

    class DivisionExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Interpret()
        {
            return Left.Interpret() / Right.Interpret();
        }
    }
}
