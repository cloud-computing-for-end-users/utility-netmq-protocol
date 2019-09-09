using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class AcknowledgeRecivedSendable : Sendable
    {
        public ModuleID TargetModuleID { get; set; }
    }
}
