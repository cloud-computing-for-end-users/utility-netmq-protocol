using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace custom_message_based_implementation.model
{
    public class SlaveConnection
    {
        public ConnectionInformation ConnectionInformation { get; set; }
        public PrimaryKey OwnerPrimaryKey { get; set; }
        public SlaveID SlaveID { get; set; }
        public Port RegistrationPort { get; set; }
        public Port ConnectToRecieveImagesPort { get; set; }



    }
}
