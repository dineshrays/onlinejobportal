using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;

namespace Jobportal.Users.Controllers
{
    public class CertificationsController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: Certifications
        public ActionResult Index()
        {
            var certifications = db.Certifications.Include(c => c.PersonalInfo);
            return View(certifications.ToList());
        }

        // GET: Certifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = db.Certifications.Find(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        // GET: Certifications/Create
        public ActionResult Create()
        {
       //    ViewBag.PersonalInfo_id = new SelectList(db.PersonalInfoes, "id", "id");
            ViewBag.PersonalInfo_id = new SelectList(db.UserDetails, "ID", "UserName");
            return View();
        }

        // POST: Certifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Certification certification)
        {
            certification.IsActive = true;
            certification.CreatedOn = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Certifications.Add(certification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonalInfo_id = new SelectList(db.PersonalInfoes, "id", "id", certification.PersonalInfo_id);
            return View(certification);
        }

        // GET: Certifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = db.Certifications.Find(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonalInfo_id = new SelectList(db.PersonalInfoes, "id", "id", certification.PersonalInfo_id);
            return View(certification);
        }

        // POST: Certifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Certification certification)
        {
            certification.IsActive = true;
            certification.ModifiedOn = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(certification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonalInfo_id = new SelectList(db.PersonalInfoes, "id", "id", certification.PersonalInfo_id);
            return View(certification);
        }

        // GET: Certifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = db.Certifications.Find(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        // POST: Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Certification certification = db.Certifications.Find(id);
            certification.IsActive = false;
            db.Certifications.Remove(certification);
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
