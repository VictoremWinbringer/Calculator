using CalcBll.Abstract;
using System;

namespace CalcBll.Concrete.Expressions
{
    class DivisionExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Interpret()
        {
            var right = Right.Interpret();

            if (Math.Abs(right) <= double.Epsilon)
                throw new DivideByZeroException();

            return Left.Interpret() / right;
        }
    }
}
