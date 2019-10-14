using custom_message_based_implementation.consts;
using custom_message_based_implementation.model;
using message_based_communication.model;

namespace custom_message_based_implementation
{
    public class RequestLogin : BaseRequest
    {
        public const string METHOD_ID = MethodID.METHOD_DATABASE_LOGIN;
        public override string SpecificMethodID => METHOD_ID;

        public Email Email { get; set; }
        public Password Password { get; set; }
    }
}