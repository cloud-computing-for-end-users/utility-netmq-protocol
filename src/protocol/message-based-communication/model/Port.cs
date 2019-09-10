using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class Port
    {
        public int ThePort { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Port other)
            {
                return
                    ThePort.Equals(other.ThePort)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked( 
                ThePort.GetHashCode() * 17
                );
        }
    }
}
