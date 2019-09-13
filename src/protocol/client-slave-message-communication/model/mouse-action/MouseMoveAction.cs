using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    public class MouseMoveAction : BaseMouseAction
    {
        public RelativeScreenLocation arg1RelativeScreenLocation { get; set; }

        public override MouseAction Action => MouseAction.MouseMove;
    }
}
