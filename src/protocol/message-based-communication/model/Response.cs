using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public sealed class Response : Sendable
    {
        public ModuleID TargetModuleID { get; set; }
        public Payload Payload { get; set; }
    }
}
