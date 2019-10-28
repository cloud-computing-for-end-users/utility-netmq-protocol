using custom_message_based_implementation.model;
using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class GetImageProducerConnectionInfo : BaseRequest
    {
        public const string METHOD_ID = consts.MethodID.METHOD_ID_GET_IMAGE_PRODUCER_CONN;
        public override string SpecificMethodID => METHOD_ID;
    }
}
