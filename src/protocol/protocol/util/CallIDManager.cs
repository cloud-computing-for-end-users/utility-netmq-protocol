using protocol.Exceptions;
using protocol.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.util
{
    public class CallIDManager
    {
        protected Dictionary<int, Action<object>> registeredCalls;

        public static CallIDManager INSTANCE = new CallIDManager();

        private CallIDManager()
        {
            this.registeredCalls = new Dictionary<int, Action<object>>();
        }

        public CallID RegisterCall(Action<object> callBack)
        {
            var callID = GetUnusedCallID();
            this.registeredCalls[callID.ID] = callBack;
            return callID;
        }

        public Action<object> RecivedCallID(CallID callID)
        {
            if(false == this.registeredCalls.ContainsKey(callID.ID))
            {
                throw new MethodFailedException();
            }

            var result = this.registeredCalls[callID.ID];
            this.registeredCalls.Remove(callID.ID);
            return result;
        }


        Random Random = new Random();
        public CallID GetUnusedCallID()
        {
            int callID = -1;
            do
            {
                callID = Random.Next();

            } while (this.registeredCalls.ContainsKey(callID));

            return new CallID() {ID=callID };
        }

    }
}
