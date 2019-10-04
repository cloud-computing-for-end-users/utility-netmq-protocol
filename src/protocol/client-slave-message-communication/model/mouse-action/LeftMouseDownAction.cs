namespace client_slave_message_communication.model.mouse_action
{
    public class LeftMouseDownAction : BaseMouseAction
    {
        public override string Action => MouseAction.LeftDown.ToString();
        public RelativeScreenLocation RelativeScreenLocation { get; set; }
    }
}
