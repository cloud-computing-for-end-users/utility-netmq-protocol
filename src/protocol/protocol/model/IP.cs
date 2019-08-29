using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class IP
    {
        public string Ip{ get; set; }

        public override bool Equals(object obj)
        {
            if (obj is IP _obj)
            {
                return this.Ip.Equals(_obj.Ip)
                    ;

            }
            return false;
        }
    }
}
