using System;
using System.Collections.Generic;

namespace CalcBll.Abstract
{
    public interface IExpressionValidator
    {
        Tuple<bool,int> IsValid(IEnumerable<string> expressions);
    }
}
