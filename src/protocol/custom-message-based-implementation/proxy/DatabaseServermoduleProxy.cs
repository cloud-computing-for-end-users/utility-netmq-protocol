using System;
using custom_message_based_implementation.consts;
using custom_message_based_implementation.model;
using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using message_based_communication.proxy;

namespace custom_message_based_implementation.proxy
{
    public class DatabaseServermoduleProxy : BaseProxy
    {
        private readonly ModuleType moduleType = new ModuleType() { TypeID = ModuleTypeConst.MODULE_TYPE_DATABASE };

        public DatabaseServermoduleProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule) : base(proxyHelper, baseCommunicationModule) {}

        protected override void SetStandardParameters(BaseRequest baseRequest)
        {
            base.SetStandardParameters(baseRequest, moduleType);
        }

        public void Login(Email email, Password password, Action<PrimaryKey> callBack)
        {
            var request = new RequestLogin{ Email = email, Password = password };

            SetStandardParameters(request);

            base.SendMessage(WrapCallBack<PrimaryKey>(callBack), request);
        }

        public void CreateAccount(Email email, Password password, Action<PrimaryKey> callBack)
        {
            var request = new RequestCreateAccount{ Email = email, Password = password };

            SetStandardParameters(request);

            base.SendMessage(WrapCallBack<PrimaryKey>(callBack), request);
        }
    }
}