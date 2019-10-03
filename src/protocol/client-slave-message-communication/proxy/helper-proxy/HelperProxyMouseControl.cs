﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using client_slave_message_communication.model.mouse_action;

namespace client_slave_message_communication.proxy.helper_proxy
{
    public class HelperProxyMouseControl
    {
        private MouseControlApi mouseControlApi;

        private Queue<BaseMouseAction> mouseActionQueue = new Queue<BaseMouseAction>();

        public void QueueMouseCommand(BaseMouseAction mouseAction)
        {
            mouseActionQueue.Enqueue(mouseAction);
        }


        public HelperProxyMouseControl()
        {
            this.mouseControlApi = new MouseControlApi();

            var t = new Thread(() =>
            {

                while (true)
                {
                    if (0 == mouseActionQueue.Count)
                    {
                        Thread.Sleep(10);
                    }
                    else
                    {
                        try
                        {
                            HandleMouseAction(mouseActionQueue.Dequeue());
                        }
                        catch (NullReferenceException e)
                        {
                            throw new Exception("Got exception in HelperProxyMouseControl");
                        }
                    }
                }
            });
            t.IsBackground = true;
            t.Start();
        }


        protected void HandleMouseAction(BaseMouseAction mouseAction)
        {
            var Type = mouseAction.GetType();
            if (BaseMouseAction.MouseAction.ClickLeft.ToString().Equals(mouseAction.Action)
                && mouseAction is ClickLeftAction _clickLeft)
            {
                this.mouseControlApi.ClickLeft();
            }
            else if (BaseMouseAction.MouseAction.MouseMove.ToString().Equals(mouseAction.Action)
                && mouseAction is MouseMoveAction _mouseMove)
            {
                this.mouseControlApi.MoveMouse(_mouseMove.RelativeScreenLocation);
                if (_mouseMove.RelativeScreenLocation != null)
                {
                    this.mouseControlApi.MoveMouse(_mouseMove.RelativeScreenLocation);

                    //only do the last in a stream of mouse moves

                    //if (0 == mouseActionQueue.Count)
                    //{
                    //    this.mouseControlApi.MoveMouse(_mouseMove.RelativeScreenLocation, windowHandle);
                    //    return;
                    //}
                    //else
                    //{
                    //    var peeked = mouseActionQueue.Peek();
                    //    if (peeked is MouseMoveAction)
                    //    {
                    //        return; // skip to next one
                    //        Logger.
                    //    }
                    //    else
                    //    {
                    //        this.mouseControlApi.MoveMouse(_mouseMove.RelativeScreenLocation, windowHandle);
                    //    }
                    //}


                    //if (0 == mouseMoveCounter)
                    //{
                    //    //dont do the mouse move
                    //}
                    //else
                    //{
                    //}
                    //mouseMoveCounter = ++mouseMoveCounter % (1 + THROW_AWAY_MOUSE_MOVE_EVERY); 
                }
            }
            else if (BaseMouseAction.MouseAction.LeftDown.ToString().Equals(mouseAction.Action)
                && mouseAction is LeftMouseDownAction _leftDown)
            {

                this.mouseControlApi.LeftDown();
            }
            else if (BaseMouseAction.MouseAction.LeftUp.ToString().Equals(mouseAction.Action)
                && mouseAction is LeftMouseUpAction _leftUp)
            {
                this.mouseControlApi.LeftUp();
            }
            else
            {
                throw new NotImplementedException("Not implemented in the ");
            }
        }
    }
}
