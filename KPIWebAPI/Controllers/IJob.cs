using KPIWebAPI.Models;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    interface ICalKPIJob
    {
        List<dynamic> Run(KpiResultParam kp);
    }
}
