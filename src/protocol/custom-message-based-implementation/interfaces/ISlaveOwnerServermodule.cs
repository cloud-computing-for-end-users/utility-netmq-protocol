using custom_message_based_implementation.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace custom_message_based_implementation.interfaces
{
    interface ISlaveOwnerServermodule
    {
        SlaveConnection GetSlave(ApplicationInfo appInfo, PrimaryKey primaryKey);

        List<ApplicationInfo> GetListOfRunnableApplications();

    }
}
