using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class Password
    {
        public string ThePassword { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is Password _obj)
            {
                return
                    this.ThePassword.Equals(_obj.ThePassword)
                    ;
            }
            return false;
        }
    }
}
