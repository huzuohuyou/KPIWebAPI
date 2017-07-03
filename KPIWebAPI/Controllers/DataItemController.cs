using KPIWebApi.Models;
using KPIWebAPI.Models;
using KPIWebAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KPIWebAPI.Controllers
{
    /// <summary>
    /// KPI公式接口API
    /// </summary>
    [RoutePrefix("formula")]
    public class DataItemController : ApiController, IDataItem
    {

        #region 返回病种下的数据项
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
        #endregion

        #region 返回KPI指标的方法体

        
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
        #endregion

        #region 获取KPI对应的参数

        
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
        #endregion

        #region 更新KPI指标方法体

        /// <summary>
        /// 更新KPI指标方法体
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [NonAction]
        public FormulaBody SavaFormulaBody([FromBody]FormulaBody body)
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
                    db.SaveChanges();
                    EP_KPI_SET set = db.EP_KPI_SET.ToList().FirstOrDefault(r => r.KPI_ID == int.Parse(body.KPIId));
                    return new FormulaBody() {KPIId = set.KPI_ID.ToString(),Note = set.KPI_DESC,FenZi=set.NUM_FORMULA,FenMu=set.FRA_FORMULA } ;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 保存KPI算法公式

        /// <summary>
        /// 保存KPI算法公式
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        [Route("current"), HttpPut]
        public Formula SaveFormula([FromBody]Formula f)
        {
            return new Formula() {Param= SaveFormulaParam(f.Param),Body= SavaFormulaBody(f.Body) };
        }
        #endregion

        #region 更新KPI指标算法参数

        /// <summary>
        /// 更新KPI指标算法参数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [NonAction]
        public List<Param> SaveFormulaParam([FromBody]List<Param> list)
        {
            try
            {
                List<Param> result = new List<Param>();
                using (var db = new KPIContext())
                {
                    db.EP_KPI_PARAM.ToList().Where(r => r.KPI_ID == list[0].KPIId).ToList().ForEach(r =>
                    {
                        db.EP_KPI_PARAM.Remove(r);
                    });
                    list.ForEach(r =>
                    {
                        db.EP_KPI_PARAM.Add(new EP_KPI_PARAM() { SD_ITEM_ID = r.DataItemId, KPI_PARAM_NAME = r.Code, KPI_ID = r.KPIId });
                    }
                    );
                    db.SaveChanges();
                    db.EP_KPI_PARAM.ToList().Where(r => r.KPI_ID == list[0].KPIId).ToList().ForEach(
                        r => result.Add(new Param() { KPIId = (int)r.KPI_ID, Code = r.KPI_PARAM_NAME, DataItemId = r.SD_ITEM_ID,Name=r.KPI_PARAM_NAME })
                        );
                    return result;
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
