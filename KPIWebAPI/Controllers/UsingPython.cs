using IronPython.Hosting;
using KPIWebAPI.Models;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;

namespace KPIWebAPI.Controllers
{
    public class UsingPython
    {
        ScriptEngine engine ;
        ScriptScope scope ;
        ScriptSource source;
        private UsingPython() { }
        public UsingPython(string ScriptString)
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();
            source = engine.CreateScriptSourceFromString(ScriptString);
        }

        public UsingPython(KPINode kpi)
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();
            source = engine.CreateScriptSourceFromString(kpi.ScriptString);
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
                        throw new Exception(item.Code.Trim()+" is null");
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
