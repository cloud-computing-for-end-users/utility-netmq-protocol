using protocol.server_methods;
using System;
using System.Collections.Generic;
using System.Text;
using static net_mq_util.ProtocolConstants;

namespace protocol.methods.server_methods
{
    public class HelloWorldMethod : ServerMethod
    {
        public static readonly string METHOD_NAME = MET_SM_HelloWorld;

        public string param1 { get; set; }

        public HelloWorldMethod()
        {

        }
        public HelloWorldMethod(string param1)
        {
            this.param1 = param1;
        }
    }
}
