using client_slave_message_communication.custom_requests;
using client_slave_message_communication.model.mouse_action;
using custom_message_based_implementation.model;
using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using message_based_communication.proxy;
using System;

namespace client_slave_message_communication.proxy
{
    public class SlaveProxy : BaseProxy
    {
        public SlaveProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule) : base(proxyHelper, baseCommunicationModule)
        {
        }

        /// <summary>
        /// item 1 of the tuple is the width of the application
        /// item 2 is the height of the application
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="pk"></param>
        public void Handshake(Action<Tuple<int,int>> callBack, PrimaryKey pk)
        {
            var request = new Handshake() { arg1PrimaryKey = pk };
            SetStandardParameters(request);

            SendMessage(WrapCallBack<Tuple<int,int>>(callBack), request);
        }
        [Obsolete("this information is not gained from the slave owner when calling the GetSlave method")]
        public void GetImageProducerConnectionInformation(Action<Port> callBack)
        {
            var request = new GetImageProducerConnectionInfo();
            SetStandardParameters(request);

            SendMessage(WrapCallBack<Port>(callBack), request);
        }

        /// <summary>
        /// the callback for this method will never be invoked
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="action"></param>
        public void DoMouseAction(Action callBack, BaseMouseAction action)
        {
            var request = new DoMouseAction<BaseMouseAction>() { arg1MouseAction = action };
            SetStandardParameters(request);

            SendMessage(WrapNoParamAction(callBack), request);
        }

        // tell the Slave to fetch a file from the file server
        public void FetchRemoteFile(Action callBack, string fileName)
        {
            var request = new FetchRemoteFile() { FileName = fileName};
            SetStandardParameters(request);

            SendMessage(WrapNoParamAction(callBack), request);
        }

        // the slave will save files to file servermodule and then terminate
        public void SaveFilesAndTerminate(Action callBack)
        {
            var request = new SaveFilesAndTerminate();
            SetStandardParameters(request);

            SendMessage(WrapNoParamAction(callBack), request);
        }

        public void DoKeyboardAction(Action callback, String key, bool isKeyDownAction)
        {
            var request = new DoKeyboardAction();
            request.IsKeyDownAction = isKeyDownAction;
            request.Key = key;

            SetStandardParameters(request);

            SendMessage(WrapNoParamAction(callback), request);
        }

        private ModuleType moduleType = new ModuleType() { TypeID = custom_message_based_implementation.consts.ModuleTypeConst.MODULE_TYPE_SLAVE };
        protected override void SetStandardParameters(BaseRequest baseRequest)
        {
            base.SetStandardParameters(baseRequest, moduleType);
        }



    }
}
