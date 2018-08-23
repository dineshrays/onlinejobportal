using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using JobPortal.ViewModel;


namespace JobPortal.Controllers
{
    public class JobPostController : Controller
    {
        JobPortalEntities db = new JobPortalEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetPgName()
        {
            List<PGViewModel> pg = (from p in db.PGs
                           //where p.PGQualification.Contains(name)
                           select new PGViewModel
                           {
                               value = p.Id,
                               text = p.PGQualification
                           }

                          ).ToList();


            return new JsonResult { Data = pg, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Create()
        {
            //var industlist = new SelectList(db.IndustryTypes.ToList(), "Id", "BusinessStreamName");
            //ViewData["industliss"] = industlist;

            var rolelist = new SelectList(db.Roles.ToList(), "Id", "RoleType");
            ViewData["rolelistview"] = rolelist;

            var functionlist = new SelectList(db.FunctionalAreas.ToList(), "Id", "FunctionalAreaName");
            ViewData["funclist"] = functionlist;


            var companylist = new SelectList(db.Companies.ToList(), "Id", "CompanyName");
            ViewData["Complist"] = companylist;
            return View();
        }
        public JsonResult GetUgName()
        {
            List<UGViewModel> UG = (from u in db.UGs
                                    //where u.UQqualification.Contains(name)
                                    select new UGViewModel
                                    {
                                        value = u.Id,
                                        text= u.UQqualification
                                    }

                          ).ToList();


            return new JsonResult { Data = UG, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetSkillName ()
        {
            List<SkilViewModel> skillist = (from s in db.Skills
                                               select new SkilViewModel
                                            {
                                                   //id=s.id,
                                                   text=s.SkillName,
                                                   value = s.id
                                               }

                                           ).ToList();
         return new JsonResult { Data = skillist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetName()
        {
            List<locationViewModel> UG = (from u in db.Cities
                                        
                                    select new locationViewModel
                                    {
                                        value = u.Id,
                                        text = u.CityName
                                    }

                          ).ToList();


            return new JsonResult { Data = UG, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult SplitId(JobPost jbps,string sid,string lid,string pid,string uid,Company comp)
        {

            db.Companies.Add(comp);
            db.SaveChanges();

            comp.Id = jbps.CompanyId;

            db.JobPosts.Add(jbps);
            db.SaveChanges();

            //List<int> list = new List<int>();
            List<JobPostSkill> jbpost = new List<JobPostSkill>();
             string[] split = sid.Split(',');
           // string[] split = jbps.skill.SkillName.Split(',');
            foreach (string listid in split)
            {
                JobPostSkill jobpostskill = new JobPostSkill();
                jobpostskill.Id = int.Parse(listid);
                jobpostskill.Isactive = true;
                jobpostskill.JobPostId = jbps.Id;
                db.JobPostSkills.Add(jobpostskill);

            }
            // List<JobLocDet> joblocdet = new List<JobLocDet>();
            //string[] splited = jbps.city.CityName.Split(',');
            string[] splited = lid.Split(',');
            foreach (string locid in splited)
            {
                JobLocDet job = new JobLocDet();
                job.CityID = int.Parse(locid);
                job.Isactive = true;
                job.CreatedOn = DateTime.Now;
                job.JobPostId = jbps.Id;
                // joblocdet.Add(job);
                db.JobLocDets.Add(job);
            }

            string[] splitted = uid.Split(',');
            foreach (string Ugid in splitted)
            {
                JobUgDet ug = new JobUgDet();
                ug.UgId = int.Parse(Ugid);
                ug.Isactive = true;
                ug.CreatedOn = DateTime.Now;
                ug.JobPostId = jbps.Id;
                db.JobUgDets.Add(ug);
            }
            string[] spl = pid.Split(',');
            //string[] splitpg = jbps.jobPgDet.splitPgId.Split(',');
            foreach (string pg in spl)
            {
                JobPgDet jobpg = new JobPgDet();
                jobpg.PGiD = int.Parse(pg);
                jobpg.Isactive = true;
                jobpg.CreatedOn = DateTime.Now;
                jobpg.JobPostId = jbps.Id;
                db.JobPgDets.Add(jobpg);
            }
            return Json("", JsonRequestBehavior.AllowGet);

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
                        path = System.IO.Path.Combine(Server.MapPath("~/CompanyImage"),
                                                  Path.GetFileName(pic.FileName));
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