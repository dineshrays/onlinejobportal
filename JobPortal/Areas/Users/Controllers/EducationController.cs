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


namespace Jobportal.Areas.User.Controllers
{
    
    public class EducationController : Controller
    {
        JobPortalEntities db = new JobPortalEntities();
        // GET: Education
        public ActionResult Index()
        {
            return View(db.UserEducations);
        }

        public ActionResult add()
        {
            var userdet = new SelectList(db.UserDetails.ToList(), "ID", "UserName");
            ViewData["userlist"] = userdet;
            return View();
        }

        [HttpPost]
        public ActionResult add(List<UserEducation> ue)
        {
            PremiumColldetail pd = new PremiumColldetail();
            //List<primecollege> pcs = (from pc in db.primecolleges
            //                          select new primecollege
            //                          {
            //                              id = pc.id,
            //                              PremiumInstituteNames = pc.PremiumInstituteNames,
            //                              ShortForms = pc.ShortForms
            //                          }
            //                       ).ToList();

           List<primecollege> pc = db.primecolleges.ToList();

            foreach (var list in ue)
            {
                
                    list.IsActive = true;
                    list.CreatedOn = System.DateTime.Now;
                    db.UserEducations.Add(list);
                    db.SaveChanges();
                foreach (var pcs in pc)
                {
                    if (list.InstituteName == pcs.PremiumInstituteNames || list.InstituteName == pcs.ShortForms)
                    {
                        pd.premium_id = pcs.id;
                        pd.UserDetails_id = list.UserDetails_id;
                        pd.UserEducation_id = list.Id;
                        db.PremiumColldetails.Add(pd);
                        db.SaveChanges();
                    }
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult edit(int id)
        {
            JobPortalViewModel vm = new JobPortalViewModel();
            UserEducation ue = new UserEducation();
            ue = db.UserEducations.Find(id);
            vm.education= ue;
            return View(vm);
        }


        [HttpPost]
        public ActionResult edit(int id, UserEducation ue)
        {
            UserEducation ues = db.UserEducations.Find(id);
            ues.ModifiedOn = System.DateTime.Now;
            db.Entry(ues).CurrentValues.SetValues(ue);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult delete(int id)
        {
            UserEducation ue = new UserEducation();
            ue = db.UserEducations.Find(id);
            ue.IsActive = false;
            return View(ue);
        }

        [HttpPost]
        public ActionResult delete(int id,UserEducation ue)
        {
            UserEducation ues = db.UserEducations.Find(id);
            ues.IsActive = false;
            db.UserEducations.Remove(ues);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);

        }
        
    }

}
