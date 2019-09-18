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



        private void getPortCallBack(out object obj, Port port)
        {
            obj = port;
        }

        public Port GetPort()
        {
            object result = null;

            Action<Port> callBack = port =>
            {
                getPortCallBack(out result, port);

            };
            new SlaveProxy(base.proxyHelper, this).GetImageProducerConnectionInformation(callBack);


            while (null == result)
            {
                
            }
            return result as Port;
        }

    }
}
