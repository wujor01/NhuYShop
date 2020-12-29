﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NhuYShop.Model.Address
{
    public class Ward
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string name_with_type { get; set; }
        public string path { get; set; }
        public string path_with_type { get; set; }
        public string code { get; set; }
        public string parent_code { get; set; }
    }
}
