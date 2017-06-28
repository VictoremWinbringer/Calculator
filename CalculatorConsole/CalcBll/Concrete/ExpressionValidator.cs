using CalcBll.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcBll.Concrete
{
    public sealed class ExpressionValidator : IExpressionValidator
    {
        public Tuple<bool, int> IsValid(IEnumerable<string> expressions)
        {
            int i = 0;

            var list = expressions.ToList();

            foreach (var exp in list)
            {


                if (string.IsNullOrWhiteSpace(exp))
                    return new Tuple<bool, int>(false, i);

                if (!Regex.IsMatch(exp, @"^\d+\.?\d*$") && !Regex.IsMatch(exp, "^[-,+,*,/]$"))
                    return new Tuple<bool, int>(false, i);

                if (Regex.IsMatch(exp, "^[-,+,*,/]$")
                    && (i == 0
                    || i == list.Count - 1
                    || Regex.IsMatch(list[i - 1], "^[-,+,*,/]$")
                    ))
                    return new Tuple<bool, int>(false, i);

                if (Regex.IsMatch(exp, @"^\d+\.?\d*$")
                    && i > 0
                    && Regex.IsMatch(list[i - 1], @"^\d+\.?\d*$"))
                    return new Tuple<bool, int>(false, i);

                i++;
            }

            return new Tuple<bool, int>(true, 0);
        }
    }
}
