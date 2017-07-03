using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWebAPI.ViewModels
{
    public class SimpleParam
    {
        public string Code { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
        public dynamic FixValue
        {
            get
            {
                if (DataType == "int")
                {
                    return Convert.ToInt32(Value);
                }
                else if (DataType == "double")
                {
                    return Convert.ToDouble(Value);
                }
                else if (DataType == "datetime")
                {
                    return Convert.ToDateTime(Value);
                }
                else
                {
                    return Value;
                }
            }
        }
    }
}