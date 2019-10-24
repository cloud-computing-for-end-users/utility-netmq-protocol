using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.consts
{
    public class MethodID
    {
        public const string METHOD_ID_SAVE_FILES_AND_TERMINATE = "SAVE_FILES_AND_TERMINATE";
        public const string METHOD_ID_FETCH_REMOTE_FILE        = "FETCH_REMOTE_FILE";
        public const string METHOD_ID_DO_KEYBOARD_ACTION       = "DO_KEY_ACTION";
        public const string METHOD_ID_DO_MOUSE_ACTION          = "DO_MOUSE_ACTION";
        public const string METHOD_ID_GET_IMAGE_PRODUCER_CONN  = "GET_IMAGE_PRODUCER_CONN";
        public const string METHOD_ID_HANDSHAKE                = "HANDSHAKE";

    }
}
