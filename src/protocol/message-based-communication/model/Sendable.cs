using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public abstract class Sendable
    {
        public CallID CallID { get; set; }
        public ModuleID SenderModuleID { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is Sendable other)
            {
                return
                    CallID.Equals(other.CallID)
                    && SenderModuleID.Equals(other.SenderModuleID)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked( 
                CallID.GetHashCode() * 17 
                + SenderModuleID.GetHashCode() * 17
                );
        }

    }
}
