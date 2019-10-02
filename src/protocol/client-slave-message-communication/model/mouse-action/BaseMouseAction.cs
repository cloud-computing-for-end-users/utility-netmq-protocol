using System;
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
            LeftDown,
            LeftUp,
            ClickRight,
            ScrollDown, // todo remove, merged to Scroll
            ScrollUp, // todo remove, merged to Scroll
            Scroll,
            ClickMouseWheel
        }

        /// <summary>
        /// must always be set by <see langword="abstract"/>subclass
        /// </summary>
        public virtual string Action { get; set; } 
    }
}
