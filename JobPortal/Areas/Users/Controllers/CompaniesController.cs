using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using Newtonsoft.Json;
using System.IO;

namespace Jobportal.Areas.User.Controllers
{
    public class CompaniesController : Controller
    {
        JobPortalEntities db = new JobPortalEntities();

        // GET: Companies
        public ActionResult Index()
        {
            //var companies = db.Companies.Include(c => c.BusinessStream);
            //return View(companies.ToList());
            return View(db.Companies);
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            var busstream = new SelectList(db.IndustryTypes.ToList(), "id", "BussinessStream");
            ViewData["busns"] = busstream;

            //ViewBag.BusinessStreamId = new SelectList(db.BusinessStreams, "id", "BussinessStream");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
                db.Companies.Add(company);
                db.SaveChanges();
                return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult StoreImage(HttpPostedFileBase file)
        {
            string path = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (pic != null && pic.ContentLength > 0)
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/CompanyImage"), Path.GetFileName(pic.FileName));
                        pic.SaveAs(path);
                        path = "/CompanyImage/" + pic.FileName;
                        TempData["path"] = path;
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }
            }
            return Json(new { filepath = path }, JsonRequestBehavior.AllowGet);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessStreamId = new SelectList(db.IndustryTypes, "id", "BussinessStream", company.IndustryId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Edit( Company company)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(company).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BusinessStreamId = new SelectList(db.BusinessStreams, "id", "BussinessStream", company.BusinessStreamId);
        //    return View(company);
        //}
        [HttpPost]
        public ActionResult Edit(int id,Company company)
        {
            Company comp = db.Companies.Find(id);
            db.Entry(comp).CurrentValues.SetValues(company);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);

        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
