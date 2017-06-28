using System;
using System.Text;

namespace Calculator
{
    sealed class Logger : ILogger
    {
        private readonly IAdapter _adapter;

        public Logger(IAdapter adapter)
        {
            _adapter = adapter;
        }

        public void Log(string message, Exception ex)
        {
            _adapter.Write(
                $">message -> {message}{Environment.NewLine}>exception -> {WithInner(ex)}{Environment.NewLine}>stack ->{ex.StackTrace}"
                );
        }

        private string WithInner(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            AddMessage(ex, sb);

            return sb.ToString();
        }

        private void AddMessage(Exception ex, StringBuilder sb)
        {
            if (ex == null)
                return;

            sb.Append(ex.Message);

            if (ex.InnerException != null)
                AddMessage(ex.InnerException, sb);
        }
    }
}
