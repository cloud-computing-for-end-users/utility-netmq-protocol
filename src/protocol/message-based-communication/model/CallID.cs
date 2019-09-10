using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class CallID
    {
        public string ID { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is CallID other)
            {
                return
                    ID.Equals(other.ID)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked(
                ID.GetHashCode() * 17
                );
        }
    }
}
