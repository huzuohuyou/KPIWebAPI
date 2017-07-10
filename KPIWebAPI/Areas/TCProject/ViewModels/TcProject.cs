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
        public string CDRSatus { get; }
        public string SDRStatus { get; }
        public string CrateTime { get; set; }
    }
}