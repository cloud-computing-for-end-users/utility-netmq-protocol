using custom_message_based_implementation.consts;
using message_based_communication.model;

namespace custom_message_based_implementation.custom_request.slave_owner_servermodule
{
    public class RequestGetListOfRunningApplications : BaseRequest
    {
        public override string SpecificMethodID => METHOD_ID;
        public const string METHOD_ID = MethodID.METHOD_SLAVE_OWNER_GET_LIST_OF_RUNNING_APP;
    }
}
