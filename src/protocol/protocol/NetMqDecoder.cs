using System;
using System.Collections.Generic;
using System.Text;
using net_mq_util;
using NetMQ;
using protocol.db_methods;
using protocol.Exceptions;
using protocol.file_methods;
using protocol.server_methods;
using protocol.slave_owner_methods;
using static net_mq_util.NetMqUtil;
using static net_mq_util.ProtocolConstants;

namespace net_mq_decoder
{
    public class NetMqDecoder
    {
        private static Dictionary<string, TargetType> STRING_TO_TARGET_TYPE = null;

        static NetMqDecoder()
        {
            STRING_TO_TARGET_TYPE = GetMethodToTargetTypeClone();
        }


        #region decoder methods
        /// <summary>
        /// the frame passed to this method must be the first frame of a message
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static NetMqUtil.TargetType GetTargetType(NetMQMessage message)
        {
            var frame = GetFirstFrame(message);

            string frameContent = frame.ConvertToString();

            if (STRING_TO_TARGET_TYPE.ContainsKey(frameContent))
            {
                return STRING_TO_TARGET_TYPE[frameContent];
            }
            else
            {
                throw new MethodFailedException();
            }
        }

        protected static NetMQFrame GetFirstFrame(NetMQMessage message)
        {
            if(0 < message.FrameCount)
            {
                return message.First;
            }
            else
            {
                throw new MethodFailedException();
            }


        }


        #region decode methods for File servermodule
        public static FileMethod DecodeFileModuleMethod(NetMQMessage message)
        {
            var firstFrame = GetFirstFrame(message);
            var methodName = GetFromFrameString(firstFrame);

            //validate the target type
            if(false == IsTargetTypeCorrect(methodName, TargetType.FileServermodule))
            {
                throw new WrongTargetException(); 
            }

            //if (HelloWorldMethod.METHOD_NAME.Equals(methodName))
            //{
            //    return DecodeServerMethodHelloWorld(message);
            //}


            throw new MethodFailedException();
        }
        #endregion

        #region decode methods for Slave owner servermodule
        public static SlaveOwnerMethod DecodeSlaveOwnerModuleMethod(NetMQMessage message)
        {
            var firstFrame = GetFirstFrame(message);
            var methodName = GetFromFrameString(firstFrame);

            //validate the target type
            if (false == IsTargetTypeCorrect(methodName, TargetType.SlaveServermodule))
            {
                throw new WrongTargetException();
            }

            //if (HelloWorldMethod.METHOD_NAME.Equals(methodName))
            //{
            //    return DecodeServerMethodHelloWorld(message);
            //}


            throw new MethodFailedException();
        }
        #endregion

        #region decode methods for database servermodule
        public static DatabaseMethod DecodeDatabaseModuleMethod(NetMQMessage message)
        {
            var firstFrame = GetFirstFrame(message);
            var methodName = GetFromFrameString(firstFrame);

            //validate the target type
            if (false == IsTargetTypeCorrect(methodName, TargetType.DatabaseServermodule))
            {
                throw new WrongTargetException();
            }

            //if (HelloWorldMethod.METHOD_NAME.Equals(methodName))
            //{
            //    return DecodeServerMethodHelloWorld(message);
            //}


            throw new MethodFailedException();
        }
        #endregion

        #region decode methods for server module
        public static ServerMethod DecodeServerModuleMethod(NetMQMessage message)
        {

            var firstFrame = message.Pop();
            var methodName = GetFromFrameString(firstFrame);

            //validate the target type
            if (false == IsTargetTypeCorrect(methodName, TargetType.ServerModule))
            {
                throw new WrongTargetException();
            }

            //the first frame have already been "Pop'ed" off and the message will therefore have one frame less than you would normally expect
            if (HelloWorldMethod.METHOD_NAME.Equals(methodName))
            {
                return DecodeServerMethodHelloWorld(message);
            }
            else
            {

            }





            throw new MethodFailedException();
        }

        protected static HelloWorldMethod DecodeServerMethodHelloWorld(NetMQMessage message)
        {
            if(1 != message.FrameCount)
            {
                throw new MethodFailedException("Not the right amount of frames");
            }

            var secondFrame = message.Pop();
            return new HelloWorldMethod(GetFromFrameString(secondFrame));
        }
        #endregion


        #endregion




        #region private helper methods

        private static bool IsTargetTypeCorrect(string methodName, TargetType expected)
        {
            if (STRING_TO_TARGET_TYPE.ContainsKey(methodName))
            {
                return expected.Equals(STRING_TO_TARGET_TYPE[methodName]);
            }

            return false;
        }

        private static string GetFromFrameString(NetMQFrame frame)
        {
            return frame.ConvertToString();

        }
        private static int GetFromFrameInt(NetMQFrame frame)
        {
            return frame.ConvertToInt32();
        }
        private static byte[] GetFromFrameByteArray(NetMQFrame frame)
        {
            return frame.ToByteArray();
        }



        #endregion
    }
}
