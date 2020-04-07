using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWebForm
{
    public class TokenResult
    {
        TokenResult()
        {
            result = new Result();
        }
        public string responseCode { get; set; }
        public object description { get; set; }
        public bool status { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
       public   Result()
        {
            billInfo = new Billinfo();
        }
        public string token { get; set; }
        public int initiateTimeStamp { get; set; }
        public int expiryTimeStamp { get; set; }
        public string transactionType { get; set; }
        public Billinfo billInfo { get; set; }
    }

    public class Billinfo
    {
        public object billId { get; set; }
        public object billPaymentId { get; set; }
    }
}