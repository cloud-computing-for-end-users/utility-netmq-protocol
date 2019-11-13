using System;
using message_based_communication.model;
using client_slave_message_communication.model.mouse_action;
using client_slave_message_communication.custom_requests;

namespace client_slave_message_communication.encoding
{
    public class CustomEncoding : message_based_communication.encoding.Encoding
    {
        protected override BaseRequest DecodeJsonToSpecificRequest(string specificMethodID, string jsonString)
        {

            switch (specificMethodID)
            {
                case DoMouseAction<BaseMouseAction>.METHOD_ID:
                    var doMouseAction = TryDecodeJson<DoMouseAction<BaseMouseAction>>(jsonString);
                    if (BaseMouseAction.MouseAction.LeftDown.ToString().Equals(doMouseAction.arg1MouseAction.Action))
                    {
                        return TryDecodeJson<DoMouseAction<LeftMouseDownAction>>(jsonString);
                    }
                    else if (BaseMouseAction.MouseAction.LeftUp.ToString().Equals(doMouseAction.arg1MouseAction.Action))
                    {
                        return TryDecodeJson<DoMouseAction<LeftMouseUpAction>>(jsonString);
                    }
                    else if (BaseMouseAction.MouseAction.RightDown.ToString().Equals(doMouseAction.arg1MouseAction.Action))
                    {
                        return TryDecodeJson<DoMouseAction<RightMouseDownAction>>(jsonString);
                    }
                    else if (BaseMouseAction.MouseAction.RightUp.ToString().Equals(doMouseAction.arg1MouseAction.Action))
                    {
                        return TryDecodeJson<DoMouseAction<RightMouseUpAction>>(jsonString);
                    }
                    else if (BaseMouseAction.MouseAction.MouseMove.ToString().Equals(doMouseAction.arg1MouseAction.Action))
                    {
                        return TryDecodeJson<DoMouseAction<MouseMoveAction>>(jsonString);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                case DoKeyboardAction.METHOD_ID:
                    return TryDecodeJson<DoKeyboardAction>(jsonString);
                case GetImageProducerConnectionInfo.METHOD_ID:
                    return TryDecodeJson<GetImageProducerConnectionInfo>(jsonString);
                case Handshake.METHOD_ID:
                    return TryDecodeJson<Handshake>(jsonString);
                case FetchRemoteFile.METHOD_ID:
                    return TryDecodeJson<FetchRemoteFile>(jsonString);
                case SaveFilesAndTerminate.METHOD_ID:
                    return TryDecodeJson<SaveFilesAndTerminate>(jsonString);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
