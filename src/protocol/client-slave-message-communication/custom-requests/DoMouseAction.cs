using client_slave_message_communication.model.mouse_action;
using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class DoMouseAction<T> : BaseRequest where T : BaseMouseAction
    {
        public const string METHOD_ID = consts.MethodID.METHOD_ID_DO_MOUSE_ACTION;
        public override string SpecificMethodID => METHOD_ID;
        public T  arg1MouseAction{ get; set; }

    }
}
