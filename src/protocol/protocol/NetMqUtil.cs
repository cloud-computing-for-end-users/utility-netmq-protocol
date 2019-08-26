using System;
using System.Collections.Generic;
using System.Text;
using static net_mq_util.ProtocolConstants;


namespace net_mq_util
{
    public class NetMqUtil
    {

        //all methods that can be called in this protocol

        //any message set using net_mq must contain atleast two frames, and the first frame must always be of type TargetType


        /// <summary>
        /// when updating this enum be sure to also add it to the dictionaries "TartgetTypeToString" and "StringToTargetType" in the static constructor
        /// </summary>
        public enum TargetType
        {
            FileServermodule,
            SlaveServermodule,
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
                MethodToTargetType.Add(MET_SM_HelloWorld, TargetType.ServerModule);

            }
        }

       
        public static Dictionary<string, TargetType> GetMethodToTargetTypeClone()
        {
            return new Dictionary<string, TargetType>(MethodToTargetType);
        }

    }
}
