//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KPIWebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class SD_ITEM_INFO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SD_ITEM_INFO()
        {
            this.EP_KPI_PARAM = new HashSet<EP_KPI_PARAM>();
        }
    
        public int SD_ITEM_ID { get; set; }
        public string SD_CODE { get; set; }
        public string SD_ITEM_CODE { get; set; }
        public string SD_ITEM_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string SD_ITEM_ALIAS { get; set; }
        public Nullable<int> ORDER_NO { get; set; }
        public string ITEM_TYPE_CODE { get; set; }
        public string ITEM_DATA_TYPE { get; set; }
        public string ITEM_UNIT { get; set; }
        public string UPD_USER_ID { get; set; }
        public Nullable<System.DateTime> UPD_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EP_KPI_PARAM> EP_KPI_PARAM { get; set; }
        public virtual SD_INFO SD_INFO { get; set; }
    }
}
