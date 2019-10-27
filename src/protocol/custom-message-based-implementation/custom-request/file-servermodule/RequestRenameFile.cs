using custom_message_based_implementation.consts;
using custom_message_based_implementation.model;
using message_based_communication.model;

namespace custom_message_based_implementation
{
    public class RequestRenameFile : BaseRequest
    {
        public const string METHOD_ID = MethodID.METHOD_FILE_RENAME_FILE;
        public override string SpecificMethodID => METHOD_ID;

        public FileName OldFileName { get; set; }
        public FileName NewFileName { get; set; }
        public PrimaryKey PrimaryKey { get; set; }
    }
}