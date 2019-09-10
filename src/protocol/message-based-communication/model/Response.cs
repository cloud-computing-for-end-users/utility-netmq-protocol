using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public sealed class Response : Sendable
    {
        public ModuleID TargetModuleID { get; set; }
        public Payload Payload { get; set; }




        public override bool Equals(object obj)
        {
            if (obj is Response other)
            {
                return
                    TargetModuleID.Equals(other.TargetModuleID)
                    && base.Equals(other)
                    && Payload.Equals(other.Payload)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked(
                base.GetHashCode() * 17
                + TargetModuleID.GetHashCode() * 17 
                + Payload.GetHashCode() * 17
                );
        }
    }
}
