using System.Collections.Generic;

namespace KPIWebAPI.ViewModels
{
    public class KpiParam
    {
        public List<string> PatientList { get; set; }
        public string SdCode { get; set; }
        public string KpiId { get; set; }
        public List<SimpleParam> KParamList{get;set;}
    }
}