using KPIWebAPI.ViewModels;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    interface ICalKPIJob
    {
        List<dynamic> Run(KpiResultParam kp);
    }
}
