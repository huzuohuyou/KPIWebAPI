﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KPIWebAPI.Models;

namespace KPIWebAPI.Controllers
{
    [RoutePrefix("kpi")]
    public class KPIController : ApiController, IKPI
    {
        [Route("all")]
        /// <summary>
        /// 获取KPI算法列表
        /// </summary>
        /// <returns></returns>
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

        [Route("all")]
        public List<Param> GetKPIParams(int kpiId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 刷新KPI列表
        /// </summary>
        public void RefreshKPIList()
        {
            throw new NotImplementedException();
        }
    }
}