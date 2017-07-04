using KPIWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPIWebApi.Utils
{
    public interface ICanAssembleFormula
    {
        KPIFormula AssembleFormula(int kpiid);
    }
}
