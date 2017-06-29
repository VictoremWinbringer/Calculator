using System;
using CalcBll.Abstract;

namespace CalcBll.Concrete.Expressions
{
    class AddExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public int Priority { get; set; }

        public double Interpret()
        {
            return Left.Interpret() + Right.Interpret();
        }
    }
}
