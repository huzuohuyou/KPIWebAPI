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
    
    public partial class CPAT_OUT_RECIPE
    {
        public int RECIPE_ID { get; set; }
        public string PATIENT_ID { get; set; }
        public string PATIENT_NO { get; set; }
        public string RECIPE_NO { get; set; }
        public string APPLY_NO { get; set; }
        public Nullable<System.DateTime> RECIPE_DATE { get; set; }
        public Nullable<int> RECIPE_TYPE_CODE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string DEPT_CODE { get; set; }
        public string DEPT_NAME { get; set; }
        public string DOCTOR_NAME { get; set; }
        public string ITEM_GOODS_NAME { get; set; }
        public string GYTJ_CODE { get; set; }
        public string GYTJ_NAME { get; set; }
        public string USAGE_CODE { get; set; }
        public string USAGE_NAME { get; set; }
        public string FREQ_CODE { get; set; }
        public string FREQ_NAME { get; set; }
        public Nullable<decimal> DOSE_ONCE { get; set; }
        public string DOSE_UNIT { get; set; }
        public Nullable<decimal> QTY_TOT { get; set; }
        public string QTY_TOT_UNIT { get; set; }
        public string USE_DAYS { get; set; }
        public string EXEC_DEPT_CODE { get; set; }
        public string EXEC_DEPT_NAME { get; set; }
        public Nullable<System.DateTime> EXEC_DATE { get; set; }
        public Nullable<System.DateTime> UPD_DATE { get; set; }
    }
}
