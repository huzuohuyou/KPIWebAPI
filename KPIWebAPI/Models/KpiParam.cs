using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWebAPI.Models
{
    public class KpiParam
    {
        public List<string> PatientList { get; set; }
        public string SdCode { get; set; }
        public string KpiId { get; set; }
        public List<SimpleParam> KParamList{get;set;}
    }
}