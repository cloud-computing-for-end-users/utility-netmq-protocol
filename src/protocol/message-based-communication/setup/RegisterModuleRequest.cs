using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.setup
{

    /// <summary>
    /// this is a special request that will not be inheriting from the BaseRequest
    /// </summary>
    public class RegisterModuleRequest 
    {
        public ModuleType ModuleType{ get; set; }
        public ConnectionInformation ConnInfo { get; set; }
        public CallID CallID { get; set; }

    }
}
