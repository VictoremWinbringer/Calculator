﻿namespace Calculator
{
    interface IExpression
    {
        double Interpret();

        IExpression Left { get; set; }
        IExpression Right { get; set; }
    }
}
