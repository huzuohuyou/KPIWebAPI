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
    
    public partial class TC_IN_GROUP_SETTING
    {
        public int ID { get; set; }
        public string NAMESPACE { get; set; }
        public string CLASS_NAME { get; set; }
        public string FUNC_NAME { get; set; }
        public Nullable<bool> 有效 { get; set; }
        public Nullable<bool> 入库 { get; set; }
        public Nullable<System.DateTime> UDP_DATE { get; set; }
        public string UDP_USER_ID { get; set; }
        public Nullable<int> SD_ID { get; set; }
    
        public virtual TC_SD_INFO TC_SD_INFO { get; set; }
    }
}
