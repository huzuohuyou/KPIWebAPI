using KPIWebAPI.Models;
using System;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    public interface IDataItem
    {
        List<Param> SDDataItemDict(string sdCode);

        List<Param> KPIParams(int kpiId);

        FormulaBody KPIFormulaBody(int kpiId);

        int SaveFormulaParam(List<Param> list);

        int SavaFormulaBody(FormulaBody body);
    }
}
