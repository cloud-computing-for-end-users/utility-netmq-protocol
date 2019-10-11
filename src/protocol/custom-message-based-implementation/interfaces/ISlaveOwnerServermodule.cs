﻿using custom_message_based_implementation.model;
using message_based_communication.model;
using System;
using System.Collections.Generic;

namespace custom_message_based_implementation.interfaces
{
    public interface ISlaveOwnerServermodule
    {
        Tuple<SlaveConnection,Port> GetSlave(ApplicationInfo appInfo, PrimaryKey primaryKey);

        List<ApplicationInfo> GetListOfRunnableApplications();

    }
}
