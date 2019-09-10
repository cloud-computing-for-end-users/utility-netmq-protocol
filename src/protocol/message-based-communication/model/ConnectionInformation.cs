using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class ConnectionInformation
    {
        public Port Port { get; set; }
        public IP IP { get; set; }



        public override bool Equals(object obj)
        {
            if (obj is ConnectionInformation other)
            {
                return
                    Port.Equals(other.Port)
                    && IP.Equals(other.IP)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked(
                Port.GetHashCode() * 17
                + IP.GetHashCode() * 17
                );
        }
    }
}
