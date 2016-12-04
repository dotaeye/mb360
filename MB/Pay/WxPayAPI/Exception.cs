using System;
using System.Collections.Generic;
using System.Web;

namespace MB.Pay.WxPayAPI
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}