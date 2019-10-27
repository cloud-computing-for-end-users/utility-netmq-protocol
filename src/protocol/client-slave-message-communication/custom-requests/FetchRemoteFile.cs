using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class FetchRemoteFile : BaseRequest
    {
        public const string METHOD_ID = consts.MethodID.METHOD_ID_FETCH_REMOTE_FILE;
        public override string SpecificMethodID => METHOD_ID;
        public string FileName { get; set; }
    }
}
