using System;
using System.Collections.Generic;
using net_mq_util;
using NetMQ;
using Newtonsoft.Json;
using protocol.db_methods;
using protocol.Exceptions;
using protocol.file_methods;
using protocol.model;
using protocol.server_methods;
using protocol.slave_owner_methods;
using static net_mq_util.NetMqUtil;
using protocol.methods.slave_owner_methods;
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
            if (0 != message.FrameCount)
            {
                throw new MethodFailedException();
            }
            NetMQFrame frame = message.First;
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

        #region decode methods for File servermodule
        public static FileMethod DecodeFileModuleMethod(NetMQMessage message)
        {
            var firstFrame = message.Pop(); // this strips the method name fram off the message object
            var methodName = GetFromFrameString(firstFrame);

            //validate the target type
            if (false == IsTargetTypeCorrect(methodName, TargetType.FileServermodule))
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
            var firstFrame = message.Pop(); // this strips the method name fram off the message object
            var methodName = GetFromFrameString(firstFrame);

            //validate the target type
            if (false == IsTargetTypeCorrect(methodName, TargetType.SlaveOwnerServermodule))
            {
                throw new WrongTargetException();
            }

            if (GetSlaveMethod.METHOD_NAME.Equals(methodName))
            {
                return DecodeGetSlaveMessage(message);
            }
            else if (GetListOfRunnableApplicationsMethod.METHOD_NAME.Equals(methodName))
            {
                return DecodeGetListOfRunnableApplicationsMessage(message);
            }


            throw new MethodFailedException();
        }

        protected static GetSlaveMethod DecodeGetSlaveMessage(NetMQMessage message)
        {
            var result = new GetSlaveMethod();
            string str = String.Empty;

            str = message.Pop().ConvertToString();
            var obj1 = ConvertToObjectFromJsonString<ApplicationInfo>(str);

            str = message.Pop().ConvertToString();
            var obj2 = ConvertToObjectFromJsonString<PrimaryKey>(str);

            result.ApplicationInfo = obj1;
            result.SlaveBelongsTo = obj2;

            return result;
        }

        protected static GetListOfRunnableApplicationsMethod DecodeGetListOfRunnableApplicationsMessage(NetMQMessage message)
        {
            var result = new GetListOfRunnableApplicationsMethod();
            string str = String.Empty;

            return result;
        }
        #endregion

        #region decode methods for database servermodule
        public static DatabaseMethod DecodeDatabaseModuleMethod(NetMQMessage message)
        {
            var firstFrame = message.Pop();
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
            if (1 != message.FrameCount)
            {
                throw new MethodFailedException("Not the right amount of frames");
            }

            var secondFrame = message.Pop();
            return new HelloWorldMethod(GetFromFrameString(secondFrame));
        }
        #endregion


        #endregion




        #region private helper methods
        private static T ConvertToObjectFromJsonString<T>(string jsonString)
        {
            PrintWhenDebugging("ConvertToObjectFromJsonString - param: " + jsonString);
            var obj = JsonConvert.DeserializeObject(jsonString, typeof(T));
            if (obj is T _obj)
            {
                return _obj;
            }
            else
            {
                throw new MethodFailedException();
            }
        }

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
