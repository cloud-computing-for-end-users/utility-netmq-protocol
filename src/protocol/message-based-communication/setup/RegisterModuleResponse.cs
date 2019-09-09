using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.setup
{
    public class RegisterModuleResponse
    {
        public CallID CallID { get; set; }
        public ModuleID ModuleID{ get; set; }
    }
}
