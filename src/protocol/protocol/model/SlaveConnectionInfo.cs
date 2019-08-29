using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class SlaveConnectionInfo : ConnectionInfo
    {
        public SlaveID SlaveId { get; set; }
        public PrimaryKey OwnerPrimaryKey { get; set; }



        public override bool Equals(object obj)
        {
            if (obj is SlaveConnectionInfo _obj)
            {
                return 
                    this.SlaveId.Equals(_obj.SlaveId)
                    && this.OwnerPrimaryKey.Equals(_obj.OwnerPrimaryKey)
                    ;

            }
            return false;
        }
    }
}
