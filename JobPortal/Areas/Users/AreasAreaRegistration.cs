using System.Web.Mvc;

namespace JobPortal.Areas.Users
{
    public class AreasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Users";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Areas_default",
                "Areas/{controller}/{action}/{id}",
                    //new { controller="User",action = "register", id = UrlParameter.Optional }
                    new { action = "Index", id = UrlParameter.Optional },
                new[] { "JobPortal.Areas.Users.Controllers" }

            );
        }
    }
}