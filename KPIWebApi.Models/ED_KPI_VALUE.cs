//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KPIWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ED_KPI_VALUE
    {
        public int ID { get; set; }
        public string SD_CPAT_NO { get; set; }
        public Nullable<int> KPI_ID { get; set; }
        public Nullable<int> INDEX_VALUE { get; set; }
        public Nullable<System.DateTime> UPD_DATE { get; set; }
    
        public virtual ED_KPI_INFO ED_KPI_INFO { get; set; }
        public virtual SD_CPATS SD_CPATS { get; set; }
    }
}
