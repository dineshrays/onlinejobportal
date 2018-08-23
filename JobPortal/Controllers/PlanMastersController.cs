using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;

namespace JobPortal.Controllers
{
    public class PlanMastersController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: PlanMasters
        public ActionResult Index()
        {
            return View(db.PlanMasters.ToList());
        }

        // GET: PlanMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMaster planMaster = db.PlanMasters.Find(id);
            if (planMaster == null)
            {
                return HttpNotFound();
            }
            return View(planMaster);
        }

        // GET: PlanMasters/Create
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanMaster planMaster)
        {
            if (ModelState.IsValid)
            {
                planMaster.Isactive = true;
                planMaster.CreatedOn = DateTime.Now;
                db.PlanMasters.Add(planMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planMaster);
        }

        // GET: PlanMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMaster planMaster = db.PlanMasters.Find(id);
            if (planMaster == null)
            {
                return HttpNotFound();
            }
            return View(planMaster);
        }

        // POST: PlanMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanMaster planMaster)
        {
            if (ModelState.IsValid)
            {
                planMaster.Isactive = true;
                planMaster.ModifiedOn = DateTime.Now;
                db.Entry(planMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planMaster);
        }

        // GET: PlanMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMaster planMaster = db.PlanMasters.Find(id);
            if (planMaster == null)
            {
                return HttpNotFound();
            }
            return View(planMaster);
        }

        // POST: PlanMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanMaster planMaster = db.PlanMasters.Find(id);
            db.PlanMasters.Remove(planMaster);
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
