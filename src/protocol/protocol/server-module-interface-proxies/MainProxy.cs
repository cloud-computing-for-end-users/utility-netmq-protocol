using NetMQ.Sockets;
using NetMQ;
using protocol.methods;
using System;
using System.Collections.Generic;
using System.Text;
using protocol.Exceptions;
using net_mq_util;

namespace protocol.server_module_interface_proxies
{
    public class MainProxy
    {
        private RequestSocket reqSocket;
        private HashSet<int> callIdInUse;


        private static Random random = new Random();
        public static MainProxy INSTANCE = new MainProxy();


        private MainProxy()
        {
            callIdInUse = new HashSet<int>();
            reqSocket = new RequestSocket();



            //bind and connect sockets

            reqSocket.Connect("tcp://" + NetMqUtil.SERVER_MODULE_IP + ":" + NetMqUtil.SERVER_MODULE_PORT);
        }


        /// <summary>
        /// the int returned is the callID
        /// </summary>
        /// <returns></returns>
        public NetMQMessage CallMethod(NetMQMessage mqMessage)
        {
            lock (reqSocket)
            {

                var callID = GenerateCallID();

                var messageToSend = new NetMQMessage();
                messageToSend.Append(callID);
                while (false == mqMessage.IsEmpty)
                {
                    messageToSend.Append(mqMessage.Pop());
                }

                reqSocket.SendMultipartMessage(messageToSend);
                var message = reqSocket.ReceiveMultipartMessage();
                var callIdFrame = message.Pop();
                int recivedCallId = callIdFrame.ConvertToInt32();

                if (recivedCallId.Equals(callID))
                {
                    //everything should be just fine
                    return message;
                }
                callIdInUse.Remove(callID);
            }

            throw new MethodFailedException();
            //return callID;
        }


        protected int GenerateCallID()
        {
            int anInt = -1;

            lock (callIdInUse)
            {
                do
                {
                    anInt = random.Next();

                } while (callIdInUse.Contains(anInt));
                callIdInUse.Add(anInt);
            }
            return anInt;
        }

    }
}
