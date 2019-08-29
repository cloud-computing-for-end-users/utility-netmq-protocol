using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class ConnectionInfo
    {
        public Port Port { get; set; }
        public IP Ip { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ConnectionInfo _obj)
            {
                return 
                    this.Port.Equals(_obj.Port)
                    && this.Ip.Equals(_obj.Ip)
                    ;
            }
            return false;
        }

    }
}
