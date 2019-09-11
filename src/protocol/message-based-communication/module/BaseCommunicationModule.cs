using message_based_communication.connection;
using message_based_communication.encoding;
using message_based_communication.model;
using message_based_communication.setup;
using NetMQ;
using NetMQ.Sockets;
using System;

namespace message_based_communication.module
{
    public abstract class BaseCommunicationModule
    {
        public abstract string CALL_ID_PREFIX { get; }

        protected ProxyHelper proxyHelper;
        protected ModuleID moduleID;
        public ModuleID ModuleID { get { return this.moduleID;  } }
        protected ModuleType moduleType;
        public ModuleType ModuleType { get { return moduleType; } }
        protected ConnectionInformation communicationHub; //this must point to a BaseRouterModule connection

        public BaseCommunicationModule(ModuleType moduleType)
        {
            this.moduleType = moduleType;
        }


        public virtual void Setup(ConnectionInformation baseRouterModule, Port baseRouterRegistrationPort, ConnectionInformation forSelf, Encoding customEncoding)
        {
            //SetupHasBeenCalled = true;

            this.communicationHub = baseRouterModule;
                
            this.proxyHelper = new ProxyHelper();
            //this.connectionWrapper.Setup(baseRouterModule, this);


            this.moduleID = RegisterModule(this.moduleType, baseRouterModule, baseRouterRegistrationPort, forSelf);

            ////throw new NotImplementedException();
            ///

            this.proxyHelper.Setup(baseRouterModule, this, customEncoding, forSelf.Port);

        }

        private static ModuleID RegisterModule(ModuleType moduleType, ConnectionInformation baseRouterModule,Port baseRouterRegistrationPort , ConnectionInformation forSelf)
        {
            var reqSocker = new RequestSocket("tcp://" + baseRouterModule.IP.TheIP  + ":"+ baseRouterRegistrationPort.ThePort);

            var request = new RegisterModuleRequest()
            {
                CallID = new CallID() { ID = "SETUP_" + new Random().Next() },
                ModuleType = moduleType,
                ConnInfo = forSelf
            };

            var encodedReq = encoding.Encoding.EncodeRegisterModuleRequest(request);
            reqSocker.SendMultipartMessage(encodedReq);

            //reciving response
            var encodedResponse = reqSocker.ReceiveMultipartMessage();
            var decodedResponse = encoding.Encoding.TryDecodeRegisterModuleResponse(encodedResponse);


            return decodedResponse.ModuleID;

        }


    }
}
