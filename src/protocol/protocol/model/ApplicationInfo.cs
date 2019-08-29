using System;
using System.Collections.Generic;
using System.Text;

namespace protocol.model
{
    public class ApplicationInfo
    {
        public enum SupportedApplication
        {
            Word,
            Excel
        }

        public SupportedApplication Application { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is ApplicationInfo _obj)
            {
                return this.Application.Equals(_obj.Application)
                    ;

            }
            return false;
        }
    }
}
