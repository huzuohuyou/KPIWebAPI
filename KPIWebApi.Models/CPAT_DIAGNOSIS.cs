//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XKPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CPAT_DIAGNOSIS
    {
        public int DIAG_ID { get; set; }
        public string PATIENT_NO { get; set; }
        public string IN_FLAG { get; set; }
        public string PATIENT_ID { get; set; }
        public Nullable<int> DIAG_TYPE_CODE { get; set; }
        public Nullable<int> DIAG_NO { get; set; }
        public string DIAG_CODE { get; set; }
        public string DIAG_NAME { get; set; }
        public Nullable<System.DateTime> DIAG_DATE { get; set; }
        public string TREAT_RESULT { get; set; }
        public Nullable<System.DateTime> UPD_DATE { get; set; }
    }
}
