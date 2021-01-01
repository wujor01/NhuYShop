using System;
using System.Collections.Generic;
using System.Text;

namespace NhuYShop.Model
{
    public class UserModel
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string SOCIAL { get; set; }
        public string PHONE { get; set; }
        public DateTime CREATEDATE { get; set; }
        public string CREATEUSER { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string UPDATEUSER { get; set; }
        public string PASSWORD { get; set; }
        public bool ISHASH { get; set; }
        public bool ISDELETED { get; set; }
        public int COUNTORDER { get; set; }
        public int VALUEORDER { get; set; }
        public int COMISSION { get; set; }
    }
}
