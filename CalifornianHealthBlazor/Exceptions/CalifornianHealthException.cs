using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalifornianHealthBlazor.Exceptions
{
    public class CalifornianHealthException : Exception
    {
        public CalifornianHealthException(string message)
            : base(message)
        {
        }
    }
}
