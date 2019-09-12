using System;
using System.Collections.Generic;
using System.Text;
using message_based_communication.model;

namespace client_slave_message_communication.custom_encoding
{
    public class CustomEncoding : message_based_communication.encoding.Encoding
    {
        protected override BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString)
        {
            throw new NotImplementedException();
        }
    }
}
