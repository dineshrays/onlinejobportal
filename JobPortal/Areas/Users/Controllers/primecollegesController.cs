using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;

namespace Jobportal.Areas.User.Controllers
{
    public class primecollegesController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: primecolleges
        public ActionResult Index()
        {
            return View(db.primecolleges.ToList());
        }

        // GET: primecolleges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            primecollege primecollege = db.primecolleges.Find(id);
            if (primecollege == null)
            {
                return HttpNotFound();
            }
            return View(primecollege);
        }

        // GET: primecolleges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: primecolleges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(primecollege primecollege)
        {
            if (ModelState.IsValid)
            {
                db.primecolleges.Add(primecollege);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(primecollege);
        }

        // GET: primecolleges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            primecollege primecollege = db.primecolleges.Find(id);
            if (primecollege == null)
            {
                return HttpNotFound();
            }
            return View(primecollege);
        }

        // POST: primecolleges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(primecollege primecollege)
        {
            if (ModelState.IsValid)
            {
                db.Entry(primecollege).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(primecollege);
        }

        // GET: primecolleges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            primecollege primecollege = db.primecolleges.Find(id);
            if (primecollege == null)
            {
                return HttpNotFound();
            }
            return View(primecollege);
        }

        // POST: primecolleges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            primecollege primecollege = db.primecolleges.Find(id);
            db.primecolleges.Remove(primecollege);
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
