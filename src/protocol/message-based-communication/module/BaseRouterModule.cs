using message_based_communication.model;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;
using message_based_communication.encoding;
using message_based_communication.connection;
using message_based_communication.setup;

namespace message_based_communication.module
{
    public abstract class BaseRouterModule : BaseServerModule
    {
        abstract protected string MODULE_ID_PREFIXES { get; }

        protected Dictionary<ModuleID, RequestSocket> moduleIdToProxyHelper = new Dictionary<ModuleID, RequestSocket>();
        protected Dictionary<ModuleType, RequestSocket> moduleTypeToProxyHelper = new Dictionary<ModuleType, RequestSocket>();
        Encoding customEncoder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"> the port that this router module will listen for modules trying to register</param>
        public BaseRouterModule(Port portForRegistrationToRouter,ModuleType moduleType, Encoding customEncoding) : base(moduleType)
        {
            new Thread(() => {
                HandleRegisterModuleRequest(portForRegistrationToRouter, customEncoding);
            }).Start();

        }

        //private void SendSendableUsingConnectionInformation(Sendable sendable, ConnectionInformation connectionInformation)
        //{
        //    throw new NotImplementedException();
        //}


        protected override void SendResponse(Response response)
        {
            //var hash1 = response.TargetModuleID.GetHashCode();
            //foreach(var k in this.moduleIdToProxyHelper.Keys)
            //{
            //    var hash2 = k.GetHashCode();
            //}
            if (this.moduleIdToProxyHelper.ContainsKey(response.TargetModuleID))
            {

                var connection = this.moduleIdToProxyHelper[response.TargetModuleID];
                connection.SendMultipartMessage(Encoding.EncodeResponse(response));

                var ack = connection.ReceiveMultipartMessage();
                ProxyHelper.ValidateAckMessage(ack, response, this.customEncoder);
            }
            else
            {
                base.SendResponse(response);
            }
        }


        public void HandleSendable(Sendable sendable)
        {
            //throwExceptionIfMissiongSetupCall();
            if (sendable is BaseRequest request)
            {
                if (base.moduleType.TypeID.Equals(request.TargetModuleType.TypeID))
                {
                    //handle request
                    HandleRequest(request);
                }
                else
                {
                    //forward the message
                    ForwardSendable(request);
                }
            }
            else if (sendable is Response response)
            {
                if (base.moduleID == response.TargetModuleID)
                {
                    //response for self
                    //HandleResponse(response);
                    throw new Exception("Do I need this?");
                }
                else
                {
                    ForwardSendable(response);
                }

            }
        }
        protected void ForwardSendable(Sendable sendable)
        {

            RequestSocket connection = null;
            NetMQMessage message = null;
            if (sendable is BaseRequest request)
            {
                connection= this.moduleTypeToProxyHelper[request.TargetModuleType];
                message = Encoding.EncodeRequest(request);
            }
            else if (sendable is Response response)
            {
                connection = this.moduleIdToProxyHelper[response.TargetModuleID];
                message = Encoding.EncodeResponse(response);
            }

            connection.SendMultipartMessage(message);
            var ack = connection.ReceiveMultipartMessage();
            ProxyHelper.ValidateAckMessage(ack, sendable, this.customEncoder);
        }



        private void HandleRegisterModuleRequest(Port port, Encoding customEncoding)
        {
            ResponseSocket resSocket = new ResponseSocket("tcp://0.0.0.0:" + port.ThePort);

            
            while (true)
            {
                var message = resSocket.ReceiveMultipartMessage();
                var decoded = encoding.Encoding.TryDecodeRegisterModuleRequest(message);
                if(null == decoded)
                {
                    throw new Exception("got a null while trying to decode ");
                }

                var moduleID = RegisterModule(decoded.ModuleType, decoded.ConnInfo);
                var response = new RegisterModuleResponse()
                {
                    CallID = decoded.CallID,
                    ModuleID = moduleID

                };
                var encodedResponse = encoding.Encoding.EncodeRegisterModuleResponse(response);
                resSocket.SendMultipartMessage(encodedResponse);
            }
        }

        private ModuleID RegisterModule(ModuleType moduleType, ConnectionInformation connectionInformation)
        {
            ModuleID moduleID = null;
            do
            {
                moduleID = new ModuleID() { ID = MODULE_ID_PREFIXES + new Random().Next() };

            } while (moduleIdToProxyHelper.ContainsKey(moduleID));

            var requestSocket = new RequestSocket("tcp://" + connectionInformation.IP.TheIP + ":" + connectionInformation.Port.ThePort);
            moduleIdToProxyHelper.Add(moduleID, requestSocket);
            moduleTypeToProxyHelper.Add(moduleType, requestSocket);
            return moduleID;
        }

        public override void Setup(ConnectionInformation baseRouterModule, Port baseRouterRegistrationPort, ConnectionInformation forSelf, Encoding customEncoding)
        {
            this.customEncoder = customEncoding;
            base.Setup(baseRouterModule, baseRouterRegistrationPort, forSelf, customEncoding);
        }
    }
}
