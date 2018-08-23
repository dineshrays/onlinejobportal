using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;

namespace Jobportal.User.Controllers
{
    public class LocationsController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: Locations
        public ActionResult Index()
        {
            var locations = db.Locations.Include(l => l.City).Include(l => l.Country).Include(l => l.State);
            return View(locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            ViewBag.City_id = new SelectList(db.Cities, "id", "CityName");
            ViewBag.Country_id = new SelectList(db.Countries, "id", "CountryName");
            ViewBag.State_id = new SelectList(db.States, "id", "StateName");
            return View();
        }
        [HttpPost]
        public ActionResult getdroplist(int id)
        {

            IEnumerable<SelectListItem> statdet = db.States.Where(s => s.CountryId == id).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.StateName
            }).ToList();

            return Json(statdet, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getcitylist(int id)
        {

            IEnumerable<SelectListItem> citydet = db.Cities.Where(s => s.StateId == id).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.CityName
            }).ToList();

            return Json(citydet, JsonRequestBehavior.AllowGet);
        }
        


        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( Location location)
        {
            location.IsActive = true;
            location.CreatedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
            }

            ViewBag.City_id = new SelectList(db.Cities, "id", "CityName", location.City_id);
            ViewBag.Country_id = new SelectList(db.Countries, "id", "CountryName", location.Country_id);
            ViewBag.State_id = new SelectList(db.States, "id", "StateName", location.State_id);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.City_id = new SelectList(db.Cities, "id", "CityName", location.City_id);
            ViewBag.Country_id = new SelectList(db.Countries, "id", "CountryName", location.Country_id);
            ViewBag.State_id = new SelectList(db.States, "id", "StateName", location.State_id);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Location location)
        {

            location.IsActive = true;
            location.ModifiedOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.City_id = new SelectList(db.Cities, "id", "CityName", location.City_id);
            ViewBag.Country_id = new SelectList(db.Countries, "id", "CountryName", location.Country_id);
            ViewBag.State_id = new SelectList(db.States, "id", "StateName", location.State_id);
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);

            location.IsActive = false;

            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
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
