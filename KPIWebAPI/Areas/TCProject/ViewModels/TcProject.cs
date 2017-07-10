using System;

namespace KPIWebAPI.Areas.TCProject.ViewModels
{
    public class TcProject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CDRIP { get; set; }
        public string CDRUName { get; set; }
        public string CDRPWD { get; set; }
        public string SDRIP { get; set; }
        public string SDRUName { get; set; }
        public string SDRPWD { get; set; }
        public DateTime Cdate { get; set; }
        public string CDRStatus { get; }
        public string SDRStatus { get; }
        public string CrateTime { get; set; }
    }
}