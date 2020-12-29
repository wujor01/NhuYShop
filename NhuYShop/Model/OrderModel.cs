using System;
using System.Collections.Generic;
using System.Text;

namespace NhuYShop.Model
{
    public class OrderModel
    {
        public string ID { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string customer_province { get; set; }
        public string customer_distrist { get; set; }
        public string customer_ward { get; set; }
        public string customer_street { get; set; }
        public string customer_tel { get; set; }
        public string detail { get; set; }
        public int value { get; set; }
        public bool is_freeship { get; set; }
        public bool is_completed { get; set; }
        public int commission { get; set; }
        public DateTime CREATEDATE { get; set; }
        public string CREATEUSER { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string UPDATEUSER { get; set; }
    }
}
