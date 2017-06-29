using System;
using CalcBll.Abstract;

namespace CalcBll.Concrete.Expressions
{
    sealed class NumberExpression : IExpression
    {
        private readonly double _variable;

        public NumberExpression(double variable)
        {
            _variable = variable;
        }

        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public int Priority { get; set; }

        public double Interpret()
        {
            return _variable;
        }
    }
}
