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

namespace Jobportal.Areas.User.Controllers
{
    public class LookoutsController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: Lookouts
        public ActionResult Index()
        {
            var lookouts = db.Lookouts.Include(l => l.UserDetail);
            return View(lookouts.ToList());
        }

        // GET: Lookouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookout lookout = db.Lookouts.Find(id);
            if (lookout == null)
            {
                return HttpNotFound();
            }
            return View(lookout);
        }

        // GET: Lookouts/Create
        public ActionResult Create()
        {
            var userlist = new SelectList(db.UserDetails, "ID", "UserName");
            ViewData["userlist"] = userlist;
            var shiftlist = new SelectList(db.Shifts, "id", "ShiftName");
            ViewData["shiflist"] = shiftlist;
            var compan = new SelectList(db.Companies, "id", "Companyname");
            ViewData["comp"] = compan;
            return View();
        }

        [HttpPost]
        public ActionResult Create( Lookout lookout,int s,List<PreferredCompany> pc,string lkid)
        {
            string[] nameslist = lkid.Split(',');

            lookout.LastActive = System.DateTime.Now;

            db.Lookouts.Add(lookout);
            db.SaveChanges();
            foreach (var list in pc)
            {
                list.Lookout_id = lookout.Id;
                db.PreferredCompanies.Add(list);
                db.SaveChanges();
            }
            PreferredLocation prl = new PreferredLocation();
            foreach (var a in nameslist)
            {
                int c = Convert.ToInt16(a);
                Location loc = db.Locations.FirstOrDefault(ax => ax.City_id == c);
                var l = Convert.ToInt16(loc.id);
                prl.Location_id = l;
                prl.Lookout_id = lookout.Id;
                db.PreferredLocations.Add(prl);
                db.SaveChanges();
                
            }

            WorkShift ws = new WorkShift();
            Shift shift = db.Shifts.Find(s);
            ws.Shift_Id = shift.id;
            ws.Lookout_Id = lookout.Id;
            db.WorkShifts.Add(ws);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult locatnname(string names)
        //{
        //    string[] nameslist = names.Split(',');
        //    string name = nameslist[nameslist.Length - 1];
        //    List<SearchList> list = new List<SearchList>();

        //    List<SearchList> contname = (from c in db.Countries
        //                                 where c.CountryName.Contains(name)
        //                                 select new SearchList
        //                                 {
        //                                     Id = c.id,
        //                                     Name = c.CountryName,
        //                                     type = 1
        //                                 }).ToList();

        //    List<SearchList> statname = (from s in db.States
        //                                 where s.StateName.Contains(name)
        //                                 select new SearchList
        //                                 {
        //                                     Id = s.id,
        //                                     Name = s.StateName,
        //                                     type = 2
        //                                 }).ToList();

        //    List<SearchList> citi = (from c in db.Cities
        //                             where c.CityName.Contains(name)
        //                             select new SearchList
        //                             {
        //                                 Id = c.id,
        //                                 Name = c.CityName,
        //                                 type = 3
        //                             }).ToList();

        //    foreach (var count in contname)
        //    {
        //        list.Add(count);
        //    }
        //    foreach (var i in statname)
        //    {
        //        list.Add(i);
        //    }
        //    foreach (var ci in citi)
        //    {
        //        list.Add(ci);
        //    }

        //    return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}


       
        public JsonResult locatnname()
        {

            List<SearchList> list = new List<SearchList>();

            List<SearchList> citi = (from c in db.Cities
                                     select new SearchList
                                     {
                                         text = c.CityName,
                                         value=c.Id,
                                     }).ToList();
            foreach (var ci in citi)
            {
                list.Add(ci);
            }

            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult getcomplist(int id)
        {
            Company comp = new Company();
            comp = db.Companies.Find(id);
            return Json(new { name = comp.CompanyName, compid = comp.Id }, JsonRequestBehavior.AllowGet);

        }


        // GET: Lookouts/Edit/5
        public ActionResult Edit(int id)
        {
            JobPortalViewModel vm = new JobPortalViewModel();
            var userlist = new SelectList(db.UserDetails, "ID", "UserName");
            ViewData["userlist"] = userlist;
            var shiftlist = new SelectList(db.Shifts, "id", "ShiftName");
            ViewData["shiflist"] = shiftlist;

            Lookout lookout = db.Lookouts.Find(id);
            lookout.LastActive = System.DateTime.Now;
            vm.lookout = lookout;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(int id,Lookout lookout,int s,List<PreferredCompany> pc)
        {
            Lookout look = db.Lookouts.Find(id);
            lookout.LastActive = System.DateTime.Now;
            db.Entry(look).CurrentValues.SetValues(lookout);
            db.SaveChanges();
            //    WorkShift ws = new WorkShift();
            //      Shift sf = new Shift();
            //        sf = db.Shifts.Find(s);
            //          ws.Lookout_Id = look.Id;
            //            ws.Shift_Id = sf.id;
            WorkShift ws = db.WorkShifts.FirstOrDefault(a => a.Lookout_Id==id);
            WorkShift wst = new WorkShift();
            Shift sf = new Shift();
              sf = db.Shifts.Find(s);
            ws.Shift_Id = sf.id;
            db.Entry(ws).State = EntityState.Modified;
            db.SaveChanges();
        
            

            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: Lookouts/Delete/5
        public ActionResult Delete(int id)
        {
            Lookout lookout = db.Lookouts.Find(id);
            return View();
        }

        // POST: Lookouts/Delete/5
        [HttpPost]
       
        public ActionResult Delete(int id,int s)
        {
            Lookout lookout = db.Lookouts.Find(id);
            WorkShift ws = new WorkShift();
            if (ws.Lookout_Id == lookout.Id)
            {
                db.WorkShifts.Remove(ws);
                db.SaveChanges();
            }
            db.Lookouts.Remove(lookout);
            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
