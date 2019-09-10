using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public class AcknowledgeRecivedSendable : Sendable
    {
        public ModuleID TargetModuleID { get; set; }



        public override bool Equals(object obj)
        {
            if (obj is AcknowledgeRecivedSendable other)
            {
                return
                    base.Equals(other)
                     && TargetModuleID.Equals(other.TargetModuleID)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked(
                base.GetHashCode()
                + TargetModuleID.GetHashCode() * 17
                );
        }
    }
}
