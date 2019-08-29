using net_mq_util;
using protocol.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.slave_owner_methods
{
    public class GetSlaveMethod : SlaveOwnerMethod
    {
        public static string METHOD_NAME = ProtocolConstants.MET_SO_GET_SLAVE;

        public ApplicationInfo ApplicationInfo { get; set; }
        public PrimaryKey SlaveBelongsTo { get; set; }


    }
}
