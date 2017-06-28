using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcBll.Abstract
{
    public interface IExpressionChain
    {
        void Add(ref IExpression root, string expression);
    }
}
