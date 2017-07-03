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
    
    public partial class CPAT_OUT_PATIENT
    {
        public string PATIENT_NO { get; set; }
        public string PATIENT_ID { get; set; }
        public string CASE_NO { get; set; }
        public string CARD_NO { get; set; }
        public string PATIENT_NAME { get; set; }
        public string ID_NO { get; set; }
        public Nullable<System.DateTime> BIRTH_DATE { get; set; }
        public Nullable<int> GENDER_CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string NATION_NAME { get; set; }
        public Nullable<int> MARI_STAT_CODE { get; set; }
        public string CHARGE_TYPE_CODE { get; set; }
        public string CHARGE_TYPE_NAME { get; set; }
        public System.DateTime REG_DATE { get; set; }
        public string REG_DEPT_CODE { get; set; }
        public string REG_DEPT_NAME { get; set; }
        public string REG_LEVEL_CODE { get; set; }
        public string REG_LEVEL_NAME { get; set; }
        public string SEE_NO { get; set; }
        public Nullable<System.DateTime> SEE_DATE { get; set; }
        public string DOCTOR_CODE { get; set; }
        public string DOCTOR_NAME { get; set; }
        public string DIAG_CODE { get; set; }
        public string DIAG_NAME { get; set; }
        public Nullable<System.DateTime> DIAG_DATE { get; set; }
        public Nullable<int> FIRST_VISIT_FLAG { get; set; }
        public Nullable<System.DateTime> UPD_DATE { get; set; }
    }
}
