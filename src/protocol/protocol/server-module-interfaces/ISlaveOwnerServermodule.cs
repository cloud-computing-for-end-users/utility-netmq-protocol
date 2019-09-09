using protocol.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.server_module_interfaces
{
    public interface ISlaveOwnerServermodule
    {
        SlaveConnectionInfo GetSlave(ApplicationInfo appInfo, PrimaryKey primaryKey);
        List<ApplicationInfo> GetListOfRunnableApplications();

    }
}
