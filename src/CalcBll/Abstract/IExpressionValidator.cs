using System;
using System.Collections.Generic;

namespace StringExpressionCalculator.Abstract
{
    public interface IExpressionValidator
    {
        Tuple<bool,int> IsValid(IEnumerable<string> expressions);
    }
}
