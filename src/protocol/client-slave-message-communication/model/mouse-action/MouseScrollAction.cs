using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    public class MouseScrollAction : BaseMouseAction
    {
        public ScrollAmount ScrollAmountX { get; set; }
        public ScrollAmount ScrollAmountY { get; set; }

        public override MouseAction Action => MouseAction.Scroll;
    }
}
