using KPIWebAPI.Models;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    interface ICalKPIJob
    {
        List<string> Run(KpiResultParam kp);
    }
}
