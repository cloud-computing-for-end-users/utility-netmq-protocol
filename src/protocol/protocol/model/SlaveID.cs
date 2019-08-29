using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class SlaveID
    {
        public string SlaveId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SlaveID _obj)
            {
                return 
                    this.SlaveId.Equals(_obj.SlaveId)
                    ;

            }
            return false;
        }
    }
}
