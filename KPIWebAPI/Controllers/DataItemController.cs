using KPIWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KPIWebAPI.Controllers
{
    [RoutePrefix("formula")]
    public class DataItemController : ApiController, IDataItem
    {
        public Tuple<string, bool> CheckFormula(string script, List<Param> list)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 返回病种下的数据项
        /// </summary>
        /// <param name="sdCode"></param>
        /// <returns></returns>
        [Route("datatiemdict"), HttpPost]
        public List<Param> SDDataItemDict([FromBody]string sdCode)
        {
            try
            {
                List<Param> list = new List<Param>();
                using (var db = new KPIContext())
                {
                    var dataItems = db.SD_ITEM_INFO.ToList().Where(r => r.SD_CODE == sdCode);
                    dataItems.ToList().ForEach(r => { list.Add(new Param() { DataItemId = r.SD_ITEM_ID, Code = r.SD_ITEM_CODE, Name = r.SD_ITEM_NAME, Type = r.ITEM_TYPE_CODE, DataType = r.ITEM_DATA_TYPE }); });
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 返回KPI指标的方法体
        /// </summary>
        /// <param name="kpiId"></param>
        /// <returns></returns>
        [Route("kpibody"), HttpPost]
        public FormulaBody KPIFormulaBody([FromBody]int kpiId)
        {
            try
            {
                using (var db = new KPIContext())
                {
                    JsonConvert.SerializeObject(new EP_KPI_SET());
                    var query = db.EP_KPI_SET.FirstOrDefault(r => r.KPI_ID == kpiId);
                    return new FormulaBody() {Note=query?.KPI_DESC,FenMu=query?.NUM_FORMULA,FenZi=query?.FRA_FORMULA };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取KPI对应的参数
        /// </summary>
        /// <param name="kpiId"></param>
        /// <returns></returns>
        [Route("kpiparam"),HttpPost]
        public List<Param> KPIParams([FromBody]int kpiId)
        {
            try
            {
                List<Param> list = new List<Param>();
                using (var db = new KPIContext())
                {
                    var query = db.EP_KPI_PARAM.ToList().Where(r => r.KPI_ID == kpiId);
                    query.ToList().ForEach(
                        r => {
                            SD_ITEM_INFO sdi = db.SD_ITEM_INFO.ToList().FirstOrDefault(i => i.SD_ITEM_ID == r.SD_ITEM_ID);
                            list.Add(new Param()
                            {
                                Id = r.ID,
                                DataItemId = r.SD_ITEM_ID,
                                Code = sdi.SD_ITEM_CODE,
                                Name = sdi.SD_ITEM_NAME,
                                DataType = sdi.ITEM_DATA_TYPE
                            });
                        }
                        );
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 更新KPI指标方法体
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("kpibody"), HttpPut]
        public int SavaFormulaBody([FromBody]FormulaBody body)
        {
            try
            {
                using (var db = new KPIContext())
                {
                    db.EP_KPI_SET.ToList().Where(r => r.KPI_ID == int.Parse(body.KPIId)).ToList().ForEach(r =>
                    {
                        db.EP_KPI_SET.Remove(r);
                    });
                    db.EP_KPI_SET.Add(new EP_KPI_SET() { KPI_ID = int.Parse(body.KPIId), KPI_DESC = body.Note, NUM_FORMULA = body.FenMu, FRA_FORMULA = body.FenZi });
                    return db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 更新KPI指标算法参数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [Route("kpiparam"), HttpPut]
        public int SaveFormulaParam([FromBody]List<Param> list)
        {
            try
            {
                using (var db = new KPIContext())
                {
                    db.EP_KPI_PARAM.ToList().Where(r => r.KPI_ID == list[0].KPIId).ToList().ForEach(r => {
                        db.EP_KPI_PARAM.Remove(r);
                    });
                    list.ForEach(r =>
                    {
                        db.EP_KPI_PARAM.Add(new EP_KPI_PARAM() { SD_ITEM_ID=r.DataItemId, KPI_PARAM_NAME =r.Code,KPI_ID=r.KPIId});
                    }
                    );
                    return db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ShowDataItemDict(string sdCode)
        {
            throw new NotImplementedException();
        }
    }
}
