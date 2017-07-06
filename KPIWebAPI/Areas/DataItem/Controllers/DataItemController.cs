using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XKPI.Areas.DataItem.Models;

namespace XKPI.Controllers
{
    public class DataItemController : ApiController
    {
        public string Test() {
            return "Hello";
        }

        [Route("ItemFields"), HttpPost]
        public List<SourceItem> Fields()
        {
            return new SourceEntity().Fields;
        }
    }
}
