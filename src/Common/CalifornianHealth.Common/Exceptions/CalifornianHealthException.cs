using System;
using System.Collections.Generic;
using System.Text;

namespace CalifornianHealth.Common.Exceptions
{
    public class CalifornianHealthException : Exception
    {
        public CalifornianHealthException(string message)
            : base(message)
        {
        }
    }
}
