using System;
using System.Collections.Generic;
using System.Text;

namespace NhuYShop.Model
{
    public class MWGModel
    {
        public List<MWGUserModel> Sheet1 { get; set; }
    }
    public class MWGUserModel
    {
        public string USERID { get; set; }
        public string FULLNAME { get; set; }
        public string STOREID { get; set; }
        public string STORENAME { get; set; }
        public string PHONGBAN { get; set; }
    }
}
