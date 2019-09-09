using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public abstract class BaseRequest : Sendable
    {
        public ModuleType TargetModuleType { get; set; }
        public abstract string SpecificMethodID { get; }

    }
}
