﻿using System;
using System.Collections.Generic;
using System.Text;

namespace client_slave_message_communication.model.mouse_action
{

    /// <summary>
    /// should be treated as an abstract class and thust shound nover be nessesary to instanciate
    /// </summary>
    public class BaseMouseAction
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

        /// <summary>
        /// must always be set by <see langword="abstract"/>subclass
        /// </summary>
        public virtual MouseAction Action { get; } 
    }
}
