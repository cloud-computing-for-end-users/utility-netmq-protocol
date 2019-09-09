using System;
using System.Collections.Generic;
using System.IO;
using net_mq_util;
using NetMQ;
using Newtonsoft.Json;
using protocol.db_methods;
using protocol.Exceptions;
using protocol.file_methods;
using protocol.methods.server_methods;
using protocol.methods.slave_owner_methods;
using protocol.model;
using protocol.server_methods;
using protocol.slave_owner_methods;
using static net_mq_util.NetMqUtil;
using static net_mq_util.ProtocolConstants;

namespace net_mq_encoder
{
    public class NetMqEncoder
    {
        private static Dictionary<string, TargetType> methodToTargetType = null;

        static NetMqEncoder()
        {
            methodToTargetType = GetMethodToTargetTypeClone();

        }


        #region implementation of all generate method messages

        protected static NetMQMessage GenerateBaseMessage(int serverModuleID, int callID)
        {
            NetMQMessage message = new NetMQMessage();
            message.Append(GetFrame(serverModuleID));
            message.Append(GetFrame(callID));
            return message;
        }

        #region encode methods for File servermodule
        public static NetMQMessage GenerateFileModuleMethodMessage(int servermoduleID, int callID, FileMethod method)
        {
            var baseMessage = GenerateBaseMessage(servermoduleID, callID);
            //if (method is HelloWorldMethod _method)
            //{
            //    return GenerateHelloWorldMessage(_method);
            //}
            throw new MethodFailedException();
        }
        #endregion





        #region encode methods for Slave owner servermodule
        public static NetMQMessage GenerateSlaveOwnerModuleMethodMessage(int servermoduleID, int callID, SlaveOwnerMethod method)
        {
            var baseMessage = GenerateBaseMessage(servermoduleID, callID);

            if (method is GetSlaveMethod)
            {
                return GenerateGetSlaveMessage(baseMessage, (GetSlaveMethod)method);

            }else if (method is GetListOfRunnableApplicationsMethod )
            {
                return GenerateGetListOfRunnableApplicationsMessage(baseMessage, (GetListOfRunnableApplicationsMethod)method);
            }

            throw new MethodFailedException();
        }
        protected static NetMQMessage GenerateGetSlaveMessage(NetMQMessage message, GetSlaveMethod method)
        {
            //AppendMethodIdFrame(message, method.MethodId);

            string json = String.Empty;

            message.Append(GetFrame(GetSlaveMethod.METHOD_NAME));

            json = ConvertToJson(method.ApplicationInfo);
            message.Append(GetFrame(json));

            json = ConvertToJson(method.SlaveBelongsTo);
            message.Append(json);

            return message;

        }

        protected static NetMQMessage GenerateGetListOfRunnableApplicationsMessage(NetMQMessage message, GetListOfRunnableApplicationsMethod method)
        {
            //AppendMethodIdFrame(message, method.MethodId);
            string json = String.Empty;

            message.Append(GetFrame(GetListOfRunnableApplicationsMethod.METHOD_NAME));

            return message;

        }
        #endregion




        #region encode methods for database servermodule
        public static NetMQMessage GenerateDatabaseModuleMethodMessage(int servermoduleID, int callID, DatabaseMethod method)
        {
            var baseMessage = GenerateBaseMessage(servermoduleID, callID);

            //if (method is HelloWorldMethod _method)
            //{
            //    return GenerateHelloWorldMessage(_method);
            //}
            throw new MethodFailedException();
        }



        #endregion



        #region encode methods for server module
        public static NetMQMessage GenerateServerModuleMethodMessage(ServermoduleID servermoduleID, CallID callID, ServerMethod method)
        {
            var baseMessage = GenerateBaseMessage(servermoduleID.ID, callID.ID);

            if (method is HelloWorldMethod)
            {
                return GenerateHelloWorldMessage(baseMessage, (HelloWorldMethod)method);
            }else if (method is RegisterServermoduleMethod)
            {
                return GenerateRegisterServermoduleMessage(baseMessage, (RegisterSlaveOwnerServermoduleMethod)method);
            }
            

            throw new MethodFailedException();
        }


        //most basic method with no real purpose
        protected static NetMQMessage GenerateHelloWorldMessage(NetMQMessage message, HelloWorldMethod method)
        {
            message.Append(GetFrame(HelloWorldMethod.METHOD_NAME));
            //AppendMethodIdFrame(message, method.MethodId);

            message.Append(GetFrame(method.param1));

            return message;
        }
        protected static NetMQMessage GenerateRegisterServermoduleMessage(NetMQMessage message, RegisterServermoduleMethod method)
        {

            message.Append(GetFrame(RegisterServermoduleMethod.METHOD_NAME));
            message.Append(ConvertToJson(method.TargetType));
            message.Append(ConvertToJson(method.ConnectionInfo));

            return message;
        }
        

        #endregion

        #region encode response

        public static NetMQMessage GenerateResponse(ServermoduleID servermoduleID,CallID callID, object obj)
        {
            var message = GenerateBaseMessage(servermoduleID.ID, callID.ID);

            if(null == obj)
            {
                message.Append(GetFrame(ProtocolConstants.RESPONDE_NULL));
            }
            else
            {
                var jsonString = ConvertToJson(obj);
                message.Append(GetFrame(jsonString));
            }

            return message;
        }
        //public static NetMQFrame GenerateResponseFrame(object obj)
        //{
        //    var jsonString = ConvertToJson(obj);
        //    return GetFrame(jsonString);
        //}
        #endregion

        #endregion


        #region private helper methods
        private static string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }

        private static NetMQFrame GetFrame(byte[] data)
        {
            return new NetMQFrame(data);
        }

        private static NetMQFrame GetFrame(string data)
        {
            return new NetMQFrame(data);
        }
        private static NetMQFrame GetFrame(int data)
        {
            return new NetMQFrame(data.ToString());
        }

        private static NetMQMessage AppendMethodIdFrame(NetMQMessage message, string methodId)
        {
            //TODO NOTICE THAT METHOD ID, should probably not really be used for anythin ---------------------------------------------------
            message.Append(GetFrame(methodId));
            return message;
        }

        /// <summary>
        /// return the the filename as the first item and the file data as the second item.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private static Tuple<NetMQFrame, NetMQFrame> GetFrames(FileInfo fileInfo)
        {
            if (fileInfo.Exists)
            {
                var fileNameFrame = new NetMQFrame(fileInfo.Name);
                var fileData = File.ReadAllBytes(fileInfo.FullName);
                var fileDataFrame = new NetMQFrame(fileData);
                return new Tuple<NetMQFrame, NetMQFrame>(fileNameFrame, fileDataFrame);
            }
            else
            {
                throw new MethodFailedException();
            }

        }

        #endregion


    }


}
