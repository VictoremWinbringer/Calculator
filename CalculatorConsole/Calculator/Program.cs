using System;
using System.Linq;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите выражение. Поддерживаються только -, +, /,* без скобок, без знака. В качестве десятичного разделителя используеться точка.");

                string expression = Console.ReadLine();

                try
                {

                    ICalc calc = new Calc(new Parser(new ExpressionBuilder()), new Logger(new Adapter()));

                    Console.WriteLine(calc.Calculate(expression));
                }
                catch
                {
                    Console.WriteLine("По пробуйте снова");
                }
            }
        }
    }
}
