using IronPython.Hosting;
using KPIWebApi.Utils;
using KPIWebAPI.ViewModels;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using XKPI.Utils;

namespace KPIWebAPI.Utils
{
    public class UsingPython
    {
        ScriptEngine engine ;
        ScriptScope scope ;
        ScriptSource source;
        private UsingPython() { }

        /// <summary>
        /// 传参公式字符串初始化引擎
        /// </summary>
        /// <param name="ScriptString"></param>

        protected UsingPython(string ScriptString)
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();
            source = engine.CreateScriptSourceFromString(ScriptString);
        }

        /// <summary>
        /// 传参公式实体初始化引擎
        /// </summary>
        /// <param name="formula"></param>
        public UsingPython(KPIFormula formula) : this(formula.KPIScript)
        {

        }

        public UsingPython(AbsAssembleFormula ican) : this(ican.AssembleFormula())
        {

        }

        

        public object ExcuteScriptString(string pyContent, List<Param> paramList)
        {
            try
            {
                var engine = Python.CreateEngine();
                var scope = engine.CreateScope();
                var source = engine.CreateScriptSourceFromString(pyContent);
                foreach (var item in paramList)
                {
                    scope.SetVariable(item.Name, item.FixValue);
                }
                source.Execute(scope);
                return scope.GetVariable("result").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExcuteScriptFile(List<Param> paramList)
        {
            try
            {
                scope.SetVariable("result", "");
                foreach (var item in paramList)
                {
                    if (item.FixValue==null)
                    {
                        scope.SetVariable(item.Code.Trim(), 0);
                        //throw new Exception(item.Code.Trim()+" is null");
                    }
                    else
                    {
                        scope.SetVariable(item.Code.Trim(), item.FixValue);
                    }
                    
                }
                source.Execute(scope);
                return scope.GetVariable("result").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 简要传参数，执行KPI算法
        /// </summary>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public object ExcuteScriptFile(List<SimpleParam> paramList)
        {
            try
            {
                scope.SetVariable("result", "");
                foreach (var item in paramList)
                {
                    if (item.FixValue == null)
                    {
                        throw new Exception(item.Code.Trim() + " is null");
                    }
                    scope.SetVariable(item.Code.Trim(), item.FixValue);
                }
                source.Execute(scope);
                return scope.GetVariable("result").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetDemo() {
            return @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPIWebAPI.ViewModels;
using XKPI.Models;
using KPIWebAPI.Models;

namespace XKPI.Utils
{
    class UPDemo : AbsAssembleFormula
    {
        public UPDemo(int kpiid):base(kpiid) { }
        public override KPIFormula AssembleFormula()
        {
            using (var db = new KPIContext())
            {
                EP_KPI_SET body = db.EP_KPI_SET.FirstOrDefault(r => r.KPI_ID == KPI_ID);
                List <EP_KPI_PARAM> list = db.EP_KPI_PARAM.Where(r => r.KPI_ID == KPI_ID).ToList();
                return new KPIFormula(body, list);
            }
        }
    }
}
";
        }
    }
}
