using client_slave_message_communication.model.keyboard_action;
using client_slave_message_communication.model.mouse_action;
using custom_message_based_implementation.model;
using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.interfaces
{
    public interface ISlave
    {
        void DoKeyboardAction(BaseKeyboardAction action);
        void DoMouseAction(BaseMouseAction action);
        void Handshake(PrimaryKey pk);
        Port GetImageProducerConnInfo();

    }
}
