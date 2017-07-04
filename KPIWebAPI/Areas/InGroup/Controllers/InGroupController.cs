using KPIWebApi.Models.XKPI;
using System.Linq;
using System.Web.Http;
using System.Dynamic;
using FrameWork;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace KPIWebAPI.Areas.InGroup.Controllers
{
    [RoutePrefix("group")]
    public class InGroupController : ApiController
    {
        /// <summary>
        /// 按指定个数向mongodb灌入数据
        /// </summary>
        /// <param name="count"></param>
        [Route("all"), HttpPost]
        public void PushAllRecord(int count)
        {
            int pageNo = 0, remainder;
            List<XKPI> listPat = new List<XKPI>();
            using (var db = new XKPIContext())
            {
                var patient_nos = db.CPAT_IN_PATIENT.ToList();
                pageNo = patient_nos.Count / count;
                remainder = patient_nos.Count % count;
            }
            for (int i = 1; i < pageNo + 1; i++)
            {
                PushPageRecord(i, count);
            }
            PushPageRecord(pageNo + 1, remainder);
        }

        /// <summary>
        /// 将指定页的数据灌入mongodb
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        [Route("page"), HttpPost]
        public void PushPageRecord(int pageNo, int count)
        {
            List<XKPI> listPat = new List<XKPI>();
            using (var db = new XKPIContext())
            {
                var patient_nos = db.CPAT_IN_PATIENT.OrderBy(p => p.PATIENT_ID).Skip((pageNo - 1) * count).Take(count).GroupBy(p => new { p.PATIENT_NO }).Select(p => p.Key).ToList();
                patient_nos.ForEach(g => listPat.Add(PushInMongo(g.PATIENT_NO)));

                var list = new List<WriteModel<XKPI>>();
                foreach (var iitem in listPat)
                {
                    list.Add(new InsertOneModel<XKPI>(iitem));
                }
                MongoCollection<XKPI>.GetInstance().InsertManyAsync(listPat);//.InsertOne(x);
                //ObjectId d = x._id;
            }
        }

        /// <summary>
        /// 灌入指定patient_no数据
        /// </summary>
        /// <param name="patient_no"></param>
        /// <returns></returns>
        [Route("one"), HttpPost]
        public XKPI PushInMongo(string patient_no)
        {
            try
            {
                using (var db = new XKPIContext())
                {
                    dynamic patient = new ExpandoObject();
                    var in_pat = db.CPAT_IN_PATIENT.ToList().FirstOrDefault(r => r.PATIENT_NO == patient_no);
                    patient.CPAT_IN_PATIENT = in_pat;
                    var check_order = db.CPAT_CHECK_RECORD.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_CHECK_RECORD = check_order;
                    var diagnosis = db.CPAT_DIAGNOSIS.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_DIAGNOSIS = diagnosis;
                    var emr_record = db.CPAT_EMR_RECORD.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_EMR_RECORD = emr_record;
                    var in_orders = db.CPAT_IN_ORDERS.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_IN_ORDERS = in_orders;
                    var out_emr = db.CPAT_OUT_EMR.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_OUT_EMR = out_emr;
                    var out_pat = db.CPAT_OUT_PATIENT.FirstOrDefault(r => r.PATIENT_NO == patient_no);
                    patient.CPAT_OUT_PATIENT = out_pat;
                    var out_recipe = db.CPAT_OUT_RECIPE.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_OUT_RECIPE = out_recipe;
                    var phy_record = db.CPAT_PATHOLOGY_RECORD.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_PATHOLOGY_RECORD = phy_record;
                    var test_record = db.CPAT_TEST_RECORD.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_TEST_RECORD = test_record;
                    var test_result = db.CPAT_TEST_RESULT.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_TEST_RESULT = test_result;
                    var test_result_germ = db.CPAT_TEST_RESULT_GERM.Where(r => r.PATIENT_NO == patient_no).ToList();
                    patient.CPAT_TEST_RESULT_GERM = test_result_germ;
                    XKPI x = new XKPI() { data = patient };
                    return x;
                    //MongoCollection<XKPI>.GetInstance().InsertOne(x);
                    //ObjectId d = x._id;
                }



            }
            catch (System.Exception ex)
            {

                throw ex;
            }


        }
        public class XKPI
        {
            public ObjectId _id;//BsonType.ObjectId 这个对应了 MongoDB.Bson.ObjectId 
            public dynamic data;
        }
    }
}
