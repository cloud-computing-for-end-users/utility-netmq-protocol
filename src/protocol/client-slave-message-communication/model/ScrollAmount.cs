namespace client_slave_message_communication.model.mouse_action
{
    public class ScrollAmount
    {
        /// <summary>
        /// Bi-directional scroll wrapper.
        /// Scrolling down (moving down) is a positive value.
        /// Scrolling up (moving up) is a negative value.
        /// </summary>
        public double Amount { get; set; }
    }
}