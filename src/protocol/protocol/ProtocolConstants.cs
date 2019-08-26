﻿using System;
using System.Collections.Generic;
using System.Text;

namespace net_mq_util
{
    public class ProtocolConstants
    {

        //general constants
        public const int MINIMUM_FRAMES_IN_MESSAGE = 1;




        //----------steps to fully add a method call to the protocol----------
        //1. add a static readonly string below
        //2. add the string with the intented target to the dictionary in "NetMqUtil"
        //3. create a class that inherits from the super class of the correct target type, 
        //      this class must have the parameters that is nessesary for the method call, and be called param1, param2 ...
        //4. create a protected method in "NetMqEncoder" that can generate a NetMqMessage with the right structure.
        //5. add the newly created method to the switch like if else statements in the correct Encode method
        //6. as in 4. but for the decoder "NetMqDecoder"
        //7. as in 5 but for the decoder.



        //----------------------NAMING CONVENTION----------------------
        //all methods starts with MET_
        //method with target file servermodule then follows with "FILE_" 
        //method with target slave owner servermodule then follows with "SO_" 
        //method with target database servermodule then follows with "DB_" 
        //method with target server module then follows with "SM_" 
        //all methods ends with the method name

        //all methods defined for the protocol have a const string here
        public static readonly string MET_SM_HelloWorld = "1HelloWorld";



    }
}
