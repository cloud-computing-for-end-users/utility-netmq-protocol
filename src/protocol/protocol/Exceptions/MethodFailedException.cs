using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.Exceptions
{
    public class MethodFailedException : Exception
    {

        public MethodFailedException (string errorMessage = "The method failed to execute as expected") : base(errorMessage)
        {

        }
    }
}
