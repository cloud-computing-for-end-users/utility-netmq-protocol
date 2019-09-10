using System;
using System.Collections.Generic;
using System.Text;

namespace message_based_communication.model
{
    /// <summary>
    /// this class should not be instansiated by the user of this library, but insteas be inherrited from
    /// </summary>
    public class ModuleType
    {
        public string TypeID{ get; set; }


        public override bool Equals(object obj)
        {
            if (obj is ModuleType other)
            {
                return
                    TypeID.Equals(other.TypeID)
                    ;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return
                unchecked(
                TypeID.GetHashCode() * 17
                );
        }

    }
}
