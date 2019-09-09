using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace custom_message_based_implementation.model
{
    public class SlaveConnection : ConnectionInformation
    {
        public PrimaryKey OwnerPrimaryKey { get; set; }
        public SlaveID SlaveID { get; set; }


    }
}
