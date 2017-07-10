using System.Web.Mvc;

namespace KPIWebAPI.Areas.TCSDItem
{
    public class TCSDItemAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TCSDItem";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TCSDItem_default",
                "TCSDItem/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}