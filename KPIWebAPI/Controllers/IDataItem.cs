using KPIWebAPI.Models;
using KPIWebAPI.ViewModels;
using System;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    public interface IDataItem
    {
        List<Param> SDDataItemDict(string sdCode);

        List<Param> KPIParams(int kpiId);

        FormulaBody KPIFormulaBody(int kpiId);

        List<Param> SaveFormulaParam(List<Param> list);

        FormulaBody SavaFormulaBody(FormulaBody body);
    }
}
