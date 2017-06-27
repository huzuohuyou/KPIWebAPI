using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KPIWebAPI.Controllers
{
    [RoutePrefix("app")]
    public class APPController : ApiController
    {
        [Route("baseinfo"), HttpGet]
        public string GetInfo()
        {
            return "欢迎使用Formula Editor";
        }
    }
}
