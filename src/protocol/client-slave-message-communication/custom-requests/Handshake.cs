using custom_message_based_implementation.model;
using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class Handshake : BaseRequest
    {
        public const string METHOD_ID = consts.MethodID.METHOD_ID_HANDSHAKE;
        public override string SpecificMethodID => METHOD_ID;
        public PrimaryKey arg1PrimaryKey { get; set; }
    }
}
