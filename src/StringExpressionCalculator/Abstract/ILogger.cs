using System;

namespace StringExpressionCalculator.Abstract
{
  public  interface ILogger
    {
        void Log(string message, Exception ex);
    }
}
