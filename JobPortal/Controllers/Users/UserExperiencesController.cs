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
    public class UserExperiencesController : Controller
    {
        private JobPortalEntities  db = new JobPortalEntities();

        // GET: UserExperiences
        public ActionResult Index()
        {
            var userExperiences = db.UserExperiences.Include(u => u.Company).Include(u => u.UserDetail);
            return View(userExperiences.ToList());
        }

        // GET: UserExperiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserExperience userExperience = db.UserExperiences.Find(id);
            if (userExperience == null)
            {
                return HttpNotFound();
            }
            return View(userExperience);
        }

        // GET: UserExperiences/Create
        public ActionResult Create()
        {
            ViewBag.Company_id = new SelectList(db.Companies, "id", "Companyname");
            ViewBag.UserId = new SelectList(db.UserDetails, "ID", "UserName");
            return View();
        }

        // POST: UserExperiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( UserExperience userExperience)
        {
             userExperience.CreatedOn = System.DateTime.Now;
            userExperience.IsActive = true;
            if (ModelState.IsValid)
            {
                db.UserExperiences.Add(userExperience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company_id = new SelectList(db.Companies, "id", "Companyname", userExperience.Company_id);
            ViewBag.UserId = new SelectList(db.UserDetails, "ID", "UserName", userExperience.UserId);
            return View(userExperience);
        }

        // GET: UserExperiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserExperience userExperience = db.UserExperiences.Find(id);
            if (userExperience == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company_id = new SelectList(db.Companies, "id", "Companyname", userExperience.Company_id);
            ViewBag.UserId = new SelectList(db.UserDetails, "ID", "UserName", userExperience.UserId);
            return View(userExperience);
        }

        // POST: UserExperiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( UserExperience userExperience)
        {
             userExperience.ModifiedOn = System.DateTime.Now;
            userExperience.IsActive = true;
            if (ModelState.IsValid)
            {
                db.Entry(userExperience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company_id = new SelectList(db.Companies, "id", "Companyname", userExperience.Company_id);
            ViewBag.UserId = new SelectList(db.UserDetails, "ID", "UserName", userExperience.UserId);
            return View(userExperience);
        }

        // GET: UserExperiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserExperience userExperience = db.UserExperiences.Find(id);
            if (userExperience == null)
            {
                return HttpNotFound();
            }
            return View(userExperience);
        }

        // POST: UserExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserExperience userExperience = db.UserExperiences.Find(id);
            userExperience.IsActive = false;
            db.UserExperiences.Remove(userExperience);
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
