using custom_message_based_implementation.consts;
using custom_message_based_implementation.custom_request.slave_owner_servermodule;
using custom_message_based_implementation.model;
using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using message_based_communication.proxy;
using System;
using System.Collections.Generic;

namespace custom_message_based_implementation.proxy
{
    public class SlaveOwnerServermoduleProxy : BaseProxy
    {
        private readonly ModuleType moduleType = new ModuleType() { TypeID = ModuleTypeConst.MODULE_TYPE_SLAVE_OWNER };

        public SlaveOwnerServermoduleProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule) : base(proxyHelper, baseCommunicationModule) {}

        public void GetSlave(PrimaryKey arg1, ApplicationInfo arg2, Action<Slave> callBack)
        {
            var request = new RequestGetSlave {Arg1PrimaryKey = arg1, Arg2AppInfo = arg2};

            SetStandardParameters(request);

            base.SendMessage(WrapCallBack<Slave>(callBack), request);
        }

        public void GetListOfRunningApplications(Action<List<ApplicationInfo>> callBack)
        {
            var request = new RequestGetListOfRunningApplications();
            this.SetStandardParameters(request);

            base.SendMessage(WrapCallBack<List<ApplicationInfo>>(callBack), request);
        }

        protected override void SetStandardParameters(BaseRequest baseRequest)
        {
            base.SetStandardParameters(baseRequest, moduleType);
        }
    }
}
