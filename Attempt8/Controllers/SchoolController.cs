using Attempt8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        public ActionResult Index()
        {
            return View();
        }

        // GET: School/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: School/Create
        public ActionResult Create()
        {
            ViewBag.Warn = "";
            return View();
        }

        // POST: School/Create
        [HttpPost]
        public ActionResult Create(SchoolViewModels model)
        {
            try
            {
                SE_ProjectEntities db = new SE_ProjectEntities();

                if(DLSInterface.loggedEmail == null)
                {
                    ViewBag.Warn = "Please Login First";
                }

                else if(db.TeacherTbls.Any(t=>t.Email.Equals(DLSInterface.loggedEmail)))
                {
                    SchoolTbl s = new SchoolTbl();
                    s.Name = model.name;

                    if(db.SchoolTbls.Any(S=>S.Name.Equals(model.name)))
                    {
                        ViewBag.Warn = "School Already Exists";
                    }
                    else
                    {
                        db.SchoolTbls.Add(s);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.Warn = "You Must Be Teacher To Register School";
                }
                if(ViewBag.Warn == null)
                {
                    return RedirectToAction("Create", "Class");
                }

                return View("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: School/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: School/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: School/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: School/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
