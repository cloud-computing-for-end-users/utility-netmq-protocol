using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class ModuleID
    {
        public string ID { get; set; }



        public override bool Equals(object obj)
        {
            if(obj is ModuleID other)
            {
                return this.ID.Equals(other.ID);
            }
            return false;
        }


        //accoring to https://stackoverflow.com/questions/9009760/implementing-gethashcode-correctly
        public override int GetHashCode()
        {
            return unchecked(ID.GetHashCode()*17);
        }
    }
}
