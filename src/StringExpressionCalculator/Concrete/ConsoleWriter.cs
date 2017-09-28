using System;
using StringExpressionCalculator.Abstract;

namespace StringExpressionCalculator.Concrete
{
    public sealed class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
