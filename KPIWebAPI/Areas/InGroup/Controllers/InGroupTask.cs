using KpiWebApi.Utils;
using KPIWebApi.Models.XKPI;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace KPIWebAPI.Areas.InGroup.Controllers
{
    /// <summary>
    /// 入组任务
    /// </summary>
    public class InGroupTask
    {
        private int eachTimeDoCount;
        private int startNo;
        private int sumCount;

        /// <summary>
        /// 异步灌入mongodb数据
        /// </summary>
        /// <param name="startNo">从第startNo个开始处理记录</param>
        /// <param name="sumCount">本任务处理的记录总数</param>
        /// <param name="eachTimeDoCount">每次处理的记录数</param>
        public InGroupTask(int startNo, int sumCount, int eachTimeDoCount)
        {
            this.eachTimeDoCount = eachTimeDoCount;
            this.startNo = startNo;
            this.sumCount = sumCount;
        }

        public void Do()
        {
            PushAllRecord();
        }

        /// <summary>
        /// 每次按指定个数向mongodb灌入数据
        /// </summary>
        public void PushAllRecord()
        {

            int pageCount = 0,
                remainder,
                statPage = startNo / eachTimeDoCount,
                endPage = 0;
            List<InGroupTask.XKPI> listPat = new List<InGroupTask.XKPI>();
            using (var db = new XKPIContext())
            {
                var patient_nos = db.CPAT_IN_PATIENT.OrderBy(p => p.PATIENT_ID).Skip(startNo).Take(sumCount).GroupBy(p => new { p.PATIENT_NO }).Select(p => p.Key).ToList();
                pageCount = patient_nos.Count / eachTimeDoCount;
                endPage = statPage + pageCount;
                remainder = patient_nos.Count % eachTimeDoCount;
            }
            for (int i = statPage; i < endPage; i++)
            {
                PushPageRecord(i, eachTimeDoCount);
            }
            PushPageRecord(endPage, eachTimeDoCount);
        }

        /// <summary>
        /// 将指定页的数据灌入mongodb
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        public void PushPageRecord(int pageNo, int count)
        {
            List<XKPI> listPat = new List<XKPI>();
            using (var db = new XKPIContext())
            {
                var patient_nos = db.CPAT_IN_PATIENT.OrderBy(p => p.PATIENT_ID).Skip(pageNo * count).Take(count).GroupBy(p => new { p.PATIENT_NO }).Select(p => p.Key).ToList();
                patient_nos.ForEach(g => listPat.Add(PushInMongo(g.PATIENT_NO)));

                var list = new List<WriteModel<XKPI>>();
                foreach (var iitem in listPat)
                {
                    list.Add(new InsertOneModel<XKPI>(iitem));
                }
                MyMongoCollection<XKPI>.GetInstance().InsertManyAsync(listPat);
            }
        }

        /// <summary>
        /// 灌入指定patient_no数据
        /// </summary>
        /// <param name="patient_no"></param>
        /// <returns></returns>
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