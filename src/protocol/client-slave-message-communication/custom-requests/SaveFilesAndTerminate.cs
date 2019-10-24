using System;
using System.Collections.Generic;
using System.Text;
using message_based_communication.model;

namespace client_slave_message_communication.custom_requests
{
    public class SaveFilesAndTerminate : BaseRequest
    {
        public override string SpecificMethodID => consts.MethodID.METHOD_ID_SAVE_FILES_AND_TERMINATE;
    }
}
