namespace custom_message_based_implementation.consts
{
    public class MethodID
    {
        // Slave owner
        public const string METHOD_SLAVE_OWNER_GET_SLAVE = "1-SO_REQ_GET_SLAVE";
        public const string METHOD_SLAVE_OWNER_GET_LIST_OF_RUNNING_APP= "2-SO_REQ_GET_LIST";

        // Database
        public const string METHOD_DATABASE_LOGIN = "1-DB_REQ_LOGIN";
        public const string METHOD_DATABASE_CREATE_ACCOUNT = "2-DB_REQ_CREATE_ACCOUNT";

        // File
        public const string METHOD_FILE_GET_LIST_OF_FILES = "1-FSM_REQ_GET_LIST_OF_FILES";
        public const string METHOD_FILE_UPLOAD_FILE = "2-FSM_REQ_UPLOAD_FILE";
        public const string METHOD_FILE_DOWNLOAD_FILE = "3-FSM_REQ_DOWNLOAD_FILE";
        public const string METHOD_FILE_RENAME_FILE = "4-FSM_REQ_RENAME_FILE";
        public const string METHOD_FILE_REMOVE_FILE = "5-FSM_REQ_REMOVE_FILE";
    }
}
