﻿using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    [Obsolete]
    public class MouseMoveAction : BaseMouseAction
    {
        public RelativeScreenLocation RelativeScreenLocation { get; set; }

        public override string Action => MouseAction.MouseMove.ToString();
    }
}
