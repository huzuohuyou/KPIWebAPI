using System.Collections.Generic;
using System.Linq;
using KPIWebAPI.ViewModels;
using XKPI.Models;
using KPIWebAPI.Models;

namespace XKPI.Utils
{
    class UPDemo : AbsAssembleFormula
    {
        public UPDemo(int kpiid):base(kpiid) { }
        public override KPIFormula AssembleFormula()
        {
            using (var db = new KPIContext())
            {
                EP_KPI_SET body = db.EP_KPI_SET.FirstOrDefault(r => r.KPI_ID == KPI_ID);
                List <EP_KPI_PARAM> list = db.EP_KPI_PARAM.Where(r => r.KPI_ID == KPI_ID).ToList();
                return new KPIFormula(body, list);
            }
        }
    }
}
