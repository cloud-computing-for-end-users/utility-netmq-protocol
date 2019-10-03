﻿using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    public class LeftMouseDownAction : BaseMouseAction
    {
        //public RelativeScreenLocation arg1ScreenLocation { get; set; }

        public override string Action => MouseAction.LeftDown.ToString();
        public RelativeScreenLocation RelativeScreenLocation { get; set; }
    }
}
