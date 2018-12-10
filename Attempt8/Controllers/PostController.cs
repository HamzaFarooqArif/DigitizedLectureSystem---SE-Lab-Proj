using Attempt8.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt8.Controllers
{
    public class PostController : Controller
    {
       
        public ActionResult Index()
        {
            if(DLSInterface.loggedEmail == null)
            {
                return RedirectToAction("Login", "Person");
            }
            SE_ProjectEntities db = new SE_ProjectEntities();
            List<PostViewModels> Posts = new List<PostViewModels>();
            foreach(Post p in db.Posts)
            {
                if(p.class_id == DLSInterface.ClassEntered)
                {
                    PostViewModels each_post = new PostViewModels();
                    each_post.Email = p.email;
                    each_post.id = p.id;
                    each_post.Summary = p.Summary;
                    each_post.details = p.Details;
                    each_post.image = p.Picture;
                    Posts.Add(each_post);
                }


            }
            return View(Posts);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Post/Create
        public ActionResult Create()
        {
            if (DLSInterface.loggedEmail == null)
            {
                RedirectToAction("Login", "Person");
             }
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, string Summary, string Details)
        {
            try
            {
                if (DLSInterface.loggedEmail == null)
                {
                    RedirectToAction("Login", "Person");
                }

                SE_ProjectEntities db = new SE_ProjectEntities();
                Post p = new Post();
                p.class_id = DLSInterface.ClassEntered;
                p.email = DLSInterface.loggedEmail;
                p.Summary = Summary;
                p.Details = Details;
                if (file != null)
                {
                    var bs = new byte[file.ContentLength];
                    using (var fs = file.InputStream)
                    {
                        var offset = 0;
                        do
                        {
                            offset += fs.Read(bs, offset, bs.Length - offset);
                        } while (offset < bs.Length);
                    }
                    p.Picture = bs;
                }
                else
                {
                    p.Picture = null;
                }

                db.Posts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Create", "Post");
            }
            catch
            {
                return View();
            }
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
           
             

            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,EditViewModels e, HttpPostedFileBase file)
        {
            try
            {
                return View();   
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
               
                return RedirectToAction("Index", "Post");

            }
            catch
            {
                return RedirectToAction("Index");
            }
            
           
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection c)
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }
        
    }
}
