namespace Calculator
{
    class MultiplicationExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Interpret()
        {
            return Left.Interpret() * Right.Interpret();
        }
    }
}
