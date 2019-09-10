using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class Payload
    {
        public object ThePayload { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Payload other)
            {
                return
                    ThePayload.Equals(other.ThePayload)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked(
                ThePayload.GetHashCode() * 17
                );
        }
    }
}
