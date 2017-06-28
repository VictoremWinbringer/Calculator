using System;

namespace Calculator
{
    sealed class Calc:ICalc
    {
        private readonly IParser _parser;
        private readonly ILogger _logger;

        public Calc(IParser parser, ILogger logger)
        {
            _parser = parser;
            _logger = logger;
        }

        public double Calculate(string expression)
        {
            try
            {
                var exp = _parser.Parse(expression);

                return exp.Interpret();

            }
            catch (Exception ex)
            {
                _logger.Log("Не удалось вычислить выражение ", ex);
                throw;
            }
        }
    }
}
