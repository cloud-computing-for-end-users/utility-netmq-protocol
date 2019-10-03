namespace client_slave_message_communication.model.mouse_action
{
    public class RightMouseUpAction : BaseMouseAction
    {
        public override string Action => MouseAction.RightUp.ToString();
        public RelativeScreenLocation RelativeScreenLocation { get; set; }
    }
}
