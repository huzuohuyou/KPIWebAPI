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
    
    public partial class CPAT_EMR_RECORD
    {
        public int EMR_REC_ID { get; set; }
        public string PATIENT_ID { get; set; }
        public string PATIENT_NO { get; set; }
        public string EMR_TYPE_CODE { get; set; }
        public string EMR_TYPE_NAME { get; set; }
        public string LGCY_TYPE_CODE { get; set; }
        public string LGCY_TYPE_NAME { get; set; }
        public string LGCY_SUB_TYPE_CODE { get; set; }
        public string LGCY_SUB_TYPE_NAME { get; set; }
        public string REC_CONTENT { get; set; }
        public string REC_CONTENT_FM { get; set; }
        public Nullable<int> REC_TYPE { get; set; }
        public string CREATOR_CODE { get; set; }
        public string CREATOR_NAME { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string REC_PSN_CODE { get; set; }
        public string REC_PSN_NAME { get; set; }
        public Nullable<System.DateTime> REC_DATE { get; set; }
        public string REC_DEPT_CODE { get; set; }
        public string REC_DEPT_NAME { get; set; }
        public string PARENT_CODE { get; set; }
        public string PARENT_NAME { get; set; }
        public string DIRECTOR_CODE { get; set; }
        public string DIRECTOR_NAME { get; set; }
        public Nullable<System.DateTime> UPD_DATE { get; set; }
    }
}
