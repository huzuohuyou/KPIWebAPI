using KPIWebApi.Models;
using KPIWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using XKPI.Models;

namespace KPIWebAPI.ViewModels
{
    public class KPIFormula
    {
        public int KPI_ID { get { return (int)ep_kpi_set.KPI_ID; }  }
        //public string SD_CODE { get { return ep_kpi_set.; } }
        public string KPI_TYPE_CODE { get; set; }
        public string KPI_NAME { get; set; }
        public int? Status { get; set; }
        private EP_KPI_SET ep_kpi_set {  get;  set; }
        private List<EP_KPI_PARAM> paramList {  get;  set; }
        public KPIFormula(EP_KPI_SET body, List<EP_KPI_PARAM> pList)
        {
            ep_kpi_set = body;
            paramList = pList;
        }
        public string KPIScript
        {
            get
            {
                try
                {
                    if (ep_kpi_set==null)
                    {
                        throw new Exception("null kpi script exist!!!");
                    }
                    string param = string.Empty, body = string.Empty, note = string.Empty;
                    if (ep_kpi_set.NUM_FORMULA == string.Empty)
                    {
                        body += string.Format(@"result={0}", ep_kpi_set.FRA_FORMULA.Trim());
                    }
                    else
                    {
                        body = string.Format(@"if(({0})==1):
    result={1}", ep_kpi_set.NUM_FORMULA.Trim(), ep_kpi_set.FRA_FORMULA.Trim());
                    }
                    body = string.Format("\n{0}", body);
                    note = string.Format("{0}\n", ep_kpi_set.KPI_DESC);

                    using (var db = new KPIContext())
                    {
                        paramList.ForEach(r =>
                        {
                            param += string.Format("{0}\n", r.KPI_PARAM_NAME.Trim());
                            var dataItem = db.SD_ITEM_INFO.FirstOrDefault(i => i.SD_ITEM_ID == r.SD_ITEM_ID);
                            note += string.Format("编码：{0} 名称：{1} 数据类型：{2}\n", dataItem.SD_ITEM_CODE.Trim(), dataItem.SD_ITEM_NAME.Trim(), dataItem.ITEM_DATA_TYPE.Trim());
                        });
                    }
                    return string.Format("'''{2}'''\n{0}{1}", param, body, note);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
