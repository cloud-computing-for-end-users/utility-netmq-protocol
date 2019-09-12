using custom_message_based_implementation.model;
using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.custom_requests
{
    public class Handshake : BaseRequest
    {
        public override string SpecificMethodID => consts.MethodID.METHOD_ID_HANDSHAKE;

        public PrimaryKey arg1PrimaryKey { get; set; }
    }
}
