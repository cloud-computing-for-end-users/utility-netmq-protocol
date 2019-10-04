namespace client_slave_message_communication.model.mouse_action
{
    public class LeftMouseUpAction : BaseMouseAction
    {
        public override string Action => MouseAction.LeftUp.ToString();
        public RelativeScreenLocation RelativeScreenLocation { get; set; }
    }
}
