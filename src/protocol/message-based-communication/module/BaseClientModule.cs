using System;
using System.Collections.Generic;
using System.Text;
using message_based_communication.model;

namespace message_based_communication.module
{
    public abstract class BaseClientModule : BaseCommunicationModule
    {
        public BaseClientModule(ModuleType moduleType) : base(moduleType)
        {
        }
    }
}
