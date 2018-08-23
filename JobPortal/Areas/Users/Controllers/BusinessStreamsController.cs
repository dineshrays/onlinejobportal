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
using Newtonsoft.Json;



namespace Jobportal.Areas.User.Controllers
{
    public class BusinessStreamsController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: BusinessStreams
        public ActionResult Index()
        {
            return View(db.IndustryTypes.ToList());
        }

        // GET: BusinessStreams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndustryType businessStream = db.IndustryTypes.Find(id);
            if (businessStream == null)
            {
                return HttpNotFound();
            }
            return View(businessStream);
        }

        // GET: BusinessStreams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessStreams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( IndustryType businessStream)
        {
            businessStream.Isactive = true;
            businessStream.CreatedOn = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.IndustryTypes.Add(businessStream);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessStream);
        }

        // GET: BusinessStreams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndustryType businessStream = db.IndustryTypes.Find(id);
            if (businessStream == null)
            {
                return HttpNotFound();
            }
            return View(businessStream);
        }

        // POST: BusinessStreams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IndustryType businessStream)
        {

            businessStream.Isactive = true;
            businessStream.ModifiedOn = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(businessStream).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businessStream);
        }

        // GET: BusinessStreams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndustryType businessStream = db.IndustryTypes.Find(id);
            if (businessStream == null)
            {
                return HttpNotFound();
            }
            return View(businessStream);
        }

        // POST: BusinessStreams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IndustryType businessStream = db.IndustryTypes.Find(id);
            db.IndustryTypes.Remove(businessStream);
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
