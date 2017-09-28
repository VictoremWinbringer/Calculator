using System;
using StringExpressionCalculator.Abstract;

namespace Calculator
{
    sealed class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
