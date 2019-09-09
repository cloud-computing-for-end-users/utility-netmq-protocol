using ManualTeSts.consts;
using ManualTeSts.cust_encoding;
using message_based_communication.encoding;
using message_based_communication.model;
using message_based_communication.setup;
using NetMQ.Sockets;
using System;

namespace ManualTeSts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            //var response = new Response()
            //{
            //    CallID = new CallID() { ID=4 },
            //    Object = new Payload() { ThePayload = "hello" },
            //    SenderModuleID = new ModuleID() { ID = 15},
            //    TargetModuleID = new ModuleID() { ID = 14 }
            //};

            //var encodedResponse = Encoding.EncodeSendable(response);
            //var decodedResponse = Encoding.DecodeIntoSendable(encodedResponse);
            //testing encoder


            var sm_conn_info = new ConnectionInformation() { IP = new IP() { TheIP = "127.0.0.1" }, Port = new Port() { ThePort = 5522 } };
            var so_conn_info = new ConnectionInformation() { IP = new IP() { TheIP = "127.0.0.1" }, Port = new Port() { ThePort = 9965 } };


            var customEncoding = new CustomEncoding();

            var request = new RegisterModuleRequest()
            {
                CallID = new CallID() { ID = "SETUP_" + new Random().Next() },
                ModuleType = new ModuleType() { TypeID = ModuleTypeConsts.ServerModuleType },
                ConnInfo = so_conn_info
            };
            var encoded = Encoding.EncodeRegisterModuleRequest(request);
            var decoded = Encoding.TryDecodeRegisterModuleRequest(encoded);



            var sm = new ServerModule(new Port() { ThePort = 5523 }, new ModuleType() { TypeID = ModuleTypeConsts.ServerModuleType });

            //var reqSocket = new RequestSocket("tcp://127.0.0.1:5522");

            //var sm_conn_info = new ConnectionInformation() { IP = new IP() { TheIP = "127.0.0.1" }, Port = new Port() { ThePort = 5522 } };
            sm.Setup(sm_conn_info, new Port() { ThePort=5523 }, sm_conn_info, customEncoding);

            var so = new SlaveOwnerServermodule(new Port() { ThePort = 9966 }, new ModuleType() { TypeID = ModuleTypeConsts.SlaveOwnerServermoduleModuleType});
            //var so_conn_info = new ConnectionInformation() { IP = new IP() { TheIP = "127.0.0.1" }, Port = new Port() { ThePort = 9965 } };

            so.Setup(sm_conn_info,new Port() {ThePort=5523 }, so_conn_info, customEncoding);

            so.Test();


            Console.ReadKey();

        }
    }
}
