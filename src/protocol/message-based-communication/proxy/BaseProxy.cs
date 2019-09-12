using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.proxy
{
    public abstract class BaseProxy
    {
        protected ProxyHelper proxyHelper;
        protected BaseCommunicationModule baseCommunicationModule;
        protected Dictionary<string, Action<Response>> callIDToResponseHandler;

        public BaseProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule)
        {
            this.proxyHelper = proxyHelper;
            this.baseCommunicationModule = baseCommunicationModule;
            this.callIDToResponseHandler = new Dictionary<string, Action<Response>>();
        }

        public void HandleResponse(Response response)
        {
            this.callIDToResponseHandler[response.CallID.ID].Invoke(response);
        }



        protected CallID GenerateAndReserverCallID()
        {
            string callID = String.Empty;
            do
            {
                callID = baseCommunicationModule.CALL_ID_PREFIX + new Random().Next();
            } while (callIDToResponseHandler.ContainsKey(callID));

            callIDToResponseHandler.Add(callID, null);
            return new CallID() { ID = callID };
        }

        /// <summary>
        /// should be implemented to call the base.SendMessage that takes more parameters as some of these will always be the same for a given base class
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="payload"></param>
        protected void SendMessage(Action<Response> callBack, BaseRequest baseRequest)
        {
            this.callIDToResponseHandler[baseRequest.CallID.ID] = callBack;
            this.proxyHelper.SendMessage(baseRequest, this);
        }

        //should be implemented and call  SetStandardParameters(BaseRequest baseRequest, ModuleType moduleType)
        protected abstract void SetStandardParameters(BaseRequest baseRequest);

        protected void SetStandardParameters(BaseRequest baseRequest, ModuleType targetModuleType)
        {
            baseRequest.CallID = GenerateAndReserverCallID();
            baseRequest.SenderModuleID = baseCommunicationModule.ModuleID;
            baseRequest.TargetModuleType = targetModuleType;
        }

        protected static Action<Response> WrapCallBack<T>(Action<T> callBack) where T : class
        {
            return
                (response) =>
                {
                    if (response.Payload.ThePayload is JArray _jArray) // this is a "hack" because Newtonsoft can't deserialise to a list in the way the payload is made right now
                    {
                        response.Payload.ThePayload = _jArray.ToObject<T>();
                    }else if(response.Payload.ThePayload is JObject jObject)
                    {
                        response.Payload.ThePayload = jObject.ToObject<T>();
                    }

                    T obj = response.Payload.ThePayload as T;

                    if(null == obj)
                    {
                        throw new Exception("Exception in the wrappedCallBack<T>(), Response: " + response + ", response type: " + response.GetType() + ", typeof(T):" + typeof(T));
                    }
                    callBack.Invoke(obj);
                };
        }
    }
}
