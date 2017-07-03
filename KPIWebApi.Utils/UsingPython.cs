using IronPython.Hosting;
using KPIWebAPI.ViewModels;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;

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
        public UsingPython(string ScriptString)
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



    }
}
