using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class SaveFilesAndTerminate : BaseRequest
    {
        public const string METHOD_ID = consts.MethodID.METHOD_ID_SAVE_FILES_AND_TERMINATE;
        public override string SpecificMethodID => METHOD_ID;
    }
}
