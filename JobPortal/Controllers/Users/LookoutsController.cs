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

namespace Jobportal.User.Controllers
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
            var compan = new SelectList(db.Companies, "Id", "CompanyName");
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

        [HttpPost]
        public ActionResult  complist(int id)
        {
            List<prefCompViewModel> prelist = (from prec in db.PreferredCompanies
                                               join c in db.Companies
                                               on prec.Company_id equals c.Id
                                               where prec.Lookout_id == id
                                               select new prefCompViewModel
                                               {
                                                   companyid = prec.Company_id,
                                                   companyname = c.CompanyName
                                               }).ToList();
            return Json(prelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult loclist (int id)
        {
            List<preflocViewModel> preloc = (from prloc in db.PreferredLocations
                                             join l in db.Locations
                                             on prloc.Location_id equals l.id
                                             where prloc.Lookout_id == id
                                             select new preflocViewModel
                                             {
                                                 locid = prloc.Location_id,
                                                 locname = l.City.CityName
                                             }).ToList();
            return Json(preloc, JsonRequestBehavior.AllowGet);
        }






        // GET: Lookouts/Edit/5
        public ActionResult Edit(int id)
        {
            JobPortalViewModel vm = new JobPortalViewModel();
            var userlist = new SelectList(db.UserDetails, "ID", "UserName");
            ViewData["userlist"] = userlist;
            var shiftlist = new SelectList(db.Shifts, "id", "ShiftName");
            ViewData["shiflist"] = shiftlist;
            var compan = new SelectList(db.Companies, "Id", "CompanyName");
            ViewData["comp"] = compan;
            Lookout lookout = db.Lookouts.Find(id);
            lookout.LastActive = System.DateTime.Now;
            vm.lookout = lookout;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(int id,Lookout lookout,int s,List<PreferredCompany> pc, string locids)
        {
            string[] nameslist = locids.Split(',');
            lookout.LastActive = System.DateTime.Now;
            db.Entry(lookout).State = EntityState.Modified;
            db.SaveChanges();
            WorkShift ws = db.WorkShifts.FirstOrDefault(a => a.Lookout_Id==id);
            Shift sf = new Shift();
             sf = db.Shifts.Find(s);
            ws.Shift_Id = sf.id;
            db.Entry(ws).State = System.Data.Entity.EntityState.Modified;  //EntityState.Modified;
            db.SaveChanges();
            foreach (var list in pc)
            {
                PreferredCompany pcs = db.PreferredCompanies.FirstOrDefault(a => a.Lookout_id == list.Lookout_id);
                db.Entry(pcs).CurrentValues.SetValues(pc);
                db.SaveChanges();
                
            }

            PreferredLocation pr = new PreferredLocation();
            List<PreferredLocation> prl = db.PreferredLocations.Where(ax => ax.Lookout_id == lookout.Id).ToList();
            foreach (var a in nameslist)
            {
                int c = Convert.ToInt16(a);
                Location loc = db.Locations.FirstOrDefault(ax => ax.City_id == c);
                //List<PreferredLocation> prl = db.PreferredLocations.Where(ax => ax.Lookout_id==lookout.Id).ToList();
                foreach(var lis in prl)
                {
                    db.PreferredLocations.Remove(lis);
                    db.SaveChanges();
                }
                var l = Convert.ToInt16(loc.id);
                pr.Location_id = l;
                pr.Lookout_id = lookout.Id;
                db.PreferredLocations.Add(pr);
                db.SaveChanges();
                
            //    var l = Convert.ToInt16(loc.id);
            //    if (prl.Lookout_id == lookout.Id)
            //    {
            //        prl.Location_id=l;
            //        db.Entry(prl).CurrentValues.SetValues(nameslist);
            //        db.SaveChanges();
            //    }
            }
            
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
            List< WorkShift> ws = db.WorkShifts.Where(a => a.Lookout_Id == lookout.Id).ToList();
            foreach(var lws in ws)
            {
                db.WorkShifts.Remove(lws);
                db.SaveChanges();
            }

            List<PreferredCompany> pc = db.PreferredCompanies.Where(b => b.Lookout_id == lookout.Id).ToList();
            foreach(var list in pc)
            {
                db.PreferredCompanies.Remove(list);
                db.SaveChanges();
            }

            List<PreferredLocation> prl = db.PreferredLocations.Where(ax => ax.Lookout_id == lookout.Id).ToList();
            foreach (var a in prl)
            {
                    db.PreferredLocations.Remove(a);
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
