using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class PrimaryKey
    {
        public string ThePrimaryKey { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is PrimaryKey _obj)
            {
                return
                    this.ThePrimaryKey.Equals(_obj.ThePrimaryKey)
                    ;
            }
            return false;
        }
    }
}
