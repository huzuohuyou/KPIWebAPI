using KPIWebApi.Models;
using KPIWebAPI.Areas.TCProject.Models;
using KPIWebAPI.Areas.TCProject.ViewModels;
using KPIWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KPIWebAPI.Areas.TCProject.Controllers
{
    /// <summary>
    /// 项目配置webapi
    /// </summary>
    [RoutePrefix("tc/api/project")]
    public class ProjectController : ApiController
    {
        /// <summary>
        /// 获取全部项目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TcProject> GetProject()
        {
            List<TcProject> list = new List<TcProject>();
            using (var db = new KPIContext())
            {
                db.TC_PROJECT.ToList().ForEach(r=> {
                    list.Add(
                        new TcProject() {ID=r.ID,Name=r.PROJECT_NAME,CDRIP = r.CDR_IP,CDRUName=r.CDR_USER_NAME,SDRPWD=r.CDR_PWD,SDRIP=r.SDRIP,SDRUName=r.SDR_USER_NAME,CDRPWD=r.SDR_PWD }
                        ); });
                return list;
            }
        }

        /// <summary>
        /// 获取某一页项目
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("page"), HttpGet]
        public List<TcProject> GetByPageProject(ProjectPage page)
        {
            List<TcProject> list = new List<TcProject>();
            using (var db = new KPIContext())
            {
                db.TC_PROJECT.Skip((page.Num - 1) * page.Count).Take(page.Count).ToList().ForEach(r =>
                {
                    list.Add(
                        new TcProject() { ID = r.ID, Name = r.PROJECT_NAME, CDRIP = r.CDR_IP, CDRUName = r.CDR_USER_NAME, SDRPWD = r.CDR_PWD, SDRIP = r.SDRIP, SDRUName = r.SDR_USER_NAME, CDRPWD = r.SDR_PWD }
                        );
                });
                return list;
            }
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPut]
        public int PutProject(TcProject p)
        {
            using (var db = new KPIContext())
            {
                TC_PROJECT tp=db.TC_PROJECT.Find(new TC_PROJECT()
                {
                    ID=p.ID
                    
                });
                tp.PROJECT_NAME = p.Name;
                tp.CDR_IP = p.CDRIP;
                tp.CDR_USER_NAME = p.CDRUName;
                tp.CDR_PWD = p.CDRPWD;
                tp.SDRIP = p.SDRIP;
                tp.SDR_USER_NAME = p.SDRUName;
                tp.SDR_PWD = p.SDRPWD;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        public int PostProject(TcProject p)
        {
            using (var db = new KPIContext())
            {
                db.TC_PROJECT.Add(new TC_PROJECT()
                {
                    PROJECT_NAME = p.Name,
                    CDR_IP = p.CDRIP,
                    CDR_USER_NAME = p.CDRUName,
                    CDR_PWD = p.CDRPWD,
                    SDRIP = p.SDRIP,
                    SDR_USER_NAME = p.SDRUName,
                    SDR_PWD = p.SDRPWD
                });
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpDelete]
        public int DelProject(TcProject p)
        {
            using (var db = new KPIContext())
            {
                db.TC_PROJECT.Remove(new TC_PROJECT { ID = p.ID });
                return db.SaveChanges();
            }
        }
    }
}
