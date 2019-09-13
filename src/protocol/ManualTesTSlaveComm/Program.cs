using client_slave_message_communication.proxy;
using message_based_communication.model;
using System;
using System.Threading;

namespace ManualTesTSlaveComm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var client = new GenericClientModule(new message_based_communication.model.ModuleType() { TypeID= custom_message_based_implementation.consts.ModuleTypeConst.MODULE_TYPE_CLIENT});

            var connForSlave = new ConnectionInformation()
            {
                IP = new IP() { TheIP = "127.0.0.1" },
                Port = new Port() { ThePort = 10142 }
            };
            var connForSelf= new ConnectionInformation()
            {
                IP = new IP() { TheIP = "127.0.0.1" },
                Port = new Port() { ThePort = 2224 }
            };
            client.Setup(connForSlave, new Port() { ThePort = 10143 }, connForSelf, new client_slave_message_communication.encoding.CustomEncoding());

            client.MoveMouse(new client_slave_message_communication.model.RelativeScreenLocation()
            {
                FromLeft = new client_slave_message_communication.model.Percent() { ThePercentage = 100 },
                FromTop = new client_slave_message_communication.model.Percent() { ThePercentage = 100}
            });

            Thread.Sleep(5000);

            client.MoveMouse(new client_slave_message_communication.model.RelativeScreenLocation()
            {
                FromLeft = new client_slave_message_communication.model.Percent() { ThePercentage = 50 },
                FromTop = new client_slave_message_communication.model.Percent() { ThePercentage = 50 }
            });

            Console.ReadKey();
        }
    }
}
