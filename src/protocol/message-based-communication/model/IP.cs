using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class IP
    {
        public string TheIP { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is IP other)
            {
                return this.TheIP.Equals(other.TheIP);
            }
            return false;
        }


        //accoring to https://stackoverflow.com/questions/9009760/implementing-gethashcode-correctly
        public override int GetHashCode()
        {
            return unchecked(TheIP.GetHashCode() * 17);
        }
    }
}
