using CalcBll.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalcBll.Concrete
{
    public sealed class ExpressionValidator : IExpressionValidator
    {
        public Tuple<bool, int> IsValid(IEnumerable<string> expressions)
        {
            int i = 0;

            var list = expressions.ToList();

            var countOpen = list.Count(x => x == "(");
            var countClose = list.Count(x => x == "(");

            if (countClose != countOpen)
                return new Tuple<bool, int>(false, 0);

            Stack<string> stack = new Stack<string>(100);

            foreach (var exp in list)
            {
                if (string.IsNullOrWhiteSpace(exp))
                    return new Tuple<bool, int>(false, i);

                if (!Regex.IsMatch(exp, @"^\d+\.?\d*$") && !Regex.IsMatch(exp, "^[-,+,*,/,),(]$"))
                    return new Tuple<bool, int>(false, i);

                if (Regex.IsMatch(exp, "^[-,+,*,/]$")
                    && (i == 0
                    || i == list.Count - 1
                    || Regex.IsMatch(stack.Peek(), "^[-,+,*,/]$")
                    ))
                    return new Tuple<bool, int>(false, i);

                if (Regex.IsMatch(exp, @"^\d+\.?\d*$")
                    && i > 0
                    && Regex.IsMatch(stack.Peek(), @"^\d+\.?\d*$"))
                    return new Tuple<bool, int>(false, i);

                if (exp.Equals("(", StringComparison.Ordinal)
                    && Regex.IsMatch(stack.Peek(), @"^\d+\.?\d*$"))
                    return new Tuple<bool, int>(false, i);

                if (exp.Equals(")", StringComparison.Ordinal)
                    && Regex.IsMatch(stack.Peek(), "^[-,+,*,/]$"))
                    return new Tuple<bool, int>(false, i);

                stack.Push(exp);

                i++;
            }

            return new Tuple<bool, int>(true, 0);
        }
    }
}
