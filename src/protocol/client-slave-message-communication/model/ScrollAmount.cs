namespace client_slave_message_communication.model.mouse_action
{
    public class ScrollAmount
    {
        /// <summary>
        /// Bi-directional scroll wrapper.
        /// Scrolling down or to the right (moving down or to the right) is a positive value.
        /// Scrolling up or to the left (moving up or to the left) is a negative value.
        /// </summary>
        public double Amount { get; set; }
    }
}