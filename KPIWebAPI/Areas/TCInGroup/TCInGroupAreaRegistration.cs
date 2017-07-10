using System.Web.Mvc;

namespace KPIWebAPI.Areas.TCInGroup
{
    public class TCInGroupAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TCInGroup";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TCInGroup_default",
                "TCInGroup/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}