using System.Collections.Generic;
using custom_message_based_implementation.model;

namespace custom_message_based_implementation.interfaces
{
    public interface IFileServermodule
    {
        List<FileName> GetListOfFiles(PrimaryKey pk);
        void UploadFile(File file, PrimaryKey pk, bool overwrite);
        File DownloadFile(FileName fileName, PrimaryKey pk);
        void RenameFile(FileName oldFileName, FileName newFileName, PrimaryKey pk);
        void RemoveFile(FileName fileName, PrimaryKey pk);
    }
}