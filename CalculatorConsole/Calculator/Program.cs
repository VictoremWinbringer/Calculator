using CalcBll.Abstract;
using CalcBll.Concrete;
using System;
using CalcBll.Concrete.Chains;

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
                    ICalc calc = new Calc(new Parser(new ExpressionBuilder(
                        new NumChain(
                            new AddChain(
                                new SubChain(
                                    new MulChain(
                                        new DivChain(
                                            null))))),new ExpressionValidator()
                        )), new Logger(new Adapter()));

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
