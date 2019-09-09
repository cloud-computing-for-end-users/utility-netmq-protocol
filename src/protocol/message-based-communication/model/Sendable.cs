using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public abstract class Sendable
    {
        public CallID CallID { get; set; }
        public ModuleID SenderModuleID { get; set; }

    }
}
