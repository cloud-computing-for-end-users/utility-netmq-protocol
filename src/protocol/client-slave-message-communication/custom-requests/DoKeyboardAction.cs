using client_slave_message_communication.model.keyboard_action;
using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class DoKeyboardAction : BaseRequest
    {
        public const string METHOD_ID = consts.MethodID.METHOD_ID_DO_KEYBOARD_ACTION;
        public override string SpecificMethodID => METHOD_ID;
        public BaseKeyboardAction arg1KeyboardAction{ get; set; }
    }
}
