using custom_message_based_implementation.consts;
using custom_message_based_implementation.model;
using message_based_communication.model;

namespace custom_message_based_implementation
{
    public class RequestDownloadFile : BaseRequest
    {
        public const string METHOD_ID = MethodID.METHOD_FILE_DOWNLOAD_FILE;
        public override string SpecificMethodID => METHOD_ID;

        public FileName FileName { get; set; }
        public PrimaryKey PrimaryKey { get; set; }
    }
}