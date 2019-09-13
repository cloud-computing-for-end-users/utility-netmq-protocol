using client_slave_message_communication.custom_requests;
using client_slave_message_communication.model.keyboard_action;
using client_slave_message_communication.model.mouse_action;
using custom_message_based_implementation.model;
using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using message_based_communication.proxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace client_slave_message_communication.proxy
{
    public class SlaveProxy : BaseProxy
    {
        public SlaveProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule) : base(proxyHelper, baseCommunicationModule)
        {
        }

        /// <summary>
        /// gives back the port that the image producer can be contacted on
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="pk"></param>
        public void Handshake(Action<Port> callBack, PrimaryKey pk)
        {
            var request = new Handshake() { arg1PrimaryKey = pk };
            SetStandardParameters(request);

            SendMessage(WrapCallBack<Port>(callBack), request);
        }
        public void GetImageProducerConnectionInformation(Action callBack)
        {
            var request = new GetImageProducerConnectionInfo();
            SetStandardParameters(request);

            SendMessage(WrapNoParamAction(callBack), request);
        }
        public void DoKeyboardAction(Action callBack, BaseKeyboardAction action)
        {
            var request = new DoKeyboardAction() { arg1KeyboardAction = action };
            SetStandardParameters(request);

            SendMessage(WrapNoParamAction(callBack), request);
        }

        public void DoMouseAction(Action callBack, BaseMouseAction action)
        {
            var request = new DoMouseAction<BaseMouseAction>() { arg1MouseAction = action };
            SetStandardParameters(request);

            SendMessage(WrapNoParamAction(callBack), request);
        }


        private Action<Response> WrapNoParamAction(Action action)
        {
            return response =>
            {
                action.Invoke();
            };
        }



        private ModuleType moduleType = new ModuleType() { TypeID = custom_message_based_implementation.consts.ModuleTypeConst.MODULE_TYPE_SLAVE };
        protected override void SetStandardParameters(BaseRequest baseRequest)
        {
            base.SetStandardParameters(baseRequest, moduleType);
        }


    }
}
