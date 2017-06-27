using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWebAPI.Models
{
    public class Formula
    {
        public List<Param> Param { get; set; }
        public FormulaBody Body { get; set; }

    }
}