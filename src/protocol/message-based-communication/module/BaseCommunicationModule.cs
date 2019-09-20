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
        protected ModuleType moduleType;
        public ModuleType ModuleType { get { return moduleType; } }
        public BaseCommunicationModule(ModuleType moduleType)
        {
            this.moduleType = moduleType;
        }


        public virtual void Setup(ConnectionInformation baseRouterModule, Port baseRouterRegistrationPort, ConnectionInformation forSelf, Encoding customEncoding)
        {
            this.proxyHelper = new ProxyHelper();
            this.proxyHelper.Setup(baseRouterModule,baseRouterRegistrationPort,moduleType, forSelf, this, customEncoding);
        }

    }
}
