using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using JobPortal.ViewModel;

namespace JobPortal.Controllers
{
    public class RecruitersController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        
        public ActionResult Index()
        {
            //var recruiters = db.Recruiters.Include(r => r.Company).Include(r => r.Recruiter1).Include(r => r.Recruiter2);
            return View(db.Recruiters);
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        
        public ActionResult Create()
        {
           var industrylist = new SelectList(db.IndustryTypes.ToList(), "Id", "BusinessStreamName");
           // List<IndustryType> ity = db.IndustryTypes.ToList();
            ViewData["industype"] = industrylist;


            var countrylist = new SelectList(db.Countries.ToList(), "Id", "CountryName");
            ViewData["countrlist"] = countrylist;

            var statelist = new SelectList(db.States.ToList(), "Id", "StateName");
            ViewData["statelists"] = statelist;

            var citylis = new SelectList(db.Cities.ToList(), "Id", "CityName");
            ViewData["citylist"] = citylis;
            return View();
        }

       
        [HttpPost]
       public ActionResult GetCity(int sid)
        {

            //List<State> st = db.States.Where(a => a.CountryId == sid).ToList();
            //return Json(st, JsonRequestBehavior.AllowGet);

            IEnumerable<SelectListItem> state = db.States.Where(s => s.CountryId == sid).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.StateName
            }).ToList();

            return Json(state, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetValue(int cid)
        {

            //List<State> st = db.States.Where(a => a.CountryId == sid).ToList();
            //return Json(st, JsonRequestBehavior.AllowGet);

            IEnumerable<SelectListItem> city = db.Cities.Where(s => s.StateId == cid).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.CityName
            }).ToList();

            return Json(city, JsonRequestBehavior.AllowGet);


        }
        public ActionResult Create1(Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                recruiter.Isactive = true;
                recruiter.ModifiedOn = DateTime.Now;
                db.Recruiters.Add(recruiter);
                db.SaveChanges();

            }
            Session["recId"] = recruiter.Id.ToString();


            return Json("", JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ComId = new SelectList(db.Companies, "Id", "CompanyName", recruiter.ComId);
            ViewBag.Id = new SelectList(db.Recruiters, "Id", "Name", recruiter.Id);
            ViewBag.Id = new SelectList(db.Recruiters, "Id", "Name", recruiter.Id);
            return View(recruiter);
        }

        [HttpPost]
        public ActionResult Edit(Recruiter recruiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ComId = new SelectList(db.Companies, "Id", "CompanyName", recruiter.ComId);
            ViewBag.Id = new SelectList(db.Recruiters, "Id", "Name", recruiter.Id);
            ViewBag.Id = new SelectList(db.Recruiters, "Id", "Name", recruiter.Id);
            return View(recruiter);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruiter recruiter = db.Recruiters.Find(id);
            if (recruiter == null)
            {
                return HttpNotFound();
            }
            return View(recruiter);
        }

        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Recruiter recruiter = db.Recruiters.Find(id);
            db.Recruiters.Remove(recruiter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Register()
        {
          return Json("", JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Drop()
        //{
        //  var countrlist = new SelectList(db.Countries.ToList(), "Id", "CountryName");
        //    ViewData["cuntrlist"] = countrlist;

        //    return View();
        //}

        public ActionResult ListOFPlan()
        {
            var Planlist = new SelectList(db.PlanMasters.ToList(), "Id", "Name");
            ViewData["Planlistview"] = Planlist;
            return View();
        }
        public ActionResult PlanDetails(int id)
        {
            PlanMaster plan = db.PlanMasters.FirstOrDefault(x => x.Id == id && x.Isactive == true);
            return View(plan);
            
        }
        [HttpPost]
        public ActionResult ListOFPlan(int pid)
        {
            
            //PlanMaster plan = db.PlanMasters.FirstOrDefault(x => x.Id == pid && x.Isactive == true);
            List<PlanMasterModel> plan = (from plnmst in db.PlanMasters
                                     where plnmst.Id == pid
                                     select new PlanMasterModel
                                     {
                                         Name = plnmst.Name,
                                         ValidFrom = plnmst.ValidFrom,
                                         ValidTo = plnmst.ValidTo,
                                         Price = plnmst.Price,
                                         NoClicks=plnmst.NoClicks
                                     }).ToList();

            return Json(plan, JsonRequestBehavior.AllowGet);

     }
        public ActionResult PlanSelect(RecruiterPlan plan)
        {
            //int id = Convert.ToInt32(Session["Rid"]);
            
            Session["count"] = plan.SubUserCount.ToString();


            plan.RecruiterId = Convert.ToInt32(Session["recId"]);
            plan.Isactive = true;
            plan.CreatedOn = DateTime.Now;
            db.RecruiterPlans.Add(plan);
            db.SaveChanges();

            int recplanId = plan.RecruiterId;
            Session["recID"] = plan.Id.ToString(); 
            


            return Json("", JsonRequestBehavior.AllowGet);

        }
        public ActionResult ListSubUser()
        {
             RecruiterPlanDet rplan = new RecruiterPlanDet();
            // RecruiterPlan rplan = new RecruiterPlan();
            //rplan.SubUserCount = Convert.ToInt32(Session["count"]);
            return View(rplan);
        }
        public ActionResult SaveDetails(List<RecruiterPlanDet>recruiterplan )
        {

            int id = Convert.ToInt32(Session["recID"]);
            foreach (var list in recruiterplan)
            {
                list.RecruiterPlanId = id;
                list.Isactive = true;
                list.CreatedOn = DateTime.Now;
                db.RecruiterPlanDets.Add(list);
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListOfSelectedPlan()
        {

            return View(db.RecruiterPlans.ToList());
        }
        public ActionResult Edits(int id)
        {
             //var reclist = db.RecruiterPlanDets.Find(id);
            List<RecruiterPlanDet> reclist = db.RecruiterPlanDets.Where(a => a.RecruiterPlanId == id).ToList();
            return View(reclist);
        }
        [HttpPost]
        public ActionResult getRecruiterPlan(List<RecruiterPlanDet> RecPlan,int rid)
        {

           
                List<RecruiterPlanDet> rec = db.RecruiterPlanDets.Where(a => a.RecruiterPlanId ==  rid).ToList();
                foreach (var reupla in rec)
                {
                    db.RecruiterPlanDets.Remove(reupla);
                    db.SaveChanges();
                }

               foreach(var list in RecPlan)
               {
                list.RecruiterPlanId = rid;
                db.RecruiterPlanDets.Add(list);
                db.SaveChanges();
               }


            return Json("", JsonRequestBehavior.AllowGet);

        }


    }
}
