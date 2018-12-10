using Attempt8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        public ActionResult Index()
        {
            SE_ProjectEntities db = new SE_ProjectEntities();
            List<AnnouncementViewModels> ann = new List<AnnouncementViewModels>();
            foreach(Announcement a in db.Announcements)
            {
                if(a.ClassId == DLSInterface.ClassEntered)
                {
                    AnnouncementViewModels b = new AnnouncementViewModels();
                    b.ClassId = a.ClassId;
                    b.id = a.id;
                    b.TeacherId = a.TeacherId;
                    b.Text = a.Text;
                    ann.Add(b);
                }
            }
            return View(ann);
        }

        // GET: Announcement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Announcement/Create
        public ActionResult Create()
        {
            if(DLSInterface.isTeacher(DLSInterface.loggedEmail) == false)
            {
                RedirectToAction("Index");
            }
            return View();
        }

        // POST: Announcement/Create
        [HttpPost]
        public ActionResult Create(AnnouncementViewModels collection)
        {
            try
            {
                // TODO: Add insert logic here
                Announcement a = new Announcement();
                a.ClassId = DLSInterface.ClassEntered;
                a.TeacherId = DLSInterface.GetIdByEmail(DLSInterface.loggedEmail);
                a.Text = collection.Text;
                SE_ProjectEntities db = new SE_ProjectEntities();
                db.Announcements.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Announcement/Edit/5
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

        // GET: Announcement/Delete/5
        public ActionResult Delete(int id)
        {
            SE_ProjectEntities db = new SE_ProjectEntities();
            db.Announcements.Remove(db.Announcements.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Announcement/Delete/5
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
