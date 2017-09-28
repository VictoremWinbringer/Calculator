using System.Globalization;
using System.Text.RegularExpressions;
using StringExpressionCalculator.Abstract;
using StringExpressionCalculator.Concrete.Expressions;

namespace StringExpressionCalculator.Concrete.Chains
{
    public class NumChain : IExpressionChain
    {
        private readonly IExpressionChain _next;

        public NumChain(IExpressionChain next)
        {
            _next = next;
        }

        public void Add(ref int priority, string exp, IExpressionBuilder builder)
        {
            if (Regex.IsMatch(exp, @"^\d+\.?\d*$"))
            {
                priority++;

                var value = double.Parse(exp, CultureInfo.CreateSpecificCulture("en-US"));

                var expression = new NumberExpression(value);

                expression.Priority = priority;

                builder.Append(expression);
            }
            else
            {
                if (_next != null)
                    _next.Add(ref priority, exp, builder);
            }
        }
    }
}
