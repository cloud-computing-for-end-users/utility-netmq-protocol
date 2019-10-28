using custom_message_based_implementation.custom_request.slave_owner_servermodule;
using message_based_communication.model;


namespace custom_message_based_implementation.encoding
{
    public class CustomEncoder : message_based_communication.encoding.Encoding
    {
        protected override BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString)
        {
            switch (specificMethodID)
            {
                case RequestGetSlave.METHOD_ID:
                    return TryDecodeJson<RequestGetSlave>(jsonString);
                case RequestGetListOfRunningApplications.METHOD_ID:
                    return TryDecodeJson<RequestGetListOfRunningApplications>(jsonString);
                case RequestLogin.METHOD_ID:
                    return TryDecodeJson<RequestLogin>(jsonString);
                case RequestCreateAccount.METHOD_ID:
                    return TryDecodeJson<RequestCreateAccount>(jsonString);
                case RequestUploadFile.METHOD_ID:
                    return TryDecodeJson<RequestUploadFile>(jsonString);
                case RequestDownloadFile.METHOD_ID:
                    return TryDecodeJson<RequestDownloadFile>(jsonString);
                case RequestGetListOfFiles.METHOD_ID:
                    return TryDecodeJson<RequestGetListOfFiles>(jsonString);
                default:
                    return null;
            }
        }
    }
}
