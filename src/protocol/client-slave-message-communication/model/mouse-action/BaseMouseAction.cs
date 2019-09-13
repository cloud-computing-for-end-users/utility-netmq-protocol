using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{
    public abstract class BaseMouseAction
    {
        public enum MouseAction
        {
            MouseMove,
            ClickLeft,
            ClickRight,
            ScrollDown,
            ScrollUp,
            ClickMouseWheel
        }

        public abstract MouseAction Action { get; }
    }
}
