using System.IO;
using System.Threading;
using StringExpressionCalculator.Abstract;

namespace StringExpressionCalculator.Concrete
{
    sealed class FileWriter : IWriter
    {
        private readonly string _file;
        private readonly Mutex _mutex;

        public FileWriter(string file)
        {
            _file = file;

            _mutex = new Mutex(false, typeof(FileWriter).FullName);
        }
        public void Write(string message)
        {
            _mutex.WaitOne();

            try
            {
                File.AppendAllText(_file, message + "\n\r");
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
    }
}
