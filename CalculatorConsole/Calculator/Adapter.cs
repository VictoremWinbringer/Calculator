using CalcBll.Abstract;
using System;

namespace Calculator
{
    sealed class Adapter : IAdapter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
