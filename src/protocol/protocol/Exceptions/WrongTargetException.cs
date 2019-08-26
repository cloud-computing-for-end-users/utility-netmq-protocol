using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.Exceptions
{
    public class WrongTargetException : Exception
    {
        public WrongTargetException(string message = "The target type of the recived method does not match the expected value") : base(message)
        {

        }

    }
}
