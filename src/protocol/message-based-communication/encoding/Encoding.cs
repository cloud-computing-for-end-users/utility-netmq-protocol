using message_based_communication.model;
using message_based_communication.setup;
using NetMQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.encoding
{
    public abstract class Encoding
    {
        private const string RESPONSE_PREFIX = "RESPONSE";
        private const string ACK_RECIVE = "ACKNOWLEDGE_RECIVED";


        private const string REG_MODULE_PREFIX = "REGISTER_MODULE";
        private const string REG_MODULE_RES_PREFIX = "REGISTER_MODULE_RESPONSE";

        public static NetMQMessage EncodeAckRecivedSendable(AcknowledgeRecivedSendable ackRecived)
        {
            var message = new NetMQMessage();
            message.Append(new NetMQFrame(ACK_RECIVE));
            message.Append(new NetMQFrame(EncodeToJson(ackRecived)));
            return message;
        }
        public static NetMQMessage EncodeResponse(Response sendable)
        {
            var message = new NetMQMessage();
            message.Append(new NetMQFrame(RESPONSE_PREFIX));
            message.Append(new NetMQFrame(EncodeToJson(sendable)));
            return message;
        }
        public static NetMQMessage EncodeRequest(BaseRequest request)
        {
            var message = new NetMQMessage();
            message.Append(new NetMQFrame(request.SpecificMethodID));
            message.Append(new NetMQFrame(EncodeToJson(request)));
            return message;
        }
        public Sendable DecodeIntoSendable(NetMQMessage message)
        {
            var first = message.Pop().ConvertToString();

            if (RESPONSE_PREFIX.Equals(first))
            {
                return TryDecodeJson<Response>(message.Pop().ConvertToString()) ;
            }else if (ACK_RECIVE.Equals(first))
            {
                return TryDecodeIntoAckRecived(message);
            }
            else
            {
                return DecodeJsonToSpecificRequest(first, message.Pop().ConvertToString());
            }
        }

        private AcknowledgeRecivedSendable TryDecodeIntoAckRecived(NetMQMessage message)
        {
            var jsonString = message.Pop().ConvertToString();
            var decoed = TryDecodeJson<AcknowledgeRecivedSendable>(jsonString);
            return decoed;
        }

        /// <summary>
        /// this must be implemented in a sub class as the json deserialisation needs the right type matched with the specific method ID
        /// </summary>
        /// <param name="specificMethodID"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        protected abstract BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString);



        public static string EncodeToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        protected static T TryDecodeJson<T> (string json) where T : class
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            if (obj is T _T)
            {
                return _T;
            }
            return null;
        }




        public static NetMQMessage EncodeRegisterModuleRequest(RegisterModuleRequest request)
        {
            var message = new NetMQMessage();
            message.Append(new NetMQFrame(REG_MODULE_PREFIX));
            message.Append(new NetMQFrame(EncodeToJson(request)));
            return message;
        }

        public static RegisterModuleRequest TryDecodeRegisterModuleRequest(NetMQMessage message)
        {
            var first = message.Pop().ConvertToString();

            if (false == REG_MODULE_PREFIX.Equals(first))
            {
                return null;
            }

            var obj = message.Pop().ConvertToString();
            
            return TryDecodeJson<RegisterModuleRequest>(obj);
        }

        public static NetMQMessage EncodeRegisterModuleResponse(RegisterModuleResponse response)
        {
            var message = new NetMQMessage();
            message.Append(new NetMQFrame(REG_MODULE_RES_PREFIX));
            message.Append(new NetMQFrame(EncodeToJson(response)));
            return message;
        }

        public static RegisterModuleResponse TryDecodeRegisterModuleResponse(NetMQMessage message)
        {
            var first = message.Pop().ConvertToString();

            if (false == REG_MODULE_RES_PREFIX.Equals(first))
            {
                return null;
            }

            var obj = message.Pop().ConvertToString();
            return TryDecodeJson<RegisterModuleResponse>(obj);
        }
    }
}
