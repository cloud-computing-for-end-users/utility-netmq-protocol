using System;
using message_based_communication.model;
using client_slave_message_communication.consts;
using client_slave_message_communication.model.mouse_action;
using client_slave_message_communication.model.keyboard_action;
using client_slave_message_communication.custom_requests;

namespace client_slave_message_communication.encoding
{
    public class CustomEncoding : message_based_communication.encoding.Encoding
    {
        protected override BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString)
        {
            if (specificMethodID.Equals(MethodID.METHOD_ID_DO_MOUSE_ACTION))
            {
                var DoMouseAction = TryDecodeJson<DoMouseAction<BaseMouseAction>>(jsonString);
                if(BaseMouseAction.MouseAction.ClickLeft.ToString().Equals(DoMouseAction.arg1MouseAction.Action))
                {
                    return TryDecodeJson<DoMouseAction<ClickLeftAction>>(jsonString);
                }
                else if (BaseMouseAction.MouseAction.LeftDown.ToString().Equals(DoMouseAction.arg1MouseAction.Action))
                {
                    return TryDecodeJson<DoMouseAction<LeftMouseDownAction>>(jsonString);
                }
                else if (BaseMouseAction.MouseAction.LeftUp.ToString().Equals(DoMouseAction.arg1MouseAction.Action))
                {
                    return TryDecodeJson<DoMouseAction<LeftMouseUpAction>>(jsonString);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (specificMethodID.Equals(MethodID.METHOD_ID_DO_KEYBOARD_ACTION))
            {
                return TryDecodeJson<DoKeyboardAction>(jsonString);
            }else if (specificMethodID.Equals(MethodID.METHOD_ID_GET_IMAGE_PRODUCER_CONN))
            {
                return TryDecodeJson<GetImageProducerConnectionInfo>(jsonString);
            }else if (specificMethodID.Equals(MethodID.METHOD_ID_HANDSHAKE))
            {
                return TryDecodeJson<Handshake>(jsonString);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
