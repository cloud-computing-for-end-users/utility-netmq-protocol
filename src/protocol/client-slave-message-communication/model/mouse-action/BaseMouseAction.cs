namespace client_slave_message_communication.model.mouse_action
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
            RightDown,
            RightUp
        }

        /// <summary>
        /// must always be set by <see langword="abstract"/>subclass
        /// </summary>
        public virtual string Action { get; set; } 
    }
}
