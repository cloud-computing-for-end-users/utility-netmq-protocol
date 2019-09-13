using client_slave_message_communication.model.mouse_action;
using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.custom_requests
{
    public class DoMouseAction<T> : BaseRequest where T : BaseMouseAction
    {
        public override string SpecificMethodID => consts.MethodID.METHOD_ID_DO_MOUSE_ACTION;
        public T  arg1MouseAction{ get; set; }

    }
}
