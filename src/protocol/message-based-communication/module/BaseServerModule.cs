using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.module
{
    public abstract class BaseServerModule : BaseCommunicationModule
    {
        public BaseServerModule(ModuleType moduleType) : base(moduleType)
        {
        }

        protected virtual void SendResponse(Response response)
        {
            this.proxyHelper.SendResponse(response);
        }


        protected Response GenerateResponseBasedOnRequestAndPayload(BaseRequest baseRequest, object responsePayload)
        {
            return new Response()
            {
                CallID = baseRequest.CallID,
                SenderModuleID = this.proxyHelper.ModuleID,
                TargetModuleID = baseRequest.SenderModuleID,
                Payload = new Payload() { ThePayload = responsePayload }
            };
        }

        public abstract void HandleRequest(BaseRequest message);

    }
}
