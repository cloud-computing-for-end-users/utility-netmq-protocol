using System;
using System.Collections.Generic;
using custom_message_based_implementation.consts;
using custom_message_based_implementation.model;
using message_based_communication.connection;
using message_based_communication.model;
using message_based_communication.module;
using message_based_communication.proxy;

namespace custom_message_based_implementation.proxy
{
    public class FileServermoduleProxy : BaseProxy
    {
        private readonly ModuleType moduleType = new ModuleType() { TypeID = ModuleTypeConst.MODULE_TYPE_FILE };

        public FileServermoduleProxy(ProxyHelper proxyHelper, BaseCommunicationModule baseCommunicationModule) : base(proxyHelper, baseCommunicationModule) {}

        protected override void SetStandardParameters(BaseRequest baseRequest)
        {
            base.SetStandardParameters(baseRequest, moduleType);
        }

        public void GetListOfFiles(PrimaryKey pk, Action<List<FileName>> callBack)
        {
            var request = new RequestGetListOfFiles { PrimaryKey = pk };

            SetStandardParameters(request);

            base.SendMessage(WrapCallBack<List<FileName>>(callBack), request);
        }

        public void UploadFile(File file, PrimaryKey pk, bool overwrite, Action callBack)
        {
            var request = new RequestUploadFile { PrimaryKey = pk, File = file, Overwrite = overwrite };

            SetStandardParameters(request);

            base.SendMessage(WrapNoParamAction(callBack), request);
        }

        public void DownloadFile(FileName fileName, PrimaryKey pk, Action<File> callBack)
        {
            var request = new RequestDownloadFile { PrimaryKey = pk, FileName = fileName };

            SetStandardParameters(request);

            base.SendMessage(WrapCallBack<File>(callBack), request);
        }

        public void RenameFile(FileName oldFileName, FileName newFileName, PrimaryKey pk, Action callBack)
        {
            var request = new RequestRenameFile { PrimaryKey = pk, OldFileName = oldFileName, NewFileName = newFileName};

            SetStandardParameters(request);

            base.SendMessage(WrapNoParamAction(callBack), request);
        }

        public void RemoveFile(FileName fileName, PrimaryKey pk, Action callBack)
        {
            var request = new RequestRemoveFile { PrimaryKey = pk, FileName = fileName };

            SetStandardParameters(request);

            base.SendMessage(WrapNoParamAction(callBack), request);
        }
    }
}