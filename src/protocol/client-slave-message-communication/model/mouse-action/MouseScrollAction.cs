using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    [Obsolete]
    public class MouseScrollAction : BaseMouseAction
    {
        public ScrollAmount ScrollAmountX { get; set; }
        public ScrollAmount ScrollAmountY { get; set; }

        public override string Action => MouseAction.Scroll.ToString();
    }
}
