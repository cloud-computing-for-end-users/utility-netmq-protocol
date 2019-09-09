using System;
using System.Collections.Generic;
using System.Text;
using ManualTeSts.request;
using message_based_communication.model;

namespace ManualTeSts.cust_encoding
{
    public class CustomEncoding : message_based_communication.encoding.Encoding
    {
        protected override BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString)
        {
            if (specificMethodID.Equals(GetStuffFromServerModule.METHOD_ID))
            {
                return TryDecodeJson<GetStuffFromServerModule>(jsonString);
            }
            else
            {
                throw new NotImplementedException();
            }

        }
    }
}
