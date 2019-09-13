using client_slave_message_communication.model;
using client_slave_message_communication.model.mouse_action;
using client_slave_message_communication.proxy;
using message_based_communication.model;
using message_based_communication.module;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManualTesTSlaveComm
{
    public class GenericClientModule : BaseClientModule
    {
        public GenericClientModule(ModuleType moduleType) : base(moduleType)
        {
        }

        public override string CALL_ID_PREFIX => "GENERIC_CLIENT_CALL_ID_";


        public void MoveMouse(RelativeScreenLocation screenLocation)
        {
            new SlaveProxy(base.proxyHelper, this).DoMouseAction(CallBack,new MouseMoveAction() { arg1RelativeScreenLocation = screenLocation });
        }

        public void ClickLeft(RelativeScreenLocation screenLocation)
        {
            new SlaveProxy(base.proxyHelper, this).DoMouseAction(CallBack, new ClickLeftAction() { arg1ScreenLocation = screenLocation });

        }

        private void CallBack()
        {
            Console.WriteLine("Got a callback");
        }
    }
}
