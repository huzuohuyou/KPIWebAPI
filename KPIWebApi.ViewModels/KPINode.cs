using KPIWebAPI.Models;
using System;
using System.Linq;

namespace KPIWebAPI.ViewModels
{
    public class KPINode
    {
        public int KPI_ID { get; set; }
        public string SD_CODE { get; set; }
        public string KPI_TYPE_CODE { get; set; }
        public string KPI_NAME { get; set; }
        public int? Status { get; set; }
        public string ScriptString
        {
            get
            {
                try
                {
                    string param = string.Empty, body = string.Empty, note = string.Empty;
                    using (var db = new KPIContext())
                    {
                        var formula = db.EP_KPI_SET.FirstOrDefault(r => r.KPI_ID == KPI_ID);
                        if (formula.NUM_FORMULA == string.Empty)
                        {
                            body += string.Format(@"result={0}", formula.FRA_FORMULA.Trim());
                        }
                        else
                        {
                            body = string.Format(@"if(({0})==1):
    result={1}", formula.NUM_FORMULA.Trim(), formula.FRA_FORMULA.Trim());
                        }
                        body = string.Format("\n{0}",  body);
                        note = string.Format("{0}\n", formula.KPI_DESC);
                    }

                    using (var db = new KPIContext())
                    {
                        var pyParams = db.EP_KPI_PARAM.ToList().Where(r => r.KPI_ID == KPI_ID);
                        pyParams.ToList().ForEach(r => {
                            param += string.Format("{0}\n", r.KPI_PARAM_NAME.Trim());
                            var dataItem = db.SD_ITEM_INFO.FirstOrDefault(i => i.SD_ITEM_ID == r.SD_ITEM_ID);
                            note += string.Format("编码：{0} 名称：{1} 数据类型：{2}\n", dataItem.SD_ITEM_CODE.Trim(), dataItem.SD_ITEM_NAME.Trim(), dataItem.ITEM_DATA_TYPE.Trim());
                        });
                    }
                    return string.Format("'''\n{2}'''\n{0}{1}", param, body,note);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }
    }
}
