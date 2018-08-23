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
    public class UserSkillController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: UserSkill
        public ActionResult Index()
        {
            var personalInfoes = db.PersonalInfoes.Include(p => p.Skill).Include(p => p.UserDetail);
            return View(personalInfoes.ToList());
        }

        // GET: UserSkill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // GET: UserSkill/Create
        public ActionResult Create()
        {
            ViewBag.Skill_Id = new SelectList(db.Skills, "id", "SkillName");
            ViewBag.User_Id = new SelectList(db.UserDetails, "ID", "UserName");
            return View();
        }

        // POST: UserSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                db.PersonalInfoes.Add(personalInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Skill_Id = new SelectList(db.Skills, "id", "SkillName", personalInfo.Skill_Id);
            ViewBag.User_Id = new SelectList(db.UserDetails, "ID", "UserName", personalInfo.User_Id);
            return View(personalInfo);
        }

        // GET: UserSkill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Skill_Id = new SelectList(db.Skills, "id", "SkillName", personalInfo.Skill_Id);
            ViewBag.User_Id = new SelectList(db.UserDetails, "ID", "UserName", personalInfo.User_Id);
            return View(personalInfo);
        }

        // POST: UserSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Skill_Id = new SelectList(db.Skills, "id", "SkillName", personalInfo.Skill_Id);
            ViewBag.User_Id = new SelectList(db.UserDetails, "ID", "UserName", personalInfo.User_Id);
            return View(personalInfo);
        }

        // GET: UserSkill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // POST: UserSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalInfo personalInfo = db.PersonalInfoes.Find(id);
            db.PersonalInfoes.Remove(personalInfo);
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
