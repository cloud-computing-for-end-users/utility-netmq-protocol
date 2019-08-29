using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class Port
    {
        public int ThePort { get; set; }



        public override bool Equals(object obj)
        {
            if (obj is Port _obj)
            {
                return
                    this.ThePort.Equals(_obj.ThePort)
                    ;
            }
            return false;
        }
    }
}
