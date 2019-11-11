using System;
using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class DoKeyboardAction : BaseRequest
    {
        public const string METHOD_ID = consts.MethodID.METHOD_ID_DO_KEYBOARD_ACTION;
        public override string SpecificMethodID => METHOD_ID;

        public String Key { get; set; }
        public bool IsKeyDownAction { get; set; }
    }
}
