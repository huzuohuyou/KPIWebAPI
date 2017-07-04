using KPIWebApi.Models.XKPI;
using System.Linq;
using System.Web.Http;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPIWebAPI.Areas.InGroup.Controllers
{
    [RoutePrefix("group")]
    public class InGroupController : ApiController
    {
        /// <summary>
        /// 多线程灌入mongodb
        /// </summary>
        /// <param name="taskCount">任务数</param>
        /// /// <param name="eachTimeDoCount">每个任务每次处理记录条数</param>
        [Route("task"), HttpPost]
        public void PushTaskRecord(int taskCount, int eachTimeDoCount)
        {
            int  remainder,sumPages,taskDoPages,remainderPages;
            List<InGroupTask.XKPI> listPat = new List<InGroupTask.XKPI>();
            using (var db = new XKPIContext())
            {
                var patient_nos = db.CPAT_IN_PATIENT.ToList();
                //taskDoCount = patient_nos.Count / taskCount;
                sumPages = patient_nos.Count / eachTimeDoCount;
                taskDoPages = sumPages / taskCount;
                remainderPages = sumPages % taskCount;

                remainder = patient_nos.Count % taskCount;
            }

            for (int i = 0; i < taskCount; i++)
            {
                InGroupTask tas = new InGroupTask(i * taskDoPages * eachTimeDoCount, taskDoPages * eachTimeDoCount, eachTimeDoCount);
                Task.Factory.StartNew(tas.Do);
            }
            InGroupTask ta = new InGroupTask(taskDoPages * taskCount * eachTimeDoCount, remainderPages * eachTimeDoCount + remainder, eachTimeDoCount);
            Task.Factory.StartNew(ta.Do);
        }

       

        
    }
}
