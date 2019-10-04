using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    public class RightMouseUpAction : BaseMouseAction
    {
        public override string Action => MouseAction.RightDown.ToString();
        public RelativeScreenLocation RelativeScreenLocation { get; set; }
    }
}
