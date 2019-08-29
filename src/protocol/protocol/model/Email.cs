using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class Email
    {
        public string TheEmail { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Email _obj)
            {
                return
                    this.TheEmail.Equals(_obj.TheEmail)
                    ;
            }
            return false;
        }
    }
}
