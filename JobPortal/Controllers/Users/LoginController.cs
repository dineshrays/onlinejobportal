using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using JobPortal.Models;
using JobPortal.ViewModel;
using Newtonsoft.Json;


namespace JobPortal.Controllers.Users
{
    public class LoginController : Controller
    {
        JobPortalEntities db = new JobPortalEntities();
        // GET: User
        public ActionResult Index()
        {
            return View(db.UserDetails);
        }


        public ActionResult userlogin()
        {
            var usertype = new SelectList(db.User_type.ToList(), "ID", "UserType");
            ViewData["userlist"] = usertype;
            return View();
        }

        [HttpPost]
        public ActionResult userlogin(UserDetail ud)
        {
            UserDetail u = db.UserDetails.FirstOrDefault(a => a.Usertype_id == 3 && a.UserName == ud.UserName && a.Password == ud.Password);
            LoginViewModel loginvm = new LoginViewModel();
            if (u!=null)
            {
                loginvm.Userid = u.ID;
                loginvm.Usertype_id = u.Usertype_id;
            }
            return Json(loginvm, JsonRequestBehavior.AllowGet);
        }
    }
}