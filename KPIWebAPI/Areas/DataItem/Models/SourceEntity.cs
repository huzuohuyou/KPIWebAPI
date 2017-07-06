using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using XKPI.Models;

namespace XKPI.Areas.DataItem.Models
{
    public class SourceEntity
    {
        public CPAT_IN_PATIENT LCPAT_IN_PATIENT { get; }
        public CPAT_CHECK_RECORD CPAT_CHECK_RECORD { get; }
        public CPAT_DIAGNOSIS CPAT_DIAGNOSIS { get; }
        public CPAT_EMR_RECORD CPAT_EMR_RECORD { get; }
        public CPAT_IN_ORDERS CPAT_IN_ORDERS { get; }
        public CPAT_OUT_EMR CPAT_OUT_EMR { get; }
        public CPAT_OUT_PATIENT CPAT_OUT_PATIENT { get; }
        public CPAT_OUT_RECIPE CPAT_OUT_RECIPE { get; }
        public CPAT_PATHOLOGY_RECORD CPAT_PATHOLOGY_RECORD { get; }
        public CPAT_TEST_RECORD CPAT_TEST_RECORD { get; }
        public CPAT_TEST_RESULT CPAT_TEST_RESULT { get; }
        public CPAT_TEST_RESULT_GERM CPAT_TEST_RESULT_GERM { get; }

        public string ClearName(string dirtyName)
        {
            Match mt = Regex.Match(dirtyName, "(?<txt>(?<=<).+(?=>))");
            return mt.Value;
        }

        public List<SourceItem> Fields
        {
            get
            {
                List<SourceItem> list = new List<SourceItem>();
                Type setType = typeof(SourceEntity);
                FieldInfo[] fields = setType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (FieldInfo fi in fields)
                {
                    if (fi.FieldType.IsClass)
                    {
                        Assembly objAssembly = Assembly.Load("XKPI.Models");
                        Type fieldType = objAssembly.GetType(fi.FieldType.FullName, false, true);
                        FieldInfo[] itemFi = fieldType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                        foreach (var item in itemFi)
                        {
                            SourceItem si = new SourceItem() {ItemName= ClearName(item.Name), ItemType = item.FieldType.Name, ItemKind = fi.FieldType.Name };
                            list.Add(si);
                        }
                    }
                    //Type tt = fi.FieldType;
                    //list.Add(fi.FieldType.Name);
                }
                return list;
            }
        }
    }
}