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
            var list = expressions.ToList();

            Stack<string> stack = new Stack<string>(100);

            int openCount = 0;
            int closedCount = 0;

            foreach (var exp in list)
            {
                if (string.IsNullOrWhiteSpace(exp)
                    || !IsOperation(exp)
                    || IsExcess(exp, stack.Count, list.Count)
                    || (stack.Count > 0
                    && (IsCopy(stack, exp, "^[-,+,*,/]$") || IsCopy(stack, exp, @"^\d+\.?\d*$"))))
                    return new Tuple<bool, int>(false, stack.Count);

                if (CheckIsOpen(stack, exp, ref openCount))
                    return new Tuple<bool, int>(false, stack.Count);

                if (CheckIsClose(stack, exp, ref closedCount))
                    return new Tuple<bool, int>(false, stack.Count);

                stack.Push(exp);
            }

            if (openCount != closedCount)
                return new Tuple<bool, int>(false, 0);

            return new Tuple<bool, int>(true, 0);
        }

        bool IsExcess(string exp, int index, int totalCount)
        {
            return Regex.IsMatch(exp, "^[-,+,*,/]$")
                && (index == 0
                || index == totalCount - 1);
        }

        bool IsOperation(string exp)
        {
            return Regex.IsMatch(exp, @"^\d+\.?\d*$") || Regex.IsMatch(exp, "^[-,+,*,/,),(]$");
        }

        bool CheckIsClose(Stack<string> stack, string exp, ref int count)
        {
            if (exp.Equals(")", StringComparison.Ordinal))
            {
                count++;

                return stack.Count > 0
                    && (stack.Peek().Equals("(", StringComparison.Ordinal)
                    || Regex.IsMatch(stack.Peek(), "^[-,+,*,/]$"));
            }

            return false;
        }

        bool CheckIsOpen(Stack<string> stack, string exp, ref int count)
        {
            if (exp.Equals("(", StringComparison.Ordinal))
            {
                count++;

                return stack.Count > 0 && Regex.IsMatch(stack.Peek(), @"^\d+\.?\d*$");
            }

            return false;
        }

        bool IsCopy(Stack<string> stack, string exp, string pattern)
        {
            return Regex.IsMatch(exp, pattern)
                && Regex.IsMatch(stack.Peek(), pattern);
        }
    }
}
