using KPIWebAPI.Models;
using System;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    public interface IDataItem
    {
        void ShowDataItemDict(string sdCode);
        List<Param> SDDataItemDict(string sdCode);
        List<Param> KPIParams(int kpiId);
        FormulaBody KPIFormulaBody(int kpiId);

        Tuple<string, bool> CheckFormula(string script, List<Param> list);

        int SaveFormulaParam(List<Param> list);

        int SavaFormulaBody(FormulaBody body);
    }
}
