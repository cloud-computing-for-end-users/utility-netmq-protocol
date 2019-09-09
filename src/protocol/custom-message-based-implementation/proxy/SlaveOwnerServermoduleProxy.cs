using custom_message_based_implementation.consts;
using custom_message_based_implementation.custom_request.slave_owner_servermodule;
using custom_message_based_implementation.model;
using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using message_based_communication.proxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace custom_message_based_implementation.proxy
{
    public class SlaveOwnerServermoduleProxy : BaseProxy
    {

        public SlaveOwnerServermoduleProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule) : base(proxyHelper, baseCommunicationModule)
        {
        }

        public void GetSlave(PrimaryKey arg1, ApplicationInfo arg2, Action<SlaveConnection> callBack)
        {
            //TODO
            Action<Response> handleResponse = res =>
            {
                callBack.Invoke(res.Payload.ThePayload as SlaveConnection);
            };

        }

        private readonly ModuleType ModuleType = new ModuleType() { TypeID = ModuleTypeConst.MODULE_TYPE_SLAVE_OWNER };


    }
}
