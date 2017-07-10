using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWebAPI.Areas.TCProject.Models
{
    public class ProjectPage
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 本页显示条数
        /// </summary>
        public int Count { get; set; }
    }
}