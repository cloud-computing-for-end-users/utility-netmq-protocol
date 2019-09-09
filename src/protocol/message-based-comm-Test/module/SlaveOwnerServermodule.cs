using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManualTeSts
{
    public class SlaveOwnerServermodule : BaseRouterModule
    {

        protected override string MODULE_ID_PREFIXES => "SLAVE_OWNER_MODULE";


        public override string CALL_ID_PREFIX => "SLAVE_OWNER_CALL_";

        public SlaveOwnerServermodule(Port portForRegistrationToRouter, ModuleType moduleType) : base(portForRegistrationToRouter, moduleType)
        {
        }


        public override void HandleRequest(BaseRequest message)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {

            new ServerModuleProxy(base.proxyHelper,this).GetThatThingForSO(HandleTestResponse);    


        }

        public void HandleTestResponse(string response)
        {
            Console.WriteLine("I got the response!!" + response);
        }
    }
}
