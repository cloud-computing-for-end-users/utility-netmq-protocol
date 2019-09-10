using ManualTeSts.request;
using message_based_communication.encoding;
using message_based_communication.model;
using message_based_communication.module;
using System;

namespace ManualTeSts
{
    public class ServerModule : BaseRouterModule
    {
        public ServerModule(Port portForRegistrationToRouter, ModuleType moduleType,Encoding customEncoding) : base(portForRegistrationToRouter, moduleType,customEncoding)
        {
        }

        protected override string MODULE_ID_PREFIXES => "SERVER_MODULE_ID_";

        public override string CALL_ID_PREFIX => "SERVER_MODULE_CALL_";




        public override void HandleRequest(BaseRequest message)
        {
            object payload = null;
            if(message is GetStuffFromServerModule _mess)
            {
                payload = HandleGetStuffFromServerModule(_mess);
                
            }
            else
            {
                throw new NotImplementedException();

            }
            var response = GenerateResponseBasedOnRequestAndPayload(message, payload);
            SendResponse(response);
        }

        private static string HandleGetStuffFromServerModule(GetStuffFromServerModule message)
        {
            return "This is the stuff m8";
        }
    }
}
