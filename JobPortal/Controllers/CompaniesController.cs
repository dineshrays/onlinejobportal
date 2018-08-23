using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;

namespace JobPortal.Controllers
{
    public class CompaniesController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.IndustryType);
            return View(companies.ToList());
        }

        
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

        
        public ActionResult Create()
        {
            ViewBag.IndustryTypeId = new SelectList(db.IndustryTypes, "Id", "IndustryType1");
            return View();
        }

        
        [HttpPost]
        
        //public ActionResult Create(Company company)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Companies.Add(company);
        //        db.SaveChanges();
        //        //return RedirectToAction("Index");
        //    }

        //  //  ViewBag.IndustryTypeId = new SelectList(db.IndustryTypes, "Id", "IndustryType1", company.IndustryTypeId);
        //    //return View(company);
        //    return Json("", JsonRequestBehavior.AllowGet);
        //}

        
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
            //ViewBag.IndustryTypeId = new SelectList(db.IndustryTypes, "Id", "IndustryType1", company.IndustryTypeId);
            return View(company);
        }

      
        [HttpPost]
        
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IndustryTypeId = new SelectList(db.IndustryTypes, "Id", "IndustryType1", company.IndustryTypeId);
            return View(company);
        }

        
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

        
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult StoreImage(HttpPostedFileBase file)
        {


            string path = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];


                if (pic != null && pic.ContentLength > 0)
                    try
                    {
                        path = System.IO.Path.Combine(Server.MapPath("~/MovieImages"),
                                                  Path.GetFileName(pic.FileName));
                        pic.SaveAs(path);
                        path = "/MovieImages/" + pic.FileName;
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
        public ActionResult StoreLogo(HttpPostedFileBase file)
        {
            string path = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];


                if (pic != null && pic.ContentLength > 0)
                    try
                    {
                        path = System.IO.Path.Combine(Server.MapPath("~/CompanyLogo"),
                                                  Path.GetFileName(pic.FileName));
                        pic.SaveAs(path);
                        path = "/CompanyLogo/" + pic.FileName;
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
        public ActionResult StorePresent(HttpPostedFileBase file)
        {
            string path = "";
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];


                if (pic != null && pic.ContentLength > 0)
                    try
                    {
                        path = System.IO.Path.Combine(Server.MapPath("~/CompanyPresentation"),
                                                  Path.GetFileName(pic.FileName));
                        pic.SaveAs(path);
                        path = "/CompanyPresentation/" + pic.FileName;
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



    }
}
