using custom_message_based_implementation.model;
using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace custom_message_based_implementation.custom_request.slave_owner_servermodule
{
    public class RequestGetSlave : BaseRequest
    {
        public const string METHOD_ID = "SO_REQ_GET_SLAVE";
        public override string SpecificMethodID => METHOD_ID;

        public PrimaryKey PrimaryKey { get; set; }
        public ApplicationInfo AppInfo{ get; set; }

    }
}
