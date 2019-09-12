using client_slave_message_communication.model.keyboard_action;
using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.custom_requests
{
    public class DoKeyboardAction : BaseRequest
    {
        public override string SpecificMethodID => consts.MethodID.METHOD_ID_DO_KEYBOARD_ACTION;
        public BaseKeyboardAction arg1KeyboardAction{ get; set; }
    }
}
