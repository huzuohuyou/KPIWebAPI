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
        #region 计算病人KPI，并存库，参数自取

        /// <summary>
        /// 计算病人KPI，并存库，参数自取
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
                    kparam.PatientList.ForEach(
                    p =>
                    {
                        List<ED_KPI_INFO> mlist;
                        if (kparam.KpiId == "")
                        {
                            mlist = db.ED_KPI_INFO.ToList().ToList();
                        }
                        else
                        {
                            mlist = db.ED_KPI_INFO.ToList().Where(k => k.KPI_ID == int.Parse(kparam.KpiId)).ToList();
                        }
                        //kparam.KpiId == "" ? mlist = db.ED_KPI_INFO.ToList().ToList() : mlist = db.ED_KPI_INFO.ToList().Where(k => k.KPI_ID == int.Parse(kparam.KpiId)).ToList();
                        mlist.ForEach(
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
                            result.Add(new { sd_code = kparam.SdCode, kpi_id = r.KPI_ID, patient_id = p, kpi_value = value });
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
        #endregion

        #region 返回KPI结果值，不存库，参数由外部提供


        /// <summary>
        /// 返回KPI结果值，不存库，参数由外部提供
        /// </summary>
        /// <param name="kparam"></param>
        /// <returns></returns>
        [Route("params"), HttpPost]
        public List<dynamic> RunWithParam([FromBody]KpiParam kparam)
        {
            try
            {
                var result = new List<dynamic>();
                using (var db = new KPIContext())
                {
                    kparam.PatientList.ForEach(
                    p =>
                    {
                        List<ED_KPI_INFO> mlist;
                        if (kparam.KpiId == "")
                        {
                            mlist = db.ED_KPI_INFO.ToList().ToList();
                        }
                        else
                        {
                            mlist = db.ED_KPI_INFO.ToList().Where(k => k.KPI_ID == int.Parse(kparam.KpiId)).ToList();
                        }
                        //kparam.KpiId == "" ? mlist = db.ED_KPI_INFO.ToList().ToList() : mlist = db.ED_KPI_INFO.ToList().Where(k => k.KPI_ID == int.Parse(kparam.KpiId)).ToList();
                        mlist.ForEach(
                        r =>
                        {
                            var body = db.EP_KPI_SET.FirstOrDefault(b => b.KPI_ID == r.KPI_ID);
                            var param = db.EP_KPI_PARAM.ToList().Where(b => b.KPI_ID == r.KPI_ID).ToList();
                            KPIFormula formula = new KPIFormula(body, param);
                            UsingPython python = new UsingPython(formula.KPIScript);
                            var value = python.ExcuteScriptFile(kparam.KParamList).ToString();
                            int rr;
                            int.TryParse(value, out rr);
                            result.Add(new { sd_code = kparam.SdCode, kpi_id = r.KPI_ID, patient_id = p, kpi_value = value });
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
        #endregion

        #region 获取参数列表

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="ep_kpi_param_List"></param>
        /// <returns></returns>
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
        #endregion

        #region 获取参数值

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="sd_item_info"></param>
        /// <param name="patient_id"></param>
        /// <returns></returns>
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
        #endregion

        #region 存储KPI值

        /// <summary>
        /// 存储KPI值
        /// </summary>
        /// <param name="value"></param>
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
        #endregion

    }
}
