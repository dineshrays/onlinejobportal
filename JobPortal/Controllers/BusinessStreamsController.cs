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
    public class IndustryTypesController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        
        public ActionResult Index()
        {
            return View(db.IndustryTypes.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndustryType IndustryType = db.IndustryTypes.Find(id);
            if (IndustryType == null)
            {
                return HttpNotFound();
            }
            return View(IndustryType);
        }

       
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( IndustryType IndustryType)
        {
            if (ModelState.IsValid)
            {
                IndustryType.CreatedOn = DateTime.Now;
                IndustryType.Isactive = true;
                db.IndustryTypes.Add(IndustryType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(IndustryType);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndustryType IndustryType = db.IndustryTypes.Find(id);
            if (IndustryType == null)
            {
                return HttpNotFound();
            }
            return View(IndustryType);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IndustryType1,Isactive,CreatedOn,ModifiedOn")] IndustryType IndustryType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(IndustryType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(IndustryType);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndustryType IndustryType = db.IndustryTypes.Find(id);
            if (IndustryType == null)
            {
                return HttpNotFound();
            }
            return View(IndustryType);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IndustryType IndustryType = db.IndustryTypes.Find(id);
            db.IndustryTypes.Remove(IndustryType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
