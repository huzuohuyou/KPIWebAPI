using KPIWebAPI.ViewModels;
using System.Collections.Generic;

namespace KPIWebAPI.ViewModels
{
    public class Formula
    {
        public List<Param> Param { get; set; }
        public FormulaBody Body { get; set; }

    }
}