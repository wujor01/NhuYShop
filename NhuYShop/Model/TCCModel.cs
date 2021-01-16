using System;
using System.Collections.Generic;
using System.Text;

namespace NhuYShop.Model
{
    public class TCCModel
    {
    }
    public class TCCJobModel
    {
        public string JOBID { get; set; }
        public List<MWGUserModel> USERLIST { get; set; }
        public int VALUE { get; set; }
        public string CONTENT { get; set; }
    }
    public class TCCYecCauModel
    {
        public string MAPHIEUXUAT { get; set; }
        public string MAYEUCAU { get; set; }
        public List<TCCJobModel> JOBLIST { get; set; }
        public string NOTE { get; set; }
        public string ERROR { get; set; }
        public int SUMVALUE { get; set; }
    }
}
