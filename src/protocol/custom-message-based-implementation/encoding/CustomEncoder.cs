using System;
using System.Collections.Generic;
using custom_message_based_implementation.custom_request.slave_owner_servermodule;
using message_based_communication.model;


namespace custom_message_based_implementation.encoding
{
    public class CustomEncoder : message_based_communication.encoding.Encoding
    {
        protected override BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString)
        {
            if (RequestGetSlave.METHOD_ID.Equals(specificMethodID))
            {
                return TryDecodeJson<RequestGetSlave>(jsonString);
            }
            else
            {
                return null;
            }
        }
    }
}
