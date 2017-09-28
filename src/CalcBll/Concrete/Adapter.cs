using System;
using CalcBll.Abstract;

namespace CalcBll.Concrete
{
    sealed class Adapter : IAdapter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
