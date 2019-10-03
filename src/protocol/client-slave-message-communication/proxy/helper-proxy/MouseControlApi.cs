using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using client_slave_message_communication.model;
using message_based_communication.model;

namespace client_slave_message_communication.proxy.helper_proxy
{
    public class MouseControlApi
    {
        public enum ApiComman
        {
            MoveMouse,
            ClickLeft,
            LeftMouseDown,
            LeftMouseUp,
            ClickRight,
            ClickDouble,
            ScrollDown,
            ScrollUp,
            LocationOfMouse
        }

        protected static Dictionary<ApiComman, string> apiCommandToActualCommand = new Dictionary<ApiComman, string>();
        protected Queue<Action> actionQueue;

        protected Socket outSocket;

        public MouseControlApi(ConnectionInformation whereToConnect = null/*TODO remove default value at some point*/)
        {
            if (null == whereToConnect)
            {
                //use hardcoded values
                whereToConnect = new ConnectionInformation()
                {
                    IP = new IP(){TheIP = "12.152.212.28"}, // NOTICE, this ip will need updating
                    Port = new Port() { ThePort = 6969}
                };
            }
            SetupConnection(whereToConnect);
            actionQueue = new Queue<Action>();

            var t = new Thread(() =>
            {

                while (true)
                {
                    if (0 == actionQueue.Count)
                    {
                        Thread.Sleep(10);
                    }
                    else
                    {
                        actionQueue.Dequeue().Invoke();
                    }
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        private void SetupConnection(ConnectionInformation connInfo)
        {
            this.outSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            outSocket.Connect(new IPEndPoint(IPAddress.Parse(connInfo.IP.TheIP), connInfo.Port.ThePort));
        }

        static MouseControlApi()
        {
            apiCommandToActualCommand.Add(ApiComman.MoveMouse, "-mo");
            apiCommandToActualCommand.Add(ApiComman.ClickLeft, "-cl");
            apiCommandToActualCommand.Add(ApiComman.LeftMouseDown, "-ld");
            apiCommandToActualCommand.Add(ApiComman.LeftMouseUp, "-lu");

        }


        public void MoveMouse(RelativeScreenLocation screenLocation)
        {

            actionQueue.Enqueue(() =>
            {
                SendCommand(ApiComman.MoveMouse, screenLocation.FromLeft.ThePercentage.ToString(), screenLocation.FromTop.ThePercentage.ToString());
            });
        }

        public void ClickLeft()
        {
            actionQueue.Enqueue(() =>{

                SendCommand(ApiComman.ClickLeft/*, arg1,arg2*/);

            });
        }

        public void LeftDown()
        {
            actionQueue.Enqueue(() =>
            {
                SendCommand(ApiComman.LeftMouseDown/*, arg1,arg2*/);
            });
        }

        public void LeftUp()
        {
            actionQueue.Enqueue(() =>
            {
                SendCommand(ApiComman.LeftMouseUp/*, arg1,arg2*/);
            });
        }

        protected void SendCommand(MouseControlApi.ApiComman command, params string[] args)
        {

            var stringToSend = new StringBuilder();
            stringToSend.Append(apiCommandToActualCommand[command]);
            foreach (var item in args)
            {
                stringToSend.Append(" ");
                stringToSend.Append(item);
            }

            var stringAsBytes = Encoding.UTF8.GetBytes(stringToSend.ToString());

            outSocket.Send(BitConverter.GetBytes(stringAsBytes.Length));
            outSocket.Send(stringAsBytes);
        }

    }
}
