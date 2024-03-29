﻿namespace client_slave_message_communication.model.mouse_action
{

    /// <summary>
    /// should be treated as an abstract class and thust shound nover be nessesary to instanciate
    /// </summary>
    public class BaseMouseAction
    {
        public enum MouseAction
        {
            LeftDown,
            LeftUp,
            ClickRight,
            RightDown,
            RightUp,
            ScrollDown, // todo remove, merged to Scroll
            ScrollUp, // todo remove, merged to Scroll
            Scroll,
            ClickMouseWheel,
            MouseMove
        }

        /// <summary>
        /// must always be set by <see langword="abstract"/>subclass
        /// </summary>
        public virtual string Action { get; set; } 
    }
}
