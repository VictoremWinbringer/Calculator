using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcBll.Abstract
{
    public interface IExpressionChain
    {
        void Add(ref int priority, string exp, IExpressionBuilder builder);
    }
}
