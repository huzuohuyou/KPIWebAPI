using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KPIWebAPI.Models;

namespace KPIWebAPI.Controllers
{
    /// <summary>
    /// 指标API
    /// </summary>
    [RoutePrefix("kpi")]
    public class KPIController : ApiController, IKPI
    {

        /// <summary>
        /// 获取KPI算法列表
        /// </summary>
        /// <returns></returns>
        [Route("all")]
        public List<KPINode> GetKPIList()
        {
            List<KPINode> list = new List<KPINode>();
            using (var db = new KPIContext())
            {
                foreach (var item in db.ED_KPI_INFO)
                {
                    list.Add(new KPINode()
                    {
                        KPI_ID = item.KPI_ID,
                        SD_CODE = item.SD_CODE,
                        KPI_TYPE_CODE = item.KPI_TYPE_CODE,
                        KPI_NAME = item.KPI_NAME,
                        Status = db.EP_KPI_SET.FirstOrDefault(r => r.KPI_ID == item.KPI_ID)?.INVALID_FLAG
                    });
                }

            }
            return list;
        }

        /// <summary>
        /// 通过KPIId获取对应脚本
        /// </summary>
        /// <param name="kpiId"></param>
        /// <returns></returns>
        [Route("script"),HttpPost]
        public KPINode KpiScript([FromBody]int kpiId)
        {
            try
            {
                using (var db = new KPIContext())
                {
                    var item = db.ED_KPI_INFO.ToList().FirstOrDefault(r => r.KPI_ID == kpiId);
                    return new KPINode()
                    {
                        KPI_ID = item.KPI_ID,
                        SD_CODE = item.SD_CODE,
                        KPI_TYPE_CODE = item.KPI_TYPE_CODE,
                        KPI_NAME = item.KPI_NAME,
                        Status = db.EP_KPI_SET.FirstOrDefault(r => r.KPI_ID == item.KPI_ID)?.INVALID_FLAG
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
