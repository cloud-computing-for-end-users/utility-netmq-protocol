using protocol.model;
using protocol.server_methods;
using System;
using System.Collections.Generic;
using System.Text;
using static net_mq_util.NetMqUtil;

namespace protocol.methods.server_methods
{
    public class RegisterServermoduleMethod : ServerMethod
    {
        public static readonly string METHOD_NAME = net_mq_util.ProtocolConstants.MET_SM_REG_SM;

        public ConnectionInfo ConnectionInfo { get; set; }
        public TargetType TargetType { get; set; }

    }
}
