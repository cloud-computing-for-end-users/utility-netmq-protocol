using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    public abstract class BaseRequest : Sendable
    {
        public ModuleType TargetModuleType { get; set; }
        public abstract string SpecificMethodID { get; }


        public override bool Equals(object obj)
        {
            if (obj is BaseRequest other)
            {
                return
                    base.Equals(other)
                     && TargetModuleType.Equals(other.TargetModuleType)
                    && SpecificMethodID.Equals(other.SpecificMethodID)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return unchecked(
                base.GetHashCode()
                + TargetModuleType.GetHashCode() * 17
                + SpecificMethodID.GetHashCode() * 17
                );
        }

    }
}
