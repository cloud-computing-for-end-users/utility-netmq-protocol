using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class Success
    {
        public static Success Yes { get { return new Success { Succeded = true }; } }
        public static Success No { get { return new Success { Succeded = false }; } }


        public bool Succeded { get; set; }



        public override bool Equals(object obj)
        {
            if(obj is Success _obj)
            {
                return 
                    this.Succeded.Equals(_obj.Succeded)
                    ;

            }
            return false;
        }

        
    }
}
