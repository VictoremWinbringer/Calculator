using System;

namespace CalcBll.Abstract
{
  public  interface ILogger
    {
        void Log(string message, Exception ex);
    }
}
