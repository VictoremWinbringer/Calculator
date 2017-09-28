using System;
using StringExpressionCalculator.Abstract;
using StringExpressionCalculator.Concrete;
using StringExpressionCalculator.Concrete.Chains;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {

            ICalculator calc = new StringExpressionCalculator.Concrete.Calculator(new Parser(new ExpressionBuilder(
                        new OpeningParenthesisChain(
                            new ClosingParenthesisChain(
                                new NumChain(
                                    new AddChain(
                                        new SubChain(
                                            new MulChain(
                                                new DivChain(
                                                    null))))))))
                    , new ExpressionValidator())
                , new Logger(new ConsoleWriter()));

            Console.WriteLine("Введите выражение. Поддерживаються только -, +, /, *  без знака. В качестве десятичного разделителя используеться точка.");

            while (true)
            {
                string expression = Console.ReadLine();

                try
                {
                    Console.WriteLine("= " + calc.Calculate(expression));
                }
                catch
                {
                    Console.WriteLine("По пробуйте снова");
                }
            }
        }
    }
}
