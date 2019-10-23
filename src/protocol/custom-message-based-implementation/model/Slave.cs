using System;
using System.Collections.Generic;
using System.Text;

namespace custom_message_based_implementation.model
{
    public class Slave
    {
        public SlaveConnection SlaveConnection { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion{ get; set; }
        public string OperatingSystemName { get; set; }


        public static Slave FromJSON(string jsonString)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Slave>(jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception when parsing JSON TO SLAVE" + e.Message);
                throw e;
            }
        }

        public string ToJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
