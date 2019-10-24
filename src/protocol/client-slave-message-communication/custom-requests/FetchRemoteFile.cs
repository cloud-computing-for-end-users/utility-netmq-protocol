using System;
using System.Collections.Generic;
using System.Text;
using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class FetchRemoteFile : BaseRequest
    {
        public override string SpecificMethodID => consts.MethodID.METHOD_ID_FETCH_REMOTE_FILE;

        public string FileName { get; set; }
    }
}
