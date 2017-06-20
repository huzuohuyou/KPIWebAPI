using KPIWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KPIWebAPI.Controllers
{
    /// <summary>
    /// 计算KPI结果API
    /// </summary>
    [RoutePrefix("kpiresult")]
    public class KPIResultController : ApiController, ICalKPIJob
    {
        /// <summary>
        /// 计算病人KPI
        /// </summary>
        /// <param kparam="sdCode">参数实体</param>
        /// <returns></returns>
        [Route("sdcode"),HttpPost]
        public List<dynamic> Run([FromBody]KpiResultParam kparam)
        {
            try
            {
                var result = new List<dynamic>();
                using (var db = new KPIContext())
                {
                    kparam.PatientList.ForEach(p =>
                    {
                        db.ED_KPI_INFO.ToList().ForEach(
                        r =>
                        {
                            var body = db.EP_KPI_SET.FirstOrDefault(b => b.KPI_ID == r.KPI_ID);
                            var param = db.EP_KPI_PARAM.ToList().Where(b => b.KPI_ID == r.KPI_ID).ToList();
                            KPIFormula formula = new KPIFormula(body, param);
                            UsingPython python = new UsingPython(formula.KPIScript);
                            var value = python.ExcuteScriptFile(GetParamList(p, param)).ToString();
                            int rr;
                            int.TryParse(value, out rr);
                            StoreKPI(new ED_KPI_VALUE() { KPI_ID = r.KPI_ID, SD_CPAT_NO = p, INDEX_VALUE = rr });
                            result.Add(new { sd_code = kparam.SdCode, kpi_id = r.KPI_ID, patient_id = p, kpi_value = value } );
                            //存库.. 
                        }
                        );
                    }
                    );
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [NonAction]
        public List<Param> GetParamList(string patientId, List<EP_KPI_PARAM> ep_kpi_param_List)
        {
            List<Param> result = new List<Param>();
            ep_kpi_param_List.ForEach(
                r =>
                {
                    using (var db = new KPIContext())
                    {
                        var sd_item_info = db.SD_ITEM_INFO.FirstOrDefault(m => m.SD_ITEM_ID == r.SD_ITEM_ID);
                        result.Add(new Param()
                        {
                            Code = sd_item_info.SD_ITEM_CODE,
                            Name = sd_item_info.SD_ITEM_CODE,
                            DataType = sd_item_info.ITEM_DATA_TYPE,
                            Value = GetParamValue(sd_item_info, patientId)
                        });
                    }
                }
                );
            return result;
        }

        [NonAction]
        public dynamic GetParamValue(SD_ITEM_INFO sd_item_info, string patient_id)
        {
            using (var db = new KPIContext())
            {
                return db.PAT_SD_ITEM_RESULT.FirstOrDefault(r =>
                r.SD_CODE == sd_item_info.SD_CODE.Trim()
                && r.SD_ITEM_CODE == sd_item_info.SD_ITEM_CODE.Trim()
                && r.PATIENT_ID == patient_id)?.SD_ITEM_VALUE;
            }
        }

        [NonAction]
        private void StoreKPI(ED_KPI_VALUE value)
        {
            try
            {
                using (var db = new KPIContext())
                {
                    db.ED_KPI_VALUE.Add(value);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
