using System;
using message_based_communication.model;
using client_slave_message_communication.consts;
using client_slave_message_communication.model.mouse_action;
using client_slave_message_communication.custom_requests;

namespace client_slave_message_communication.encoding
{
    public class CustomEncoding : message_based_communication.encoding.Encoding
    {
        protected override BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString)
        {
            if (specificMethodID.Equals(MethodID.METHOD_ID_DO_MOUSE_ACTION))
            {
                var doMouseAction = TryDecodeJson<DoMouseAction<BaseMouseAction>>(jsonString);
                if (BaseMouseAction.MouseAction.LeftDown.ToString().Equals(doMouseAction.arg1MouseAction.Action))
                {
                    return TryDecodeJson<DoMouseAction<LeftMouseDownAction>>(jsonString);
                }
                if (BaseMouseAction.MouseAction.LeftUp.ToString().Equals(doMouseAction.arg1MouseAction.Action))
                {
                    return TryDecodeJson<DoMouseAction<LeftMouseUpAction>>(jsonString);
                }
                else if (BaseMouseAction.MouseAction.RightDown.ToString().Equals(DoMouseAction.arg1MouseAction.Action))
                {
                    return TryDecodeJson<DoMouseAction<RightMouseDownAction>>(jsonString);
                }
                else if (BaseMouseAction.MouseAction.RightUp.ToString().Equals(DoMouseAction.arg1MouseAction.Action))
                {
                    return TryDecodeJson<DoMouseAction<RightMouseUpAction>>(jsonString);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            if (specificMethodID.Equals(MethodID.METHOD_ID_DO_KEYBOARD_ACTION))
            {
                return TryDecodeJson<DoKeyboardAction>(jsonString);
            } 
            if (specificMethodID.Equals(MethodID.METHOD_ID_GET_IMAGE_PRODUCER_CONN))
            {
                return TryDecodeJson<GetImageProducerConnectionInfo>(jsonString);
            }
            if (specificMethodID.Equals(MethodID.METHOD_ID_HANDSHAKE))
            {
                return TryDecodeJson<Handshake>(jsonString);
            }
            throw new NotImplementedException();
        }
    }
}
