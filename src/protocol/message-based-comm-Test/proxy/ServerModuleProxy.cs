using message_based_communication.connection;
using message_based_communication.module;
using message_based_communication.proxy;
using System;
using ManualTeSts.request;
using message_based_communication.model;
using ManualTeSts.consts;

namespace ManualTeSts
{
    public class ServerModuleProxy : BaseProxy
    {
        public ServerModuleProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule) : base(proxyHelper, baseCommunicationModule)
        {
        }


        public void GetThatThingForSO(Action<string> callBack)
        {
            base.SendMessage(
                res => {
                    callBack.Invoke(res.Payload.ThePayload as string);
                },
                new GetStuffFromServerModule()
                {

                    CallID = base.GenerateAndReserverCallID(),
                    SenderModuleID = base.baseCommunicationModule.ModuleID,
                    TargetModuleType = new ModuleType() { TypeID = ModuleTypeConsts.ServerModuleType},
                    WhatIWant = "I want that thing you know"
                });

        }
    }
}
