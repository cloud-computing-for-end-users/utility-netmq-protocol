using NetMQ;
using System;
using System.Collections.Generic;
using System.Text;
using static net_mq_util.ProtocolConstants;


namespace net_mq_util
{
    public class NetMqUtil
    {
        //public constants for accessible to all in the system
        public const int SERVER_MODULE_PORT = 9825;
        public const string SERVER_MODULE_IP = "127.0.0.1";



        private static readonly bool DEBUG_MODE = true;
        public static void PrintWhenDebugging(string str)
        {
            if (DEBUG_MODE)
            {
                Console.WriteLine(str);
            }
        }

        //all methods that can be called in this protocol

        //any message set using net_mq must contain atleast two frames, and the first frame must always be of type TargetType


        /// <summary>
        /// when updating this enum be sure to also add it to the dictionaries "TartgetTypeToString" and "StringToTargetType" in the static constructor
        /// </summary>
        public enum TargetType
        {
            FileServermodule,
            SlaveOwnerServermodule,
            DatabaseServermodule,
            ServerModule
        }

        protected static Dictionary<string, TargetType> MethodToTargetType = new Dictionary<string, TargetType>();

        static NetMqUtil()
        {
            //for code folding, defining methods and their TargetType to dicionarie
            //every mothod added here must be implemented in the NetMqEncoder and Decoder
            if (true)
            {
                //--------------methods for Server module--------------
                MethodToTargetType.Add(MET_SM_HelloWorld, TargetType.ServerModule);
                MethodToTargetType.Add(MET_SM_REG_DB_SM, TargetType.ServerModule);
                MethodToTargetType.Add(MET_SM_REG_FILE_SM, TargetType.ServerModule);
                MethodToTargetType.Add(MET_SM_REG_SO_SM, TargetType.ServerModule);


                //--------------methods for Slave Owner module--------------
                MethodToTargetType.Add(MET_SO_GET_SLAVE, TargetType.SlaveOwnerServermodule);
                MethodToTargetType.Add(MET_SO_GET_LIST_OF_RUNNABLE_APPLICATIONS, TargetType.SlaveOwnerServermodule);


                //--------------methods for File Server module--------------



                //--------------methods for Database Server module--------------







            }
        }

        public static NetMQMessage CloneShallowNetMqMessage(NetMQMessage message)
        {
            List<NetMQFrame> messageFrames = new List<NetMQFrame>(); //deconstructing message to access the frame with the target type
            while (false == message.IsEmpty)
            {
                messageFrames.Add(message.Pop());
            }

            foreach(var frame in messageFrames)
            {
                message.Append(frame);
            }

            return new NetMQMessage(messageFrames);
        }
        public static Dictionary<string, TargetType> GetMethodToTargetTypeClone()
        {
            return new Dictionary<string, TargetType>(MethodToTargetType);
        }

    }
}
