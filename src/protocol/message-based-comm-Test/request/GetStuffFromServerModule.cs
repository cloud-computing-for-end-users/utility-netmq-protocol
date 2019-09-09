using message_based_communication.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManualTeSts.request
{
    public class GetStuffFromServerModule : BaseRequest
    {

        public string WhatIWant { get; set; }

        public override string SpecificMethodID => METHOD_ID;
        public const string METHOD_ID = "GET_STUFF_FROM_SM";
    }
}
