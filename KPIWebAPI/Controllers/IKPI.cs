using KPIWebAPI.Models;
using KPIWebAPI.ViewModels;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    public interface IKPI
    {
        List<KPINode> GetKPIList();

        KPINode KpiScript(int kpiId);
        
    }
}
