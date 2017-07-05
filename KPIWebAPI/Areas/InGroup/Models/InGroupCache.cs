using KPIWebApi.Models;
using KPIWebApi.Models.XKPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWebAPI.Areas.InGroup.Models
{
    public class InGroupCache
    {
        public List<CPAT_IN_PATIENT> List_CPAT_IN_PATIENT { get; }
        public List<CPAT_CHECK_RECORD> List_CPAT_CHECK_RECORD { get; }
        public List<CPAT_DIAGNOSIS> List_CPAT_DIAGNOSIS { get; }
        public List<CPAT_EMR_RECORD> List_CPAT_EMR_RECORD { get; }
        public List<CPAT_IN_ORDERS> List_CPAT_IN_ORDERS { get; }
        public List<CPAT_OUT_EMR> List_CPAT_OUT_EMR { get; }
        public List<CPAT_OUT_PATIENT> List_CPAT_OUT_PATIENT { get; }
        public List<CPAT_OUT_RECIPE> List_CPAT_OUT_RECIPE { get; }
        public List<CPAT_PATHOLOGY_RECORD> List_CPAT_PATHOLOGY_RECORD { get; }
        public List<CPAT_TEST_RECORD> List_CPAT_TEST_RECORD { get; }
        public List<CPAT_TEST_RESULT> List_CPAT_TEST_RESULT { get; }
        public List<CPAT_TEST_RESULT_GERM> List_CPAT_TEST_RESULT_GERM { get; }
        //private static InGroupCache cache = new InGroupCache();
        private InGroupCache(List<string> pat_no)
        {
            using (var db = new XKPIContext())
            {
                List_CPAT_IN_PATIENT =db.CPAT_IN_PATIENT.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_CHECK_RECORD = db.CPAT_CHECK_RECORD.Where(r => pat_no.Contains(r.PATIENT_NO) ).ToList();
                List_CPAT_DIAGNOSIS = db.CPAT_DIAGNOSIS.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_EMR_RECORD = db.CPAT_EMR_RECORD.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_IN_ORDERS = db.CPAT_IN_ORDERS.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_OUT_EMR = db.CPAT_OUT_EMR.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_OUT_PATIENT = db.CPAT_OUT_PATIENT.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_OUT_RECIPE = db.CPAT_OUT_RECIPE.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_PATHOLOGY_RECORD = db.CPAT_PATHOLOGY_RECORD.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_TEST_RECORD = db.CPAT_TEST_RECORD.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_TEST_RESULT = db.CPAT_TEST_RESULT.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
                List_CPAT_TEST_RESULT_GERM = db.CPAT_TEST_RESULT_GERM.Where(r => pat_no.Contains(r.PATIENT_NO)).ToList();
            }
        }

        //public static InGroupCache GetInstance()
        //{
            //if (cache == null)
            //{
            //    cache = new InGroupCache();
            //}
            //return cache;
        //}
    }
}