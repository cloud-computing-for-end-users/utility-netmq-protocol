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

            Action<Response> handleResponse = res =>
            {
                callBack.Invoke(res.Payload.ThePayload as SlaveConnection);
            };


            var request = new RequestGetSlave();
            
            request.PrimaryKey = arg1;
            request.AppInfo = arg2;
            SetStandardParameters(request);

            base.SendMessage(WrapCallBack<SlaveConnection>(callBack), request);
        }

        public void GetListOfRunningApplications(Action<List<ApplicationInfo>> callBack)
        {

        }

        protected static Action<Response> WrapCallBack<T>(Action<T> callBack) where T : class
        {
            return 
                (response) =>
            {
                callBack.Invoke(response.Payload.ThePayload as T);
            };
        }










        private readonly ModuleType moduleType = new ModuleType() { TypeID = ModuleTypeConst.MODULE_TYPE_SLAVE_OWNER };
        protected override void SetStandardParameters(BaseRequest baseRequest)
        {
            base.SetStandardParameters(baseRequest, moduleType);
        }



    }
}
