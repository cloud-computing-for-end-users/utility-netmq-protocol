using net_mq_decoder;
using net_mq_encoder;
using net_mq_util;
using NetMQ;
using NetMQ.Sockets;
using protocol.Exceptions;
using protocol.methods;
using protocol.methods.server_methods;
using protocol.model;
using protocol.server_methods;
using protocol.server_module_interfaces;
using protocol.util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using static net_mq_util.NetMqUtil;

namespace protocol.server_module_interface_proxies
{
    public class ServerModuleProxy : IServerModule
    {

        private ServermoduleID serverModuleID;
        protected CallIDManager callIDManager;

        protected RequestSocket reqSocket;
        public ServerModuleProxy(ServermoduleID servermoduleID)
        {
            //    this.mainProxy = MainProxy.INSTANCE;
            this.serverModuleID = servermoduleID;
            this.callIDManager = CallIDManager.INSTANCE;

            this.reqSocket = new RequestSocket("tcp://" + NetMqUtil.SERVER_MODULE_IP + ":" + NetMqUtil.SERVER_MODULE_PORT);
        }



        public void HelloWorld(Action<object> callBack)
        {
            var method = new HelloWorldMethod() {param1= "yo from the slaveOwner" };
            var encodedMethod = NetMqEncoder.GenerateServerModuleMethodMessage(this.serverModuleID, this.callIDManager.RegisterCall(callBack), method );
            this.reqSocket.SendMultipartMessage(encodedMethod);

        }

        //public static Success RegisterDatabaseServermodule(ConnectionInfo connInfo)
        //{
        //    var method = new RegisterDatabaseServermoduleMethod();
        //    method.ConnectionInfo = connInfo;

        //    var netMqMessage = NetMqEncoder.GenerateServerModuleMethodMessage(method);
        //    var response = this.mainProxy.CallMethod(netMqMessage);

        //    return NetMqDecoder.DecodeResponse<Success>(response);
        //}

        //public Success RegisterFileServermodule(ConnectionInfo connInfo)
        //{
        //    var method = new RegisterFileServermoduleMethod();
        //    method.ConnectionInfo = connInfo;

        //    var netMqMessage = NetMqEncoder.GenerateServerModuleMethodMessage(method);
        //    var response = this.mainProxy.CallMethod(netMqMessage);

        //    return NetMqDecoder.DecodeResponse<Success>(response);
        //}

        public static ServermoduleID RegisterSlaveOwnerServermodule(ConnectionInfo connInfo)
        {
            //var method = new RegisterSlaveOwnerServermoduleMethod();
            //method.ConnectionInfo = connInfo;

            //var netMqMessage = NetMqEncoder.GenerateServerModuleMethodMessage(method);
            //var response = this.mainProxy.CallMethod(netMqMessage);

            //return NetMqDecoder.DecodeResponse<Success>(response);


            return RegisterServermoduleByTargetType(TargetType.SlaveOwnerServermodule, connInfo);

        }

        protected static ServermoduleID RegisterServermoduleByTargetType(TargetType target, ConnectionInfo connInfo)
        {
            if (null == connInfo)
            {
                throw new Exception("Does not allow null");
            }

            ServerMethod method = null;
            if (TargetType.SlaveOwnerServermodule.Equals(target)) //TODO expand
            {
                method = new RegisterSlaveOwnerServermoduleMethod()
                {
                    ConnectionInfo = connInfo
                };

            }

            //socket only used for getting the servermodule ID
            var setupSocket = new RequestSocket();
            setupSocket.Connect("tcp://" + net_mq_util.NetMqUtil.SERVER_MODULE_IP + ":" + net_mq_util.NetMqUtil.SERVER_MODULE_PORT);



            var localCallID = new Random().Next();

            if(null == method)
            {
                throw new Exception();
            }
            var encodedMethod = net_mq_encoder.NetMqEncoder.GenerateServerModuleMethodMessage(new ServermoduleID() { ID = ProtocolConstants.SERVERMODULE_ID_NOT_YET_ASSIGNED },new CallID() {ID = localCallID }, method);

            setupSocket.SendMultipartMessage(encodedMethod);

            var response = setupSocket.ReceiveMultipartMessage();

            setupSocket.Close();

            var decodedReponse = net_mq_decoder.NetMqDecoder.DecodeResponse<ServermoduleID>(response);

            if (
                ProtocolConstants.SERVERMODULE_ID_NOT_YET_ASSIGNED.Equals(decodedReponse.Item2.Item1.ID)
                && localCallID.Equals(decodedReponse.Item2.Item2.ID)
                )
            {
                return decodedReponse.Item1;
            }
            else
            {
                throw new MethodFailedException("The SlaveOwner Setup method failed");
            }
        }

    }
}
