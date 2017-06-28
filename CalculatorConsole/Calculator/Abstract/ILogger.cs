using System;

namespace Calculator
{
    interface ILogger
    {
        void Log(string message, Exception ex);
    }
}
