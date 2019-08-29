using protocol.slave_owner_methods;
using net_mq_util;



namespace protocol.methods.slave_owner_methods
{
    public class GetListOfRunnableApplicationsMethod : SlaveOwnerMethod
    {
        public const string METHOD_NAME = ProtocolConstants.MET_SO_GET_LIST_OF_RUNNABLE_APPLICATIONS;
    }
}
