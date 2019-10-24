using custom_message_based_implementation.consts;
using custom_message_based_implementation.model;
using message_based_communication.model;

namespace custom_message_based_implementation
{
    public class RequestGetListOfFiles : BaseRequest
    {
        public const string METHOD_ID = MethodID.METHOD_FILE_GET_LIST_OF_FILES;
        public override string SpecificMethodID => METHOD_ID;

        public PrimaryKey PrimaryKey { get; set; }
    }
}