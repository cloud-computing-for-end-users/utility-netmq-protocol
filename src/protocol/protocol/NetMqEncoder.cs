using System;
using System.Collections.Generic;
using System.IO;
using net_mq_util;
using NetMQ;
using Newtonsoft.Json;
using protocol.db_methods;
using protocol.Exceptions;
using protocol.file_methods;
using protocol.methods.slave_owner_methods;
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



        #region encode methods for File servermodule
        public static NetMQMessage GenerateFileModuleMethodMessage(FileMethod method)
        {
            //if (method is HelloWorldMethod _method)
            //{
            //    return GenerateHelloWorldMessage(_method);
            //}
            throw new MethodFailedException();
        }
        #endregion





        #region encode methods for Slave owner servermodule
        public static NetMQMessage GenerateSlaveOwnerModuleMethodMessage(SlaveOwnerMethod method)
        {
            if (method is GetSlaveMethod)
            {
                return GenerateGetSlaveMessage((GetSlaveMethod)method);

            }else if (method is GetListOfRunnableApplicationsMethod )
            {
                return GenerateGetListOfRunnableApplicationsMessage((GetListOfRunnableApplicationsMethod)method);
            }

            throw new MethodFailedException();
        }
        protected static NetMQMessage GenerateGetSlaveMessage(GetSlaveMethod method)
        {
            var message = new NetMQMessage();
            string json = String.Empty;

            message.Append(GetFrame(GetSlaveMethod.METHOD_NAME));

            json = ConvertToJson(method.ApplicationInfo);
            message.Append(GetFrame(json));

            json = ConvertToJson(method.SlaveBelongsTo);
            message.Append(json);

            return message;
        }

        protected static NetMQMessage GenerateGetListOfRunnableApplicationsMessage(GetListOfRunnableApplicationsMethod method)
        {
            var message = new NetMQMessage();
            string json = String.Empty;

            message.Append(GetFrame(GetListOfRunnableApplicationsMethod.METHOD_NAME));

            return message;
        }
        #endregion




        #region encode methods for database servermodule
        public static NetMQMessage GenerateDatabaseModuleMethodMessage(DatabaseMethod method)
        {
            //if (method is HelloWorldMethod _method)
            //{
            //    return GenerateHelloWorldMessage(_method);
            //}
            throw new MethodFailedException();
        }



        #endregion




        #region encode methods for server module
        public static NetMQMessage GenerateServerModuleMethodMessage(ServerMethod method)
        {
            if (method is HelloWorldMethod _method)
            {
                return GenerateHelloWorldMessage(_method);
            }

            throw new MethodFailedException();
        }


        //most basic method with no real purpose
        protected static NetMQMessage GenerateHelloWorldMessage(HelloWorldMethod method)
        {
            var message = new NetMQMessage();

            message.Append(GetFrame(HelloWorldMethod.METHOD_NAME));
            message.Append(GetFrame(method.param1));

            return message;
        }


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
            return new NetMQFrame(data);
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
