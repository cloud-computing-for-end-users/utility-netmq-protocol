﻿using message_based_communication.encoding;
using message_based_communication.model;
using message_based_communication.module;
using message_based_communication.proxy;
using message_based_communication.setup;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;

namespace message_based_communication.connection
{
    public class ProxyHelper
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// this is the module where requests are sent to
        /// </summary>
        private RequestSocket outTraffic;

        /// <summary>
        /// this is the module that any responses / incomming requests are forwarded to
        /// </summary>
        private BaseCommunicationModule baseModule;
        private Encoding customEncoding;

        //protected ModuleID moduleID;
        public ModuleID ModuleID { get; /*{ return this.moduleID; }*/ set; }

        private Dictionary<string, BaseProxy> callIDToReponseHandler = new Dictionary<string, BaseProxy>();

        public ProxyHelper()
        {

        }

        /// <summary>
        /// this method must be call after the constructor before any other methods are called
        /// </summary>
        /// <param name="routerModule"></param>
        /// <param name="baseModule"></param>
        public void Setup(ConnectionInformation routerModule, Port baseRouterRegistrationPort,ModuleType moduleType,ConnectionInformation forSelf ,BaseCommunicationModule baseModule, Encoding customEncoding)
        {
            Logger.Debug("Starting proxy helper setup with args: " + routerModule + "; " + baseRouterRegistrationPort + "; " + moduleType + "; " + forSelf + "; " + baseModule + "; " + customEncoding);
            //this.routerModule = routerModule;
            this.baseModule = baseModule;
            this.customEncoding = customEncoding;

            this.outTraffic = new RequestSocket("tcp://" + routerModule.IP.TheIP + ":" + routerModule.Port.ThePort);

            this.ModuleID = RegisterModule(baseModule.ModuleType, routerModule, baseRouterRegistrationPort, forSelf);

            var t = new Thread(() =>
            {

                ReciveSendable(customEncoding, forSelf.Port);

            });
            t.IsBackground = true;
            t.Start();
        }

        private static ModuleID RegisterModule(ModuleType moduleType, ConnectionInformation baseRouterModule, Port baseRouterRegistrationPort, ConnectionInformation forSelf)
        {
            var reqSocker = new RequestSocket("tcp://" + baseRouterModule.IP.TheIP + ":" + baseRouterRegistrationPort.ThePort);

            var request = new RegisterModuleRequest()
            {
                CallID = new CallID() { ID = "SETUP_" + new Random().Next() },
                ModuleType = moduleType,
                ConnInfo = forSelf
            };

            var encodedReq = encoding.Encoding.EncodeRegisterModuleRequest(request);
            reqSocker.SendMultipartMessage(encodedReq);

            //reciving response
            var encodedResponse = reqSocker.ReceiveMultipartMessage();
            var decodedResponse = encoding.Encoding.TryDecodeRegisterModuleResponse(encodedResponse);


            return decodedResponse.ModuleID;

        }



        public void SendResponse(Response response)
        {
            lock (outTraffic)
            {
                var res = Encoding.EncodeResponse(response);
                Console.WriteLine("Sending response: { " + response + " }");
                outTraffic.SendMultipartMessage(res);

                //Check for acknoladgement from recipient

                var ack = outTraffic.ReceiveMultipartMessage();

                ValidateAckMessage(ack, response, this.customEncoding);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseProxy"> The Base proxy that will handle the response</param>
        public void SendMessage(BaseRequest message, BaseProxy baseProxy)
        {
            lock (outTraffic)
            {

                //var callID = new Random().Next();

                callIDToReponseHandler.Add(message.CallID.ID, baseProxy);

                var req = Encoding.EncodeRequest(message);
                this.outTraffic.SendMultipartMessage(req);

                //listen for recive ack.
                var ack = this.outTraffic.ReceiveMultipartMessage();
                ValidateAckMessage(ack, message, this.customEncoding);
            }
        }

        public static void ValidateAckMessage(NetMQMessage ackMessage, Sendable whatWasSent, Encoding customEncoding)
        {
            var decodedAck = customEncoding.DecodeIntoSendable(ackMessage);
            if (decodedAck is AcknowledgeRecivedSendable _ack)
            {
                //if (false == (_ack.CallID.ID == whatWasSent.CallID.ID))
                //{
                 //   throw new Exception();
               // }
            }
            else
            {
                throw new Exception();
            }

        }

        public void ReciveSendable(Encoding customEncoding, Port portToListenForIncommingData)
        {
            if (null == portToListenForIncommingData)
            {
                throw new Exception();
            }

            ResponseSocket inTraffic = new ResponseSocket("tcp://" + "0.0.0.0:" + portToListenForIncommingData.ThePort);

            while (true)
            {
                //recive message
                var message = inTraffic.ReceiveMultipartMessage();
                var sendable = customEncoding.DecodeIntoSendable(message);

                //send message ack
                inTraffic.SendMultipartMessage(Encoding.EncodeAckRecivedSendable(new AcknowledgeRecivedSendable()
                {
                    CallID = sendable.CallID,
                    SenderModuleID = this.ModuleID, //TODO this whole ack think is currently kinda wrong or atleast it is just the next node in line that will send back an ack
                    TargetModuleID = sendable.SenderModuleID
                }));



                if (sendable is BaseRequest _request)
                {
                    //this is a request
                    if (baseModule is BaseServerModule _baseServerModule
                        && _request.TargetModuleType.TypeID.Equals(this.baseModule.ModuleType.TypeID)
                        )
                    {
                        _baseServerModule.HandleRequest(_request);
                    }
                    else if (baseModule is BaseRouterModule _router)
                    {
                        _router.HandleSendable(sendable);
                    }
                }
                else if (sendable is Response response)
                {
                    if (baseModule is BaseRouterModule _router
                        && response.TargetModuleID.ID != ModuleID.ID
                        )
                    {
                        _router.HandleSendable(response);
                    }
                    else
                    {
                        lock (this.callIDToReponseHandler)
                        {
                            if (callIDToReponseHandler.ContainsKey(response.CallID.ID))
                            {
                                //this is a response made to a request
                                callIDToReponseHandler[response.CallID.ID].HandleResponse(response);
                            }
                        }
                    }

                }
            }
        }




    }
}
