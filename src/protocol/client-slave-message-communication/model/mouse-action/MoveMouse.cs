using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    public class MoveMouse : BaseMouseAction
    {
        public RelativeScreenLocation RelativeScreenLocation { get; set; }
    }
}
