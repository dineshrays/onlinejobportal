using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using JobPortal.Models;
using JobPortal.ViewModel;
using Newtonsoft.Json;


namespace Jobportal.Areas.Users.Controllers
{
    public class UserController : Controller
    {
        JobPortalEntities db = new JobPortalEntities();
        // GET: User
        public ActionResult Index()
        {
            return View(db.UserDetails);
        }
        public ActionResult register()
        {
            var usertype = new SelectList(db.User_type.ToList(), "ID", "UserType");
            ViewData["userlist"] = usertype;
            return View();
        }
        [HttpPost]
        public ActionResult create(UserDetail ud)
        {
            ud.IsActive = true;
            ud.RegistrationDate = DateTime.Now;
            ud.CreatedOn = System.DateTime.Now;
            db.UserDetails.Add(ud);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public ActionResult StoreImage(HttpPostedFileBase file)
        {
            string path = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (pic != null && pic.ContentLength > 0)
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/UserImage"), Path.GetFileName(pic.FileName));
                        pic.SaveAs(path);
                        path = "/UserImage/" + pic.FileName;
                        TempData["path"] = path;
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }
            }
            return Json(new { filepath = path }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult edit(int id)
        {

            JobPortalViewModel vm = new JobPortalViewModel();
            UserDetail ud = new UserDetail();
            ud = db.UserDetails.Find(id);
            vm.userdetails = ud;
            return View(vm);
        }

        [HttpPost]
        public ActionResult edit(int id, UserDetail ud)
        {
            ud.ModifiedOn = System.DateTime.Now;
            ud.IsActive = true;
            db.Entry(ud).State = EntityState.Modified;
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult delete(int id)
        {
            UserDetail ud = new UserDetail();
            ud = db.UserDetails.Find(id);
            ud.IsActive = false;
            return View(ud);
        }

        [HttpPost]
        public ActionResult delete(int id, UserDetail ud)
        {
            UserDetail userdet = db.UserDetails.Find(id);
            userdet.IsActive = false;
            db.UserDetails.Remove(userdet);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);

        }
        
    }
}