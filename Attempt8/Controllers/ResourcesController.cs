using Attempt8.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Controllers
{
    public class ResourcesController : Controller
    {
        // GET: Resources
        public ActionResult Index()
        {
            
            SE_ProjectEntities db = new SE_ProjectEntities();
            List<ResourceViewModels> model = new List<ResourceViewModels>();
            foreach(MaterialResource m in db.MaterialResources)
            {
                if(m.Classid == DLSInterface.ClassEntered)
                {
                    ResourceViewModels s = new ResourceViewModels();
                    s.name = m.Name;
                    s.id = m.id;
                    model.Add(s);
                }
            }
            return View(model);
        }

        // GET: Resources/Details/5
        public ActionResult Details(int id)
        {

            try
            {

                MaterialResource s = new MaterialResource();
                SE_ProjectEntities db = new SE_ProjectEntities();
                foreach (MaterialResource b in db.MaterialResources)
                {
                    if (b.id == id)
                    {
                        s.Name = b.Name;
                        s.Length = b.Length;
                        s.Content = b.Content;
                        s.id = b.id;
                        s.Type = b.Type;
                        break;
                    }
                }
                MemoryStream ms = new MemoryStream(s.Content, 0, 0, true, true);
                Response.ContentType = s.Type;
                Response.AddHeader("content-disposition", "attachment;filename=" + s.Name);
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
                return new FileStreamResult(Response.OutputStream, s.Type);

            }
            catch
            {
                return View();
            }
        }

        // GET: Resources/Create
        public ActionResult Create()
        {
            if(!DLSInterface.isTeacher(DLSInterface.loggedEmail))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Resources/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase uploadFile)
        {
       

                byte[] tempFile = new byte[uploadFile.ContentLength];
                uploadFile.InputStream.Read(tempFile, 0, uploadFile.ContentLength);
                SE_ProjectEntities db = new SE_ProjectEntities();
                MaterialResource s = new MaterialResource();
                s.Content = tempFile;
                s.Name = uploadFile.FileName;
                s.Length = uploadFile.ContentLength;
                s.Type = uploadFile.ContentType;
                s.TeacherId = DLSInterface.GetIdByEmail(DLSInterface.loggedEmail);
                s.Classid = DLSInterface.ClassEntered;
                db.MaterialResources.Add(s);
                db.SaveChanges();       


                return RedirectToAction("Index");
        
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Resources/Edit/5
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

        // GET: Resources/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Resources/Delete/5
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
